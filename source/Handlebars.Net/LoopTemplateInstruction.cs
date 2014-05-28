using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Handlebars.Net.Extensions;

namespace Handlebars.Net {

	public class LoopTemplateInstruction : BaseTemplateInstruction {
		protected IEnumerable<ITemplateInstruction> ChildInstructions { get; set; }
		protected string FieldName { get; set; }

		public LoopTemplateInstruction( string fieldName, IEnumerable<ITemplateInstruction> childInstructions ) {
			FieldName = fieldName;
			ChildInstructions = childInstructions;
		}

		public override bool Equals( object obj ) {
			var loop = obj as LoopTemplateInstruction;
			if ( loop == null ) { return false; }

			return FieldName.Equals( loop.FieldName )
				   && ChildInstructions.SequenceEqual( loop.ChildInstructions );
		}

		public override int GetHashCode() {
			unchecked {
				var hashCode = 13;

				hashCode += hashCode * 13 + ( FieldName.GetHashCode() );
				hashCode += hashCode * 13 + ( ChildInstructions.GetHashCode() );

				return hashCode;
			}
		}

		#region ITemplateInstruction Methods

		public override string Evaluate( Stack<object> context ) {
			var resolver = new FieldResolver();
			var resolvedObject = resolver.Resolve( context, FieldName ) as IEnumerable;

			// strings are IEnumerable, but that differs from the behavior of handlebars.js
			// and will probably be confusing to an end user
			if ( resolvedObject == null || resolvedObject is String ) { return ""; }

			var sb = new StringBuilder();

			resolvedObject.Each( x => ChildInstructions.Each( y => sb.Append( y.Evaluate( x ) ) ) );

			return sb.ToString();
		}

		#endregion
	}
}
