using System.Collections.Generic;
using System.Linq;

namespace Handlebars.Net {

	public class LoopTemplateInstruction {
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
	}
}
