using System;
using System.Collections.Generic;

namespace Handlebars.Net {
	public class HandlebarsTemplateParser : ITemplateParser {
		// TODO: make these a configuration object
		private const string tokenOpen = "{{";
		private const string tokenClose = "}}";

		private Dictionary<string, Type> RegisteredHelpers { get; set; }
		public List<ITemplateInstruction> Instructions { get; set; }

		public HandlebarsTemplateParser() {
			Instructions = new List<ITemplateInstruction>();
			RegisterDefaultHelpers();
		}

		private void RegisterDefaultHelpers() {
			RegisteredHelpers = new Dictionary<string, Type>
			{
				{"each", typeof(LoopTemplateInstructionParser)}
			};
		}

		#region ITemplateParser Members

		public IEnumerable<ITemplateInstruction> Parse( string template ) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
