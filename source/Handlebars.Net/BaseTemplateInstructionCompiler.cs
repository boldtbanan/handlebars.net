using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public abstract class BaseTemplateInstructionCompiler : ITemplateInstructionCompiler {

		#region ITemplateInstructionCompiler Members

		public abstract IEnumerable<ITemplateInstruction> Compile(IEnumerable<string> tokens, ITemplateCompiler parser);

		#endregion

		protected virtual string[] GetArguments( string token, TemplateRuleset ruleset ) {
			return token.Substring( ruleset.TokenHelperOpen.Length,
				token.Length - ( ruleset.TokenHelperOpen.Length + ruleset.TokenClose.Length ) )
				.Trim().Split( ' ' )
				.Skip( 1 ) // skip the tag
				.ToArray();	
		}
	}
}