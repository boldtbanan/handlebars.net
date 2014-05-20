using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class LiteralTests {
		[TestMethod]
		public void Literal() {
			const string literalValue = "Test value";

			var sut = new Literal( literalValue );

			var result = sut.Evaluate( new { } );

			Assert.AreEqual( literalValue, result );
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void LiteralWithNullValue() {
			var sut = new Literal( null );
		}

		// BaseTemplateInstruction is an abstract class. We're just testing the template method of Write here
		// which just calls evaluate and performs the write of the resultant value. 
		[TestMethod]
		public void BaseTemplateInstructionWrite() {
			const string literalValue = "Test value";

			var sut = new Literal( literalValue );

			var sb = new StringBuilder();
			sut.Write( new StringWriter( sb ), new { } );

			Assert.AreEqual( literalValue, sb.ToString() );
		}

		[TestMethod]
		public void LiteralEquals() {
			Assert.AreEqual( new Literal( "Literal Value" ), new Literal( "Literal Value" ) );
			Assert.AreNotEqual( new Literal( "Literal Value 1" ), new Literal( "Literal Value 2" ) );
		}
	}
}
