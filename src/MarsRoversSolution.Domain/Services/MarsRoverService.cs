using Ardalis.GuardClauses;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Services
{
    public class MarsRoverService : IMarsRoverService
    {
        private const char RotateLeftCommand = 'L';
        private const char RotateRightCommand = 'R';
        private const char MoveCommand = 'M';

        public string ProcessCommands(MarsRover rover, string commands)
        {
            Guard.Against.Null(rover, nameof(rover));
            Guard.Against.NullOrWhiteSpace(commands, nameof(commands));

            foreach(var command in commands)
            {
                if (command == RotateLeftCommand)
                {
                    rover.Rotate(Direction.Left);
                }
                else if (command == RotateRightCommand)
                {
                    rover.Rotate(Direction.Right);
                }
                else if (command == MoveCommand)
                {
                    rover.Move();
                }
                else
                {
                    throw new ArgumentException($"{command} is an invalid command for a Mars Rover");
                }
            }

            return rover.ToString();
        }
    }
}
