using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Models;

namespace MarsRoversSolution.ConsoleApp.Models
{
    public class MarsRoversCase
    {
        public MarsTerrain Terrain { get; set; }
        public Position Position { get; set; }
        public Heading Heading { get; set; }
        public string Commands { get; set; }
    }
}
