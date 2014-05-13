using System.Collections.Generic;
using System.IO;

namespace Handlebars.Net {
	public abstract class BaseTemplateInstruction : ITemplateInstruction {

		public abstract string Evaluate( Stack<object> context );

		public virtual string Evaluate( object scope ) {
			return Evaluate( new Stack<object>( new[] { scope } ) );
		}

		public virtual void Write( TextWriter writer, object scope ) {
			Write( writer, new Stack<object>( new[] { scope } ) );
		}

		public virtual void Write( TextWriter writer, Stack<object> scope ) {
			writer.Write( Evaluate( scope ) );
		}
	}
}
