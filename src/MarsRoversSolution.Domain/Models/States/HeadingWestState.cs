using Ardalis.GuardClauses;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Models.States
{
    internal class HeadingWestState : IRoverState
    {
        public HeadingWestState(MarsRover marsRover)
        {
            Rover = Guard.Against.Null(marsRover, nameof(marsRover));
        }

        private MarsRover _rover;
        public MarsRover Rover
        {
            get => _rover;
            set => _rover = value;
        }

        public Heading Heading => Heading.West;

        public void Move()
        {
            var newPosition = new Position(Rover.Position.EastUnits - 1, Rover.Position.NorthUnits);

            // Does nothing if the rover is about to trepass the mars terrain limits
            if (!Rover.Terrain.ContainsPosition(newPosition))
                return;

            --Rover.Position.EastUnits;
        }

        public void Rotate(Direction direction)
        {
            IRoverState newRoverState;

            // The rover enters in another state, changing its heading and its move behaviour 
            // next time it moves
            if (direction == Direction.Left)
            {
                newRoverState = new HeadingSouthState(Rover);
            }
            else
            {
                newRoverState = new HeadingNorthState(Rover);
            }

            Rover.RoverState = newRoverState;
        }
    }
}
