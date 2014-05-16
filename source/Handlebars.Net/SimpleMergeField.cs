using System;
using System.Collections.Generic;

namespace Handlebars.Net {
	public class SimpleMergeField : BaseTemplateInstruction {
		public string FieldName { get; set; }
		public string Format { get; set; }

		public SimpleMergeField( string fieldName, string format = null ) {
			FieldName = fieldName;
			Format = format;
		}

		public override string Evaluate( Stack<object> context ) {
			var resolver = new FieldResolver();
			var resolvedObject = resolver.Resolve( context, FieldName );

			if ( resolvedObject == null ) {
				return "";
			}

			return Format == null
				? resolvedObject.ToString()
				: String.Format( "{0:" + Format + "}", resolvedObject );
		}

		public override int GetHashCode() {
			unchecked {
				var hash = 17;
				hash = hash * 23 + ( FieldName == null ? 0 : FieldName.GetHashCode() );
				hash = hash * 23 + ( Format == null ? 0 : Format.GetHashCode() );
				return hash;
			}
		}

		public override bool Equals( object obj ) {
			var field = obj as SimpleMergeField;
			if ( field == null ) { return false; }

			return this.GetHashCode() == obj.GetHashCode()
				|| ( FieldName.Equals( field.FieldName ) && Format.Equals( field.Format ) );
		}
	}
}