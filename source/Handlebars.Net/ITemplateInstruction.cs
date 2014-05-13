using System.Collections.Generic;
using System.IO;

namespace Handlebars.Net {
	public interface ITemplateInstruction {
		string Evaluate( object scope );
		string Evaluate( Stack<object> context );
		void Write( TextWriter writer, object scope );
		void Write( TextWriter writer, Stack<object> context );
	}
}