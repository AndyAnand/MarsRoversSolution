using Ardalis.GuardClauses;
using EnumsNET;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Interfaces;
using MarsRoversSolution.Domain.Models.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Models
{
    /// <summary>
    /// Represents the rover itself, a rover can Rotate on one of the possible 4 directions
    /// and can Move on the direction it is facing at the moment, adding or subtracting one
    /// in the North or East units of its current Position
    /// </summary>
    public class MarsRover
    {
        private readonly Dictionary<Heading, IRoverState> _headingToRoverStateMapper;
        public MarsRover(MarsTerrain terrain,
            Position initialPosition, Heading heading)
        {
            Position = Guard.Against.Null(initialPosition, nameof(initialPosition));
            Terrain = Guard.Against.Null(terrain, nameof(terrain));

            _headingToRoverStateMapper = new Dictionary<Heading, IRoverState>()
            {
                { Heading.East, new HeadingEastState(this) },
                { Heading.North, new HeadingNorthState(this) },
                { Heading.South, new HeadingSouthState(this) },
                { Heading.West, new HeadingWestState(this) }
            };

            RoverState = _headingToRoverStateMapper[heading];
        }

        /// <summary>
        /// Had to take a desing pattern decision here, I noticed a rover could do two actions: Rotate and Move
        /// and the Move behaviour is affected by the rover's heading direction, the behaviour of the Rover changes
        /// based on its state. So I implemented the State pattern what avoided me from writting tons of if's in the Rotate
        /// method (and two more if/else if for each new rotate direction we could add (i.e. northwest, southeast, 
        /// northeast, etc). It also saved me from writing a lot of if's to decide on which coordinates of the Position
        /// I should add/subtract
        /// </summary>
        public IRoverState RoverState { get; set; }
        public Position Position { get; private set; }
        public MarsTerrain Terrain { get; private set; }
        public Heading Heading => RoverState.Heading;

        public void Rotate(Direction direction)
        {
            RoverState.Rotate(direction);
        }

        public void Move()
        {
            RoverState.Move();
        }

        public override string ToString()
            => $"{Position} {Heading.AsString(EnumFormat.Description)}";
    }
}
