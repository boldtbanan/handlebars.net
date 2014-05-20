using System;
using System.Collections.Generic;

namespace Handlebars.Net {
	public class Literal : BaseTemplateInstruction {
		public string Value { get; set; }

		public Literal( string value ) {
			if ( value == null ) { throw new ArgumentNullException( "value" ); }

			Value = value;
		}

		#region ITemplateInstruction Members

		public override string Evaluate( Stack<object> context ) {
			return Value;
		}

		#endregion

		public override int GetHashCode() {
			return Value.GetHashCode();
		}

		public override bool Equals( object obj ) {
			var literal = obj as Literal;
			if ( literal == null ) { return false; }

			return this.GetHashCode() == literal.GetHashCode();
		}

		public override string ToString() {
			return Value;
		}
	}
}
