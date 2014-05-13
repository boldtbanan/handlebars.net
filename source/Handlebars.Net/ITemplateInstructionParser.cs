using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateInstructionParser {
		IEnumerable<ITemplateInstruction> Parse( string template, ITemplateParser parser );
	}
}