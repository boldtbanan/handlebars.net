using System;
using System.Collections.Generic;

namespace Handlebars.Net {
	public class LoopTemplateInstructionCompiler : ITemplateInstructionCompiler {

		#region ITemplateParser Members

		public IEnumerable<ITemplateInstruction> Compile( string template, ITemplateCompiler parser ) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
