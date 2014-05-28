using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class LoopTemplateInstructionTests {

		[TestMethod]
		public void LoopTemplateInstructionLoopLiteral() {
			var sut = new LoopTemplateInstruction( "Field", new[] { new Literal( "Literal" ) } );

			var actual = sut.Evaluate( new {
				Field = new[] {
					new {Fruit = "Apple"},
					new {Fruit = "Banana"}
				}
			} );

			Assert.AreEqual( "LiteralLiteral", actual );
		}

		[TestMethod]
		public void LoopTemplateInstructionLoopSimpleMerge() {
			var sut = new LoopTemplateInstruction( "Field", new[] { new SimpleMergeField( "Fruit" ), } );

			var actual = sut.Evaluate( new {
				Field = new[] {
					new {Fruit = "Apple"},
					new {Fruit = "Banana"}
				}
			} );

			Assert.AreEqual( "AppleBanana", actual );
		}

		[TestMethod]
		public void LoopTemplateInstructionLoopWithMultipleChildInstructions() {
			var sut = new LoopTemplateInstruction( "this", new List<ITemplateInstruction> {
				new SimpleMergeField( "this" ),
				new Literal( "@" )
			} );

			var actual = sut.Evaluate( new[]{
				'p','i','e'
			} );

			Assert.AreEqual( "p@i@e@", actual );
		}

		[TestMethod]
		public void LoopTemplateInstructionLoopOnNonexistantField() {
			var sut = new LoopTemplateInstruction( "Field", new[] { new Literal( "Literal" ) } );

			var actual = sut.Evaluate( new {
				AnotherField = new[] {
					new {Fruit = "Apple"},
					new {Fruit = "Banana"}
				}
			} );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void LoopTemplateInstructionLoopOnNullField() {
			var sut = new LoopTemplateInstruction( "Field", new[] { new Literal( "Literal" ) } );

			var actual = sut.Evaluate( new Dictionary<string, object> { { "Field", null } } );

			Assert.AreEqual( "", actual );
		}

		[TestMethod]
		public void LoopTemplateInstructionLoopOnNonEnumerableField() {
			var sut = new LoopTemplateInstruction( "this", new[] { new Literal( "Literal" ) } );

			var actual = sut.Evaluate( 1 );

			Assert.AreEqual( "", actual );
		}

		// strings are IEnumerable, but we don't want that behavior here, so create an explicit test case
		[TestMethod]
		public void LoopTemplateInstructionLoopOnStringField() {
			var sut = new LoopTemplateInstruction( "this", new List<ITemplateInstruction> {
				new Literal( "Literal" ) 
			} );

			var actual = sut.Evaluate( "Pie" );

			Assert.AreEqual( "", actual );
		}

		#region equality tests

		[TestMethod]
		public void LoopTemplateInstructionEqualsSameObject() {
			var childInstructions1 = new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			};

			var loop1 = new LoopTemplateInstruction( "Field1", childInstructions1 );

			Assert.AreEqual( loop1, loop1 );
		}

		[TestMethod]
		public void LoopTemplateInstructionEqualsDifferentObject() {
			Assert.AreEqual( new LoopTemplateInstruction( "Field1", new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new LoopTemplateInstruction( "Field1", new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ) );
		}

		[TestMethod]
		public void LoopTemplateInstructionNotEqualWithDifferentField() {
			Assert.AreNotEqual( new LoopTemplateInstruction( "Field1", new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new LoopTemplateInstruction( "Field2", new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ) );
		}

		[TestMethod]
		public void LoopTemplateInstructionNotEqualWithDifferentChildren() {
			Assert.AreNotEqual( new LoopTemplateInstruction( "Field1", new[] {
				new Literal("Literal1"),
				new Literal("Literal2")
			} ), new LoopTemplateInstruction( "Field1", new[] {
				new Literal("Literal1")
			} ) );
		}

		#endregion
	}
}
