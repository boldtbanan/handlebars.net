using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateParser {
		IEnumerable<ITemplateInstruction> Parse( string template );
	}
}