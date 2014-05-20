using System;
using System.Collections.Generic;

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
			return new[] { new Literal( template ) };
		}

		#endregion
	}
}
