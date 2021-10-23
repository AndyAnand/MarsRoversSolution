using Ardalis.GuardClauses;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Interfaces;

namespace MarsRoversSolution.Domain.Models.States
{
    internal class HeadingNorthState : IRoverState
    {
        public HeadingNorthState(MarsRover marsRover)
        {
            Rover = Guard.Against.Null(marsRover, nameof(marsRover));
        }

        private MarsRover _rover;
        public MarsRover Rover
        {
            get => _rover;
            set => _rover = value;
        }

        public Heading Heading => Heading.North;

        public void Move()
        {
            var newPosition = new Position(Rover.Position.EastUnits, Rover.Position.NorthUnits + 1);

            // Does nothing if the rover is about to trepass the mars terrain limits
            if (!Rover.Terrain.ContainsPosition(newPosition))
                return;

            ++Rover.Position.NorthUnits;
        }

        public void Rotate(Direction direction)
        {
            IRoverState newRoverState;

            if (direction == Direction.Left)
            {
                newRoverState = new HeadingWestState(Rover);
            }
            else
            {
                newRoverState = new HeadingEastState(Rover);
            }

            Rover.RoverState = newRoverState;
        }
    }
}
