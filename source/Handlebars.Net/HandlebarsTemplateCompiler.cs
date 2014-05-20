using System;
using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public class HandlebarsTemplateCompiler : ITemplateCompiler {
		// TODO: make these a configuration object
		private const string tokenOpen = "{{";
		private const string tokenClose = "}}";

		private Dictionary<string, Type> RegisteredHelpers { get; set; }
		public List<ITemplateInstruction> Instructions { get; set; }

		public HandlebarsTemplateCompiler() {
			Instructions = new List<ITemplateInstruction>();
			RegisterDefaultHelpers();
		}

		private void RegisterDefaultHelpers() {
			RegisteredHelpers = new Dictionary<string, Type>
			{
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

			var enumerator = tokens.GetEnumerator();

			while ( enumerator.MoveNext() ) {
				var token = enumerator.Current;

				instructions.Add( new Literal( token ) );
			}

			return instructions;
		}

		public IEnumerable<string> TokenizeTemplate( string template ) {
			return new[] { template };
		}
	}
}
