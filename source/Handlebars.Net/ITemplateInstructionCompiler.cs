using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateInstructionCompiler {
		IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens, ITemplateCompiler parser );
	}
}