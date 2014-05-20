using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class HandlebarsTemplateCompilerTests {
		[TestMethod]
		public void HandlebarsTemplateParserParseLiteral() {
			var compiler = new HandlebarsTemplateCompiler();

			var actual = compiler.Compile( "Literal" );

			var expected = new List<ITemplateInstruction> {
				new Literal("Literal")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		[Ignore]
		public void HandlebarsTemplateParserParseSimpleMergeField() {
			var compiler = new HandlebarsTemplateCompiler();

			var actual = compiler.Compile( "{{Field:Format}}" );

			var expected = new List<ITemplateInstruction> {
				new SimpleMergeField("Field", "Format")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		[TestMethod]
		[Ignore]
		public void HandlebarsTemplateParserParseMergeAndLiteral() {
			var compiler = new HandlebarsTemplateCompiler();

			var actual = compiler.Compile( "Literal{{Field:Format}}" );

			var expected = new List<ITemplateInstruction> {
				new Literal("Literal"),
				new SimpleMergeField("Field", "Format")
			};

			CompareInstructions( expected, actual.ToList() );
		}

		private void CompareInstructions( List<ITemplateInstruction> expected, List<ITemplateInstruction> actual ) {
			Assert.AreEqual( expected.Count, actual.Count );

			var ct = expected.Count;
			for ( var i = 0; i < ct; i++ ) {
				Assert.AreEqual( expected[i], actual[i] );
			}
		}
	}
}
