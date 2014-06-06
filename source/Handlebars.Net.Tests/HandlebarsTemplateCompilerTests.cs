using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class HandlebarsTemplateCompilerTests {
		private HandlebarsTemplateCompiler compiler;

		[TestInitialize]
		public void Setup() {
			compiler = new HandlebarsTemplateCompiler();
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerLiteralTemplate() {
			var actual = compiler.Compile( "Literal" );

			var expected = new List<ITemplateInstruction> {
				new Literal("Literal")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerSimpleMergeField() {
			var actual = compiler.Compile( "{{Field:Format}}" );

			var expected = new List<ITemplateInstruction> {
				new SimpleMergeField("Field", "Format")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerMergeAndLiteralTemplate() {
			var actual = compiler.Compile( "Literal{{Field1:Format1}}{{ Field2:Format2 }}" );

			var expected = new List<ITemplateInstruction> {
				new Literal("Literal"),
				new SimpleMergeField("Field1", "Format1"),
				new SimpleMergeField("Field2", "Format2")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		[ExpectedException( typeof( EndOfStreamException ) )]
		public void HandlebarsTemplateCompilerUnclosedMergeField() {
			compiler.Compile( "Literal{{Field:Format" );
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerLoopInstruction() {
			var actual = compiler.Compile( "{{#each Field.OtherField}}a{{Field}}{{/each}}" );

			var expected = new List<ITemplateInstruction>{
				new LoopTemplateInstruction("Field.OtherField",
					new List<ITemplateInstruction> {
						new Literal("a"),
						new SimpleMergeField("Field")
					})
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerBlockHelperWithLeadingSpaces() {
			var actual = compiler.Compile( "{{# each Field.OtherField}}{{/each}}" );

			var expected = new List<ITemplateInstruction>{
				new LoopTemplateInstruction("Field.OtherField",
					new List<ITemplateInstruction>())
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		public void HandlebarsTemplateCompilerBlockHelperWithSpaceInClosingTag() {
			var actual = compiler.Compile( "{{#each Field.OtherField}}{{/ each}}" );

			var expected = new List<ITemplateInstruction>{
				new LoopTemplateInstruction("Field.OtherField",
					new List<ITemplateInstruction>())
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		[ExpectedException( typeof( EndOfStreamException ) )]
		public void HandlebarsTemplateCompilerUnclosedBlockHelper() {
			compiler.Compile( "{{#each Field.OtherField}}a{{Field}}" );
		}

		[TestMethod, Ignore]
		public void HandlebarsTemplateCompilerIfInstructionWithTruthyProperty()
		{
			var actual = compiler.Compile("{{#if Field}}{{/if}}");

			var expected = new List<ITemplateInstruction>
			{
				new IfTemplateInstruction ("Field", ArgumentType.Property, new ITemplateInstruction[0] )
			};
		}

		#region Helper Methods

		private static void CompareInstructions( IReadOnlyList<ITemplateInstruction> expected, IReadOnlyList<ITemplateInstruction> actual ) {
			Assert.AreEqual( expected.Count, actual.Count );

			var ct = expected.Count;
			for ( var i = 0; i < ct; i++ ) {
				Assert.AreEqual( expected[i], actual[i] );
			}
		}

		#endregion
	}
}
