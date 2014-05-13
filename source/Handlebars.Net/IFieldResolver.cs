using System.Collections.Generic;

namespace Handlebars.Net {
	public interface IFieldResolver {
		object Resolve( IEnumerable<object> context, string field );
	}
}