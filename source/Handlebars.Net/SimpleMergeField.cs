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
				: String.Format("{0:" + Format + "}", resolvedObject);
		}
	}
}