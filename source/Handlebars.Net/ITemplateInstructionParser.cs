using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateInstructionCompiler {
		IEnumerable<ITemplateInstruction> Compile( string template, ITemplateCompiler parser );
	}
}