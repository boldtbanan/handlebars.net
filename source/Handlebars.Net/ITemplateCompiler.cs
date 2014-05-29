using System.Collections.Generic;

namespace Handlebars.Net {
	public interface ITemplateCompiler {
		TemplateRuleset Ruleset { get; }
		IEnumerable<ITemplateInstruction> Compile( string template );
		IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens );
	}
}