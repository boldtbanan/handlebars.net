using System;
using Handlebars.Net.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Handlebars.Net.Test {
	[TestClass]
	public class ObjectExTests {
		[TestMethod]
		public void ObjectExBooleanIsTruthy() {
			Assert.IsTrue( true.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExBooleanIsFalsey() {
			Assert.IsFalse( false.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExIntIsTruthy() {
			Assert.IsTrue( ( ( int ) 1 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExIntIsFalsey() {
			Assert.IsFalse( ( ( int ) 0 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExUIntIsTruthy() {
			Assert.IsTrue( ( ( uint ) 1 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExUIntIsFalsey() {
			Assert.IsFalse( ( ( uint ) 0 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExLongIsTruthy() {
			Assert.IsTrue( 1L.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExULongIsFalsey() {
			Assert.IsFalse( 0UL.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExULongIsTruthy() {
			Assert.IsTrue( 1UL.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExLongIsFalsey() {
			Assert.IsFalse( 0L.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExShortIsTruthy() {
			Assert.IsTrue( ( ( short ) 1 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExShortIsFalsey() {
			Assert.IsFalse( ( ( short ) 0 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExUShortIsTruthy() {
			Assert.IsTrue( ( ( ushort ) 1 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExUShortIsFalsey() {
			Assert.IsFalse( ( ( ushort ) 0 ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExDecimalIsTruthy() {
			Assert.IsTrue( 1M.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExDecimalIsFalsey() {
			Assert.IsFalse( 0M.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExFloatIsTruthy() {
			Assert.IsTrue( 1F.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExFloatIsFalseyAsZero() {
			Assert.IsFalse( 0F.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExFloatIsFalseyAsNaN() {
			Assert.IsFalse( float.NaN.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExDoubleIsTruthy() {
			Assert.IsTrue( 1D.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExDoubleIsFalseyAsZero() {
			Assert.IsFalse( 0D.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExDoubleIsFalseyAsNaN() {
			Assert.IsFalse( double.NaN.IsTruthy() );
		}

		[TestMethod]
		public void ObjectExStringIsTruthy() {
			Assert.IsTrue( "false".IsTruthy() );
		}

		[TestMethod]
		public void ObjectExStringIsFalsey() {
			Assert.IsFalse( "".IsTruthy() );
		}

		[TestMethod]
		public void ObjectExNullIsFalsey() {
			Assert.IsFalse( ( ( string ) null ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExIEnumerableIsTruthy() {
			Assert.IsTrue( ( new[] { 0 } ).IsTruthy() );
		}

		[TestMethod]
		public void ObjectExIEnumerableIsFalsey() {
			Assert.IsFalse( ( new int[0] ).IsTruthy() );
		}
	}
}
