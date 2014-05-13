using System.Collections.Generic;
using System.IO;

namespace Handlebars.Net {
	public class Literal : BaseTemplateInstruction {
		public string Value { get; set; }

		public Literal( string value ) {
			Value = value;
		}

		#region ITemplateInstruction Members

		public override string Evaluate( Stack<object> context ) {
			return Value;
		}

		#endregion
	}
}
