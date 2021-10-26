using EnumsNET;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Interfaces;
using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversSolution.Tests
{
    public class MarsRoverTests
    {
        [Fact]
        public void PositionIsRequired()
        {
            Position nullPosition = null;
            var marsTerrain = new MarsTerrain(5, 5);

            Assert.Throws<ArgumentNullException>(() 
                => new MarsRover(marsTerrain, nullPosition, Domain.Enums.Heading.East));
        }

        [Fact]
        public void InitialPositionShouldBeInsideTerrain()
        {
            Position outOfBoundsPosition = new Position(7, 42);
            var marsTerrain = new MarsTerrain(5, 5);

            Assert.Throws<ArgumentException>(()
                => new MarsRover(marsTerrain, outOfBoundsPosition, Domain.Enums.Heading.East));
        }

        [Fact]
        public void TerrainIsRequired()
        {
            var roverPosition = new Position(2,3);
            MarsTerrain nullTerrain = null;

            Assert.Throws<ArgumentNullException>(()
                => new MarsRover(nullTerrain, roverPosition, Domain.Enums.Heading.East));
        }

        /// <summary>
        /// Guarantees that a rover always have a RoverState for each Heading possible, 
        /// which is necessary for moving and rotating correctly.
        /// </summary>
        [Fact]
        public void ShouldHaveAlwaysAStateForEachHeadingValue()
        {
            var allHeadingValues = Enums.GetValues<Heading>();
            var marsTerrain = new MarsTerrain(5, 5);
            var roverPosition = new Position(2, 3);

            foreach (var heading in allHeadingValues)
            {
                var newRover = new MarsRover(marsTerrain, roverPosition, heading);

                Assert.NotNull(newRover.RoverState);
            }
        }

        [Fact]
        public void CanMoveProperlyFacingNorth()
        {
            //Arrange
            var initialHeading = Heading.North;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 0;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            //Act
            rover.Move();
            rover.Move();
            rover.Move();

            //Assert
            // Rover should continue facing north
            Assert.True(rover.Heading == initialHeading);
            // Rover should not move in the East coordinate
            Assert.True(rover.Position.EastUnits == initialEastUnits);
            // Rover should move +3 units in the North coordinate
            Assert.True(rover.Position.NorthUnits == initialNorthUnits + 3);
        }

        [Fact]
        public void CanMoveProperlyFacingSouth()
        {
            //Arrange
            var initialHeading = Heading.South;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            //Act
            rover.Move();
            rover.Move();
            rover.Move();

            //Assert
            // Rover should continue facing south
            Assert.True(rover.Heading == initialHeading);
            // Rover should not move in the East coordinate
            Assert.True(rover.Position.EastUnits == initialEastUnits);
            // Rover should move -3 units in the North coordinate
            Assert.True(rover.Position.NorthUnits == initialNorthUnits - 3);
        }

        [Fact]
        public void CanMoveProperlyFacingEast()
        {
            //Arrange
            var initialHeading = Heading.East;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            //Act
            rover.Move();
            rover.Move();
            rover.Move();

            //Assert
            // Rover should continue facing east
            Assert.True(rover.Heading == initialHeading);
            // Rover should move +3 in the East coordinate
            Assert.True(rover.Position.EastUnits == initialEastUnits + 3);
            // Rover should not move in the North coordinate
            Assert.True(rover.Position.NorthUnits == initialNorthUnits);
        }

        [Fact]
        public void CanMoveProperlyFacingWest()
        {
            //Arrange
            var initialHeading = Heading.West;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            //Act
            rover.Move();
            rover.Move();

            //Assert
            // Rover should continue facing east
            Assert.True(rover.Heading == initialHeading);
            // Rover should move -2 in the East coordinate
            Assert.True(rover.Position.EastUnits == initialEastUnits - 2);
            // Rover should not move in the North coordinate
            Assert.True(rover.Position.NorthUnits == initialNorthUnits);
        }

        [Fact]
        public void CanRotateProperly()
        {
            var initialHeading = Heading.West;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            #region Rotating counter clockwise

            rover.Rotate(Direction.Left);
            // West + Left = South
            Assert.True(rover.Heading == Heading.South);

            rover.Rotate(Direction.Left);
            // South + Left = East
            Assert.True(rover.Heading == Heading.East);

            rover.Rotate(Direction.Left);
            // East + Left = North
            Assert.True(rover.Heading == Heading.North);

            rover.Rotate(Direction.Left);
            // North + Left = North
            Assert.True(rover.Heading == Heading.West);

            #endregion

            #region Rotating clockwise

            rover.Rotate(Direction.Right);
            // West + Right = North
            Assert.True(rover.Heading == Heading.North);

            rover.Rotate(Direction.Right);
            // North + Right = East
            Assert.True(rover.Heading == Heading.East);

            rover.Rotate(Direction.Right);
            // East + Right = South
            Assert.True(rover.Heading == Heading.South);

            rover.Rotate(Direction.Right);
            // South + Right = West
            Assert.True(rover.Heading == Heading.West);

            #endregion

            // Rover should move at all
            Assert.True(rover.Position.EastUnits == initialEastUnits);
            Assert.True(rover.Position.NorthUnits == initialNorthUnits);
        }

        /// <summary>
        /// Translates into the first input case of the Mars Rovers problem documentation
        /// </summary>
        [Fact]
        public void ShouldMoveToADeterminedPositionAndHeading()
        {
            var initialHeading = Heading.North;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 1;
            var initialNorthUnits = 2;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);
            var finalPosition = new Position(1,3);
            var finalHeading = Heading.North;

            // Commands: LMLMLMLMM
            rover.Rotate(Direction.Left);
            rover.Move();
            rover.Rotate(Direction.Left);
            rover.Move();
            rover.Rotate(Direction.Left);
            rover.Move();
            rover.Rotate(Direction.Left);
            rover.Move();
            rover.Move();

            Assert.True(rover.Position.EastUnits == finalPosition.EastUnits);
            Assert.True(rover.Position.NorthUnits == finalPosition.NorthUnits);
            Assert.True(rover.Heading == finalHeading);
        }

        /// <summary>
        /// Translates into the second input case of the Mars Rovers problem documentation
        /// </summary>
        [Fact]
        public void ShouldMoveToAnotherDeterminedPositionAndHeading()
        {
            var initialHeading = Heading.East;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 3;
            var initialNorthUnits = 3;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);
            var finalPosition = new Position(5, 1);
            var finalHeading = Heading.East;

            // Commands: MMRMMRMRRM
            rover.Move();
            rover.Move();
            rover.Rotate(Direction.Right);
            rover.Move();
            rover.Move();
            rover.Rotate(Direction.Right);
            rover.Move();
            rover.Rotate(Direction.Right);
            rover.Rotate(Direction.Right);
            rover.Move();

            Assert.True(rover.Position.EastUnits == finalPosition.EastUnits);
            Assert.True(rover.Position.NorthUnits == finalPosition.NorthUnits);
            Assert.True(rover.Heading == finalHeading);
        }
    }
}
