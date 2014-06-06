using System;
using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public class IfTemplateInstructionCompiler : BaseTemplateInstructionCompiler {
		public override IEnumerable<ITemplateInstruction> Compile( IEnumerable<string> tokens, ITemplateCompiler parser ) {
			var tokenList = tokens.ToList();
			var openTag = tokenList.First();

			var arguments = GetArguments( openTag, parser.Ruleset );
			if (arguments.Length == 0) { throw new ArgumentException("Missing argument"); }

			return new[] {
				new IfTemplateInstruction(arguments[0], ArgumentType.Property,
					parser.Compile(tokenList.Skip(1).Take(tokenList.Count - 2))),
			};
		}
	}
}
