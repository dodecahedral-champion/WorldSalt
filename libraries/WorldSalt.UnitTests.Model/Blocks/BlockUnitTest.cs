namespace WorldSalt.UnitTests.Model {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Model.Blocks;
	using WorldSalt.Model.Values;

	[TestFixture]
	public class BlockUnitTest {
		[Test]
		public void ShouldAssignConstructorValues() {
			var expectedType = Guid.NewGuid();
			var expectedSubtype = (byte)0x85;
			var expectedOrientation = new BlockOrientation { ForwardFace = BlockFace.Top, Rotation = BlockAxisRotation.Roll270 };
			var expectedState = MockRepository.GenerateMock<IBlockState>();

			var target = new Block(expectedType, expectedSubtype, expectedOrientation, expectedState);

			Assert.AreEqual(expectedType, target.Type);
			Assert.AreEqual(expectedSubtype, target.SubType);
			Assert.AreEqual(expectedOrientation, target.Orientation);
			Assert.AreSame(expectedState, target.State);
		}

		[Test]
		public void ShouldConstructWithDefaultSubtype() {
			var expectedType = Guid.NewGuid();

			var target = new Block(expectedType);

			Assert.AreEqual(expectedType, target.Type);
			Assert.AreEqual((byte)0x00, target.SubType);
			Assert.AreEqual(default(BlockOrientation), target.Orientation);
			Assert.IsNull(target.State);
		}

		[Test]
		public void ShouldConstructWithDefaultOrientationAndState() {
			var expectedType = Guid.NewGuid();
			var expectedSubtype = (byte)0x32;

			var target = new Block(expectedType, expectedSubtype);

			Assert.AreEqual(expectedType, target.Type);
			Assert.AreEqual(expectedSubtype, target.SubType);
			Assert.AreEqual(default(BlockOrientation), target.Orientation);
			Assert.IsNull(target.State);
		}
	}
}
