using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Models;
using MarsRoversSolution.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversSolution.Tests
{
    public class MarsRoverServiceTests
    {
        [Fact]
        public void DoesntAcceptInvalidCommands()
        {
            var initialHeading = Heading.North;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 0;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);
            string commands = "XLMS";

            var marsRoverService = new MarsRoverService();

            Assert.Throws<ArgumentException>(() 
                => marsRoverService.ProcessCommands(rover, commands));
        }

        [Fact]
        public void AcceptValidCommands()
        {
            var initialHeading = Heading.North;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 2;
            var initialNorthUnits = 0;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);
            string commands = "LRMMMMML";

            var marsRoverService = new MarsRoverService();

            Assert.NotNull(marsRoverService.ProcessCommands(rover, commands));
        }

        /// <summary>
        /// Reproduces the first input case of the Mars Rovers challenge documentation
        /// </summary>
        [Fact]
        public void ProcessCommandsFirstOutputIsRight()
        {
            var initialHeading = Heading.North;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 1;
            var initialNorthUnits = 2;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            var marsRoverService = new MarsRoverService();
            var marsRoversChallengeFirstTestCaseExpectedOutput = "1 3 N";

            Assert.Equal(marsRoversChallengeFirstTestCaseExpectedOutput, 
                marsRoverService.ProcessCommands(rover, "LMLMLMLMM"));
        }

        /// <summary>
        /// Reproduces the first input case of the Mars Rovers challenge documentation
        /// </summary>
        [Fact]
        public void ProcessCommandsSecondOutputIsRight()
        {
            var initialHeading = Heading.East;
            var marsTerrain = new MarsTerrain(5, 5);
            var initialEastUnits = 3;
            var initialNorthUnits = 3;
            var roverPosition = new Position(initialEastUnits, initialNorthUnits);
            var rover = new MarsRover(marsTerrain, roverPosition, initialHeading);

            var marsRoverService = new MarsRoverService();
            var marsRoversChallengeSecondTestCaseExpectedOutput = "5 1 E";

            Assert.Equal(marsRoversChallengeSecondTestCaseExpectedOutput, 
                marsRoverService.ProcessCommands(rover, "MMRMMRMRRM"));
        }
    }
}
