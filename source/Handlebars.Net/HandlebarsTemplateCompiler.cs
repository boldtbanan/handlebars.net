using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Handlebars.Net {
	public class HandlebarsTemplateCompiler : ITemplateCompiler {
		public TemplateRuleset Ruleset { get; protected set; }

		// TODO: This guy should ensure that all helpers are ITemplateInstructionCompilers 
		private Dictionary<string, Type> RegisteredCompilers { get; set; }
		public List<ITemplateInstruction> Instructions { get; set; }

		public HandlebarsTemplateCompiler() {
			Ruleset = TemplateRuleset.Handlebars;

			Instructions = new List<ITemplateInstruction>();
			RegisterDefaultCompilers();
		}

		private void RegisterDefaultCompilers() {
			RegisteredCompilers = new Dictionary<string, Type> {
				{"each", typeof(LoopTemplateInstructionCompiler)},
				{"if", typeof(IfTemplateInstructionCompiler)}
			};
		}

		#region ITemplateCompiler Members

		public IEnumerable<ITemplateInstruction> Compile( string template ) {
			var tokens = TokenizeTemplate( template );
			return Compile( tokens );
		}

		public IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens ) {
			var instructions = new List<ITemplateInstruction>();

			var tokenSkip = Ruleset.TokenOpen.Length;
			var tokenStop = Ruleset.TokenOpen.Length + Ruleset.TokenClose.Length;

			var enumerator = tokens.GetEnumerator();

			while ( enumerator.MoveNext() ) {
				var token = enumerator.Current;

				if ( token.StartsWith( Ruleset.TokenHelperOpen, StringComparison.InvariantCultureIgnoreCase ) ) {
					var helperName = GetHelperName( token );
					Type compilerType;

					if ( !RegisteredCompilers.TryGetValue( helperName, out compilerType ) ) {
						throw new TemplateParseException( String.Format( "Unknown Helper {0}", helperName ), token );
					}

					var constructorInfo = compilerType.GetConstructor( Type.EmptyTypes );
					if ( constructorInfo == null ) {
						// TODO: make this a typed exception
						throw new Exception( "No public constructor that takes no parameters" );
					}

					var compiler = constructorInfo.Invoke( null ) as ITemplateInstructionCompiler;

					if ( compiler == null ) {
						// TODO: make this a typed exception
						throw new Exception( "Not an ITemplateInstructionCompiler" );
					}

					var helperTokens = ExtractHelperTokenBlock( enumerator, helperName );

					instructions.AddRange( compiler.Compile( helperTokens, this ) );

				} else if ( token.StartsWith( Ruleset.TokenOpen, StringComparison.InvariantCultureIgnoreCase ) ) {
					var field = token.Substring( tokenSkip, token.Length - tokenStop ).Trim().Split( ':' );

					var fieldName = field[0];
					var fieldFormat = field.Length > 1 ? field[1] : null;

					instructions.Add( new SimpleMergeField( fieldName, fieldFormat ) );
				} else {
					instructions.Add( new Literal( token ) );
				}
			}

			return instructions;
		}

		#endregion

		public IEnumerable<string> TokenizeTemplate( string template ) {
			var tokens = new List<string>();

			var isOpen = false;

			var currentIndex = 0;
			var nextIndex = -1;

			do {
				nextIndex = template.IndexOf( isOpen ? Ruleset.TokenClose : Ruleset.TokenOpen, currentIndex,
					StringComparison.InvariantCultureIgnoreCase );

				if ( nextIndex > -1 ) {
					if ( isOpen ) {
						nextIndex += Ruleset.TokenClose.Length;
					}
					if ( nextIndex > currentIndex ) {
						tokens.Add( template.Substring( currentIndex, nextIndex - currentIndex ) );
					}

					currentIndex = nextIndex;
					isOpen = !isOpen;
				} else {
					if ( isOpen ) {
						throw new EndOfStreamException( String.Format( "Reached end of stream before finding a matching {0}", Ruleset.TokenClose ) );
					}

					if ( currentIndex < template.Length - 1 ) {
						tokens.Add( template.Substring( currentIndex ) );
					}
				}

			} while ( nextIndex > -1 );

			return tokens;
		}

		protected string GetHelperName( string token ) {
			return token.Replace( Ruleset.TokenHelperOpen, "" ).Replace( Ruleset.TokenClose, "" ).Trim().Split( ' ' )[0];
		}


		private IEnumerable<string> ExtractHelperTokenBlock( IEnumerator<string> enumerator, string tag ) {
			var tokens = new List<string>();

			var closingTagRegex = new Regex( String.Format( @"{0}\s*{1}\s*{2}",
				Ruleset.TokenHelperClose, tag, Ruleset.TokenClose ) );

			var closingTagFound = false;
			var nestingLevel = 0;

			do {
				var token = enumerator.Current;
				tokens.Add( token );

				if ( token.StartsWith( Ruleset.TokenHelperOpen, StringComparison.InvariantCultureIgnoreCase )
					&& GetHelperName( token ) == tag ) {
					nestingLevel++;
				}

				if ( closingTagRegex.IsMatch( token ) && --nestingLevel == 0 ) {
					closingTagFound = true;
					break;
				}
			} while ( !closingTagFound && enumerator.MoveNext() );

			if ( !closingTagFound ) {
				throw new EndOfStreamException( String.Format(
					"Reached end of stream before finding a closing {0}{1}{2}",
					Ruleset.TokenHelperClose, tag, Ruleset.TokenClose ) );
			}

			return tokens;
		}
	}
}
