using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {
	public class FieldResolver : IFieldResolver {
		#region IFieldResolver Members

		public object Resolve( IEnumerable<object> context, string field ) {
			if ( context == null ) { throw new ArgumentNullException( "context" ); }
			if ( field == null ) { throw new ArgumentNullException( "field" ); }

			var contextIsEmpty = true;
			var enumerator = context.GetEnumerator();
			object value = null;

			while ( enumerator.MoveNext() ) {
				contextIsEmpty = false;
				var scope = enumerator.Current;

				if ( field == "this" ) {
					return scope;
				}

				if ( TryResolve( scope, field, out value ) ) { break; }
			}

			if ( contextIsEmpty ) { throw new ArgumentException( "Empty context", "context" ); }

			return value;
		}

		#endregion

		private static bool TryResolve( object scope, string field, out object value ) {
			// split to sup dot notation
			var fields = field.Split( '.' );
			return TryResolve( scope, fields, out value );

		}

		private static bool TryResolve( object scope, IList<string> fields, out object value ) {
			return TryResolveDictionary( scope as IDictionary, fields, out value )
				|| TryResolveObject( scope, fields, out value );
		}

		private static bool TryResolveDictionary( IDictionary scope, IList<string> fields, out object value ) {
			value = null;

			// We were passed a null scope, so we can't do anything more
			if ( scope == null ) { return false; }

			var field = fields[0];

			var dictionaryTypes = scope.GetType().GetGenericArguments();

			// This isn't an IDictionary<String, T> or the object can't be resolved
			if ( dictionaryTypes.FirstOrDefault() != typeof( String ) || !scope.Contains( field ) ) { return false; }

			value = scope[field];

			return ( fields.Count == 1 || TryResolve( scope[field], fields.Skip( 1 ).ToArray(), out value ) );
		}

		private static bool TryResolveObject( object scope, IList<string> fields, out object value ) {
			value = null;

			// We were passed a null scope, so we can't do anything more
			if ( scope == null ) { return false; }

			var field = fields[0];

			var property = scope.GetType().GetProperty( field );
			if ( property == null ) { return false; }

			value = property.GetValue( scope, null );
			return fields.Count == 1 || TryResolve( value, fields.Skip( 1 ).ToArray(), out value );
		}


	}
}
