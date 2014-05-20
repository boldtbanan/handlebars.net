using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateCompiler {
		IEnumerable<ITemplateInstruction> Compile( string template );
	}
}