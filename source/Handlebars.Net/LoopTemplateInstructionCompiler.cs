using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public class LoopTemplateInstructionCompiler : BaseTemplateInstructionCompiler {
		#region ITemplateParser Members

		public override IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens, ITemplateCompiler compiler ) {
			var tokenList = tokens.ToList();

			// TODO: protect against bad token sets passed in
			// must be at least an opening and closing token

			var arguments = GetArguments( tokenList[0], compiler.Ruleset );
			// TODO validate arguments

			var childInstructions = compiler.Compile( tokenList.Skip( 1 ).Take( tokenList.Count - 2 ) );

			return new[] {
				new LoopTemplateInstruction(arguments.First(), childInstructions),
			};
		}

		#endregion
	}
}
