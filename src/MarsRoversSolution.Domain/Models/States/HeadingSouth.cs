using Ardalis.GuardClauses;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Models.States
{
    internal class HeadingSouthState : IRoverState
    {
        public HeadingSouthState(MarsRover marsRover)
        {
            Rover = Guard.Against.Null(marsRover, nameof(marsRover));
        }

        private MarsRover _rover;
        public MarsRover Rover
        {
            get => _rover;
            set => _rover = value;
        }

        public Heading Heading => Heading.South;

        public void Move()
        {
            var newPosition = new Position(Rover.Position.EastUnits, Rover.Position.NorthUnits - 1);

            // Does nothing if the rover is about to trepass the mars terrain limits
            if (!Rover.Terrain.ContainsPosition(newPosition))
                return;

            --Rover.Position.NorthUnits;
        }

        public void Rotate(Direction direction)
        {
            IRoverState newRoverState;

            // The rover enters in another state, changing its heading and its move behaviour 
            // next time it moves
            if (direction == Direction.Left)
            {
                newRoverState = new HeadingEastState(Rover);
            }
            else
            {
                newRoverState = new HeadingWestState(Rover);
            }

            Rover.RoverState = newRoverState;
        }
    }
}
