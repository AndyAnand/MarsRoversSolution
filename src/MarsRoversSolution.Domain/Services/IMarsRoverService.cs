using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Services
{
    public interface IMarsRoverService
    {
        string ProcessCommands(MarsRover rover, string commands);
    }
}
