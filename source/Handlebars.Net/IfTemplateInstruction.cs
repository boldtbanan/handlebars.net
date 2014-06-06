using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Handlebars.Net.Extensions;

namespace Handlebars.Net {
	public enum ArgumentType {
		Property,
		Method
	}

	public class IfTemplateInstruction : BaseTemplateInstruction {
		public string Argument { get; set; }
		public ArgumentType ArgumentType { get; set; }
		protected IEnumerable<ITemplateInstruction> ChildInstructions { get; set; }

		public IfTemplateInstruction( string argument, ArgumentType argumentType,
			IEnumerable<ITemplateInstruction> childInstructions ) {
			Argument = argument;
			ArgumentType = argumentType;
			ChildInstructions = childInstructions;
		}

		public override string Evaluate( Stack<object> context ) {
			if ( EvaluateArgument( context ) ) {
				return String.Concat( ChildInstructions.Select( x => x.Evaluate( context ) ) );
			}

			return "";
		}

		protected virtual bool EvaluateArgument( Stack<object> context ) {
			switch ( ArgumentType ) {
				case ArgumentType.Property:
					var resolver = new FieldResolver();
					var result = resolver.Resolve( context, Argument );

					return result.IsTruthy();

					break;

				case ArgumentType.Method:
					throw new NotImplementedException();
					break;
			}

			throw new InvalidEnumArgumentException( "ArgumentType", ( int ) ArgumentType, typeof( ArgumentType ) );
		}

		public override bool Equals( object obj ) {
			var instruction = obj as IfTemplateInstruction;
			if ( instruction == null ) { return false; }

			return Argument.Equals( instruction.Argument )
				&& ArgumentType.Equals( instruction.ArgumentType )
				&& ChildInstructions.SequenceEqual( instruction.ChildInstructions );
		}

		public override int GetHashCode() {
			unchecked {
				var hashCode = 13;

				hashCode += hashCode * 13 + ( Argument.GetHashCode() );
				hashCode += hashCode * 13 + ( ArgumentType.GetHashCode() );
				hashCode += hashCode * 13 + ( ChildInstructions.GetHashCode() );

				return hashCode;
			}
		}

	}
}
