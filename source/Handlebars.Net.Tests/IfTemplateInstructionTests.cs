using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class IfTemplateInstructionTests {
		[TestMethod]
		public void IfTemplateInstructionWithBooleanTruePropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = true } );

			Assert.AreEqual( "Apple", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithBooleanFalsePropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = false } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithTruthyNumberPropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = 1 } );

			Assert.AreEqual( "Apple", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithFalseyNumberPropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = 0 } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithTruthyStringPropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = "false" } );

			Assert.AreEqual( "Apple", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithFalseyStringPropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = "" } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithTruthyEnumerablePropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = new[] { 1 } } );

			Assert.AreEqual( "Apple", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithFalseyEnumerablePropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = new int[0] } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithUnresolvedPropertyArgument() {
			var instruction = new IfTemplateInstruction( "Field", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { AnotherField = 1 } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void IfTemplateInstructionWithNullPropertyArgument() {
			var instruction = new IfTemplateInstruction( "this", ArgumentType.Property,
				new List<ITemplateInstruction> {
					new Literal("Apple")
				}
			);

			var actual = instruction.Evaluate( new { Field = ( string ) null } );

			Assert.AreEqual( "", actual );
		}

		#region equality tests

		[TestMethod]
		public void IfTemplateInstructionEqualsSameObject() {
			var childInstructions = new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			};

			var instruction = new IfTemplateInstruction( "Field1", ArgumentType.Method, childInstructions );

			Assert.AreEqual( instruction, instruction );
		}

		[TestMethod]
		public void IfTemplateInstructionEqualsDifferentObject() {
			Assert.AreEqual( new IfTemplateInstruction( "Field1", ArgumentType.Method, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new IfTemplateInstruction( "Field1", ArgumentType.Method, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ) );
		}

		[TestMethod]
		public void IfTemplateInstructionNotEqualWithDifferentField() {
			Assert.AreNotEqual( new IfTemplateInstruction( "Field1", ArgumentType.Method, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new IfTemplateInstruction( "Field2", ArgumentType.Method, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ) );
		}

		[TestMethod]
		public void IfTemplateInstructionNotEqualWithArgumentTypesField() {
			Assert.AreNotEqual( new IfTemplateInstruction( "Field1", ArgumentType.Method, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new IfTemplateInstruction( "Field1", ArgumentType.Property, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ) );
		}

		[TestMethod]
		public void LoopTemplateInstructionNotEqualWithDifferentChildren() {
			Assert.AreNotEqual( new IfTemplateInstruction( "Field1", ArgumentType.Property, new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new IfTemplateInstruction( "Field1", ArgumentType.Property, new[] {
				new Literal("Literal1")
			} ) );
		}

		#endregion

	}
}
