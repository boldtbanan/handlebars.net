using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public class LoopTemplateInstructionCompiler : ITemplateInstructionCompiler {
		#region ITemplateParser Members

		public IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens, ITemplateCompiler compiler ) {
			var tokenList = tokens.ToList();

			// TODO: protect against bad token sets passed in
			// must be at least an opening and closing token

			var openingTag = tokenList[0];
			var arguments = openingTag.Substring( compiler.Ruleset.TokenHelperOpen.Length,
				openingTag.Length - ( compiler.Ruleset.TokenHelperOpen.Length + compiler.Ruleset.TokenClose.Length ) )
				.Trim().Split( ' ' )
				.Skip(1);	// skip the tag

			// TODO validate arguments

			var childInstructions = compiler.Compile( tokenList.Skip( 1 ).Take( tokenList.Count - 2 ) );

			return new[] {
				new LoopTemplateInstruction(arguments.First(), childInstructions),
			};
		}

		#endregion
	}
}
