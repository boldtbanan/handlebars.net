using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Handlebars.Net {
	public class HandlebarsTemplateCompiler : ITemplateCompiler {
		// TODO: make these a configuration object
		private const string tokenOpen = "{{";
		private const string tokenClose = "}}";

		// TODO: This guy should ensure that all helpers are ITemplateInstructionCompilers 
		private Dictionary<string, Type> RegisteredHelpers { get; set; }
		public List<ITemplateInstruction> Instructions { get; set; }

		public HandlebarsTemplateCompiler() {
			Instructions = new List<ITemplateInstruction>();
			RegisterDefaultHelpers();
		}

		private void RegisterDefaultHelpers() {
			RegisteredHelpers = new Dictionary<string, Type> {
				{"each", typeof(LoopTemplateInstructionCompiler)}
			};
		}

		#region ITemplateParser Members

		public IEnumerable<ITemplateInstruction> Compile( string template ) {
			var tokens = TokenizeTemplate( template );
			return Compile( tokens );
		}

		#endregion

		public IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens ) {
			var instructions = new List<ITemplateInstruction>();

			var tokenSkip = tokenOpen.Length;
			var tokenStop = tokenOpen.Length + tokenClose.Length;

			var enumerator = tokens.GetEnumerator();

			while ( enumerator.MoveNext() ) {
				var token = enumerator.Current;

				if ( token.StartsWith( tokenOpen, StringComparison.InvariantCultureIgnoreCase ) ) {
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

		public IEnumerable<string> TokenizeTemplate( string template ) {
			var tokens = new List<string>();

			var isOpen = false;

			var currentIndex = 0;
			var nextIndex = -1;

			do {
				nextIndex = template.IndexOf( isOpen ? tokenClose : tokenOpen, currentIndex,
					StringComparison.InvariantCultureIgnoreCase );

				if ( nextIndex > -1 ) {
					if ( isOpen ) {
						nextIndex += tokenClose.Length;
					}
					if ( nextIndex > currentIndex ) {
						tokens.Add( template.Substring( currentIndex, nextIndex - currentIndex ) );
					}

					currentIndex = nextIndex;
					isOpen = !isOpen;
				} else {
					if ( isOpen ) {
						throw new EndOfStreamException( String.Format( "Reached end of stream before finding a matching {0}", tokenClose ) );
					}

					if ( currentIndex < template.Length - 1 ) {
						tokens.Add( template.Substring( currentIndex ) );
					}
				}

			} while ( nextIndex > -1 );

			return tokens;
		}
	}
}
