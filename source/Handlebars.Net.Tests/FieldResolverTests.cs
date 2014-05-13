using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class FieldResolverTests {
		private FieldResolver resolver;

		[TestInitialize]
		public void Setup() {
			resolver = new FieldResolver();
		}

		[TestMethod]
		public void FieldResolverResolveThisKeyword() {
			var resolveTo = new { };
			var context = new Stack<object>( new[] { resolveTo } );

			var result = resolver.Resolve( context, "this" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveFromObject() {
			var resolveTo = new { };
			var context = new Stack<object>( new[] {
				new { Field = resolveTo }
			} );

			var result = resolver.Resolve( context, "Field" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveFromDictionary() {
			var resolveTo = new { };
			var context = new Stack<object>( new[] {
				new Dictionary<string, object>{
					{ "Field", resolveTo }
			} } );

			var result = resolver.Resolve( context, "Field" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverUnableToResolve() {
			var context = new Stack<object>( new[] {
				new {Field = 1}
			} );

			var result = resolver.Resolve( context, "AnotherField" );

			Assert.IsNull( result );
		}

		[TestMethod]
		[ExpectedException( typeof( ArgumentNullException ) )]
		public void FieldResolverNullContext() {
			resolver.Resolve( null, "Field" );
		}

		[TestMethod]
		[ExpectedException( typeof( ArgumentException ) )]
		public void FieldResolverEmptyContext() {
			var context = new Stack<object>();

			resolver.Resolve( context, "Field" );
		}

		[TestMethod]
		[ExpectedException( typeof( ArgumentNullException ) )]
		public void FieldResolverNullField() {
			var context = new Stack<object>( new[] {
				new {Field = 1}
			} );

			resolver.Resolve( context, null );
		}

		[TestMethod]
		public void FieldResolverEmptyStringForFieldInDictionary() {
			var resolveTo = new { };
			var context = new Stack<object>( new[] {
				new Dictionary<string, object>{
				{"", resolveTo}
			} } );

			var result = resolver.Resolve( context, "" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverEmptyStringForFieldInObject() {
			var context = new Stack<object>( new[] {
				new { Field = "Field" }
			} );

			var result = resolver.Resolve( context, "" );

			Assert.IsNull( result );
		}

		[TestMethod]
		public void FieldResolverResolveInHigherContext() {
			var resolveTo = new { };

			var context = new Stack<object>( new object[] {
				new { Field = false },
				new { Field = resolveTo }
			} );

			var result = resolver.Resolve( context, "Field" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveInHigherContextWithListContext() {
			var resolveTo = new { };

			var context = new List<object>( new object[] {
				new {Field = resolveTo},
				new {Field = false}
			} );

			var result = resolver.Resolve( context, "Field" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveDotNotationWithNoScopeConflicts() {
			var resolveTo = new { };

			var context = new Stack<object>( new object[] {
				new {
					Field1 = new {
						Field2 = resolveTo
					}
				}
			} );

			var result = resolver.Resolve( context, "Field1.Field2" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveDotNotationWithScopeConflicts() {
			var resolveTo = new { };

			var context = new Stack<object>( new object[] {
				new {
					Field1 = 1
				},
				new {
					Field1 = new {
						Field2 = resolveTo
					}
				}
			} );

			var result = resolver.Resolve( context, "Field1.Field2" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveDotNotationDictionaryWithinObject() {
			var resolveTo = new { };

			var context = new Stack<object>( new object[] {
				new {
					Field1 = new Dictionary<string, object> {
						{ "Field2", resolveTo }
					}
				}
			} );

			var result = resolver.Resolve( context, "Field1.Field2" );

			Assert.AreEqual( resolveTo, result );
		}

		[TestMethod]
		public void FieldResolverResolveDotNotationObjectWithinDictionary() {
			var resolveTo = new { };

			var context = new Stack<object>( new object[] {
				new Dictionary<string, object> {
					{ "Field1", new { Field2 = resolveTo } }
				} 
			} );

			var result = resolver.Resolve( context, "Field1.Field2" );

			Assert.AreEqual( resolveTo, result );
		}
	}
}
