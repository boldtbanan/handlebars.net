using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlebars.Net {

	public class LoopTemplateInstruction {
		public ITemplateCompiler Compiler { get; set; }
		private List<ITemplateInstruction> ChildInstructions { get; set; }

		public LoopTemplateInstruction( ITemplateCompiler compiler ) {
			Compiler = compiler;
		}
	}
}
