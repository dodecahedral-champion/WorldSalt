namespace WorldSalt.UnitTests.Model {
	using System;
	using NUnit.Framework;
	using Rhino.Mocks;
	using WorldSalt.Model;
	using WorldSalt.Model.Entities;
	using WorldSalt.Model.Grids;
	using WorldSalt.Model.Values;

	[TestFixture]
	public class GriddedEntityUnitTest {
		private IGridFactory gridFactory;

		[SetUp]
		public void Setup() {
			gridFactory = MockRepository.GenerateMock<IGridFactory>();
		}

		[Test]
		public void ShouldAssignConstructorValues() {
			var expectedGrid = MockRepository.GenerateMock<IGrid>();
			gridFactory.Expect(x => x.Create()).Return(expectedGrid);
			var expectedParent = MockRepository.GenerateMock<IEntity>();
			var expectedPlacement = new Placement { Orientation = new Orientation { Pitch = 10, Roll = 20, Yaw = 30 }, Position = new Position { X = 11, Y = 21, Z = 31 } };
			var expectedVelocity = new Velocity { X = 12, Y = 22, Z = 32 };

			var target = new GriddedEntity(gridFactory, expectedParent, expectedPlacement, expectedVelocity);

			Assert.AreSame(expectedGrid, target.Grid);
			Assert.AreSame(expectedParent, target.Parent);
			Assert.AreEqual(expectedPlacement, target.Placement);
			Assert.AreEqual(expectedVelocity, target.Velocity);
			gridFactory.VerifyAllExpectations();
		}

		[Test]
		public void ShouldConstructWithDefaultNullParent() {
			var expectedGrid = MockRepository.GenerateMock<IGrid>();
			gridFactory.Expect(x => x.Create()).Return(expectedGrid);
			var expectedPlacement = new Placement { Orientation = new Orientation { Pitch = 10, Roll = 20, Yaw = 30 }, Position = new Position { X = 11, Y = 21, Z = 31 } };
			var expectedVelocity = new Velocity { X = 12, Y = 22, Z = 32 };

			var target = new GriddedEntity(gridFactory, expectedPlacement, expectedVelocity);

			Assert.AreSame(expectedGrid, target.Grid);
			Assert.IsNull(target.Parent);
			Assert.AreEqual(expectedPlacement, target.Placement);
			Assert.AreEqual(expectedVelocity, target.Velocity);
			gridFactory.VerifyAllExpectations();
		}

		[Test]
		public void ShouldConstructWithDefaultZeroVelocity() {
			var expectedGrid = MockRepository.GenerateMock<IGrid>();
			gridFactory.Expect(x => x.Create()).Return(expectedGrid);
			var expectedPlacement = new Placement { Orientation = new Orientation { Pitch = 10, Roll = 20, Yaw = 30 }, Position = new Position { X = 11, Y = 21, Z = 31 } };

			var target = new GriddedEntity(gridFactory, expectedPlacement);

			Assert.AreSame(expectedGrid, target.Grid);
			Assert.IsNull(target.Parent);
			Assert.AreEqual(expectedPlacement, target.Placement);
			Assert.AreEqual(default(Velocity), target.Velocity);
			gridFactory.VerifyAllExpectations();
		}
	}
}
