using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class SimpleMergeFieldTests {
		[TestMethod]
		public void SimpleMergeFieldWithThis() {
			var sut = new SimpleMergeField( "this" );

			const string resolveTo = "Bananas";

			var result = sut.Evaluate( resolveTo );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void SimpleMergeFieldWithString() {
			var sut = new SimpleMergeField( "Field" );

			const string resolveTo = "Bananas";

			var result = sut.Evaluate( new { Field = resolveTo } );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void SimpleMergeFieldWithObject() {
			var sut = new SimpleMergeField( "Field" );

			var resolveTo = new TestObject { MergeField = "Bananas" };

			var result = sut.Evaluate( new { Field = resolveTo } );

			Assert.AreEqual( resolveTo.ToString(), result );
		}

		[TestMethod]
		public void SimpleMergeFieldWithFormattedDate() {
			var format = "MMM dd, yyyy hh:mm";
			var sut = new SimpleMergeField( "this", format );

			var resolveTo = new DateTime(2014, 4, 26, 1, 2, 3);

			var result = sut.Evaluate( resolveTo );

			Assert.AreEqual( resolveTo.ToString(format), result );
		}

		[TestMethod]
		public void SimpleMergeFieldWithInvalidFormatForObject() {
			var sut = new SimpleMergeField( "this", "MMM dd, yyyy hh:mm" );

			var resolveTo = "bananas";

			var result = sut.Evaluate( resolveTo );

			Assert.AreEqual( "bananas", result );
		}

		[TestMethod]
		public void SimpleMergeFieldWithNonexistantField() {
			var sut = new SimpleMergeField( "Field" );

			var result = sut.Evaluate( new { AnotherField = "Bananas" } );

			Assert.AreEqual( "", result );
		}

		#region Supporting Classes

		private class TestObject {
			public object MergeField { get; set; }
			public override string ToString() {
				return "TestClass.ToString()";
			}
		}

		#endregion
	}
}
