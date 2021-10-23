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
        public void TerrainIsRequired()
        {
            var hoverPosition = new Position(2,3);
            MarsTerrain nullTerrain = null;

            Assert.Throws<ArgumentNullException>(()
                => new MarsRover(nullTerrain, hoverPosition, Domain.Enums.Heading.East));
        }

        /// <summary>
        /// Guarantees that a hover always have a RoverState for each Heading possible, 
        /// which is necessary for moving and rotating correctly.
        /// </summary>
        [Fact]
        public void ShouldHaveAlwaysAStateForEachHeadingValue()
        {
            var allHeadingValues = Enums.GetValues<Heading>();
            var marsTerrain = new MarsTerrain(5, 5);
            var hoverPosition = new Position(2, 3);

            foreach (var heading in allHeadingValues)
            {
                var newHover = new MarsRover(marsTerrain, hoverPosition, heading);

                Assert.NotNull(newHover.RoverState);
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
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);

            //Act
            hover.Move();
            hover.Move();
            hover.Move();

            //Assert
            // Hover should continue facing north
            Assert.True(hover.Heading == initialHeading);
            // Hover should not move in the East coordinate
            Assert.True(hover.Position.EastUnits == initialEastUnits);
            // Hover should move +3 units in the North coordinate
            Assert.True(hover.Position.NorthUnits == initialNorthUnits + 3);
        }

        [Fact]
        public void CanMoveProperlyFacingSouth()
        {
            //Arrange
            var initialHeading = Heading.South;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);

            //Act
            hover.Move();
            hover.Move();
            hover.Move();

            //Assert
            // Hover should continue facing south
            Assert.True(hover.Heading == initialHeading);
            // Hover should not move in the East coordinate
            Assert.True(hover.Position.EastUnits == initialEastUnits);
            // Hover should move -3 units in the North coordinate
            Assert.True(hover.Position.NorthUnits == initialNorthUnits - 3);
        }

        [Fact]
        public void CanMoveProperlyFacingEast()
        {
            //Arrange
            var initialHeading = Heading.East;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);

            //Act
            hover.Move();
            hover.Move();
            hover.Move();

            //Assert
            // Hover should continue facing east
            Assert.True(hover.Heading == initialHeading);
            // Hover should move +3 in the East coordinate
            Assert.True(hover.Position.EastUnits == initialEastUnits + 3);
            // Hover should not move in the North coordinate
            Assert.True(hover.Position.NorthUnits == initialNorthUnits);
        }

        [Fact]
        public void CanMoveProperlyFacingWest()
        {
            //Arrange
            var initialHeading = Heading.West;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);

            //Act
            hover.Move();
            hover.Move();

            //Assert
            // Hover should continue facing east
            Assert.True(hover.Heading == initialHeading);
            // Hover should move -2 in the East coordinate
            Assert.True(hover.Position.EastUnits == initialEastUnits - 2);
            // Hover should not move in the North coordinate
            Assert.True(hover.Position.NorthUnits == initialNorthUnits);
        }

        [Fact]
        public void CanRotateProperly()
        {
            var initialHeading = Heading.West;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 5;
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);

            #region Rotating counter clockwise

            hover.Rotate(Direction.Left);
            // West + Left = South
            Assert.True(hover.Heading == Heading.South);

            hover.Rotate(Direction.Left);
            // South + Left = East
            Assert.True(hover.Heading == Heading.East);

            hover.Rotate(Direction.Left);
            // East + Left = North
            Assert.True(hover.Heading == Heading.North);

            hover.Rotate(Direction.Left);
            // North + Left = North
            Assert.True(hover.Heading == Heading.West);

            #endregion

            #region Rotating clockwise

            hover.Rotate(Direction.Right);
            // West + Right = North
            Assert.True(hover.Heading == Heading.North);

            hover.Rotate(Direction.Right);
            // North + Right = East
            Assert.True(hover.Heading == Heading.East);

            hover.Rotate(Direction.Right);
            // East + Right = South
            Assert.True(hover.Heading == Heading.South);

            hover.Rotate(Direction.Right);
            // South + Right = West
            Assert.True(hover.Heading == Heading.West);

            #endregion

            // Hover should move at all
            Assert.True(hover.Position.EastUnits == initialEastUnits);
            Assert.True(hover.Position.NorthUnits == initialNorthUnits);
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
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);
            var finalPosition = new Position(1,3);
            var finalHeading = Heading.North;

            // Commands: LMLMLMLMM
            hover.Rotate(Direction.Left);
            hover.Move();
            hover.Rotate(Direction.Left);
            hover.Move();
            hover.Rotate(Direction.Left);
            hover.Move();
            hover.Rotate(Direction.Left);
            hover.Move();
            hover.Move();

            Assert.True(hover.Position.EastUnits == finalPosition.EastUnits);
            Assert.True(hover.Position.NorthUnits == finalPosition.NorthUnits);
            Assert.True(hover.Heading == finalHeading);
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
            var hoverPosition = new Position(initialEastUnits, initialNorthUnits);
            var hover = new MarsRover(marsTerrain, hoverPosition, initialHeading);
            var finalPosition = new Position(5, 1);
            var finalHeading = Heading.East;

            // Commands: MMRMMRMRRM
            hover.Move();
            hover.Move();
            hover.Rotate(Direction.Right);
            hover.Move();
            hover.Move();
            hover.Rotate(Direction.Right);
            hover.Move();
            hover.Rotate(Direction.Right);
            hover.Rotate(Direction.Right);
            hover.Move();

            Assert.True(hover.Position.EastUnits == finalPosition.EastUnits);
            Assert.True(hover.Position.NorthUnits == finalPosition.NorthUnits);
            Assert.True(hover.Heading == finalHeading);
        }
    }
}
