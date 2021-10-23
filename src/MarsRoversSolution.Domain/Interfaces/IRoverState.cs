using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Interfaces
{
    public interface IRoverState
    {
        MarsRover Rover { get; }
        Heading Heading { get; }
        void Move();
        void Rotate(Direction onDirection);
    }
}
