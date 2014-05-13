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
	}
}
