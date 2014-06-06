using System;
using System.Collections;

namespace Handlebars.Net.Extensions {
	public static class ObjectEx {
		/// <summary>
		/// Determines the "truthiness" of an object using javascript rules
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static bool IsTruthy( this object o ) {
			if ( o == null ) { return false; }
			if ( o is bool ) { return ( bool ) o; }
			if ( o is double ) { return !o.Equals( 0D ) && !o.Equals( double.NaN ); }
			if ( o is float ) { return !o.Equals( 0F ) && !o.Equals( float.NaN ); }
			if ( o is decimal
				|| o is int
				|| o is long
				|| o is short
				|| o is uint
				|| o is ulong
				|| o is ushort ) {
				return !o.Equals( Convert.ChangeType( 0, o.GetType() ) );
			}

			var enumerable = ( o as IEnumerable );
			return ( enumerable != null ) && enumerable.GetEnumerator().MoveNext();

		}
	}
}
