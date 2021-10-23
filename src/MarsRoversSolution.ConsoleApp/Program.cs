using Ardalis.GuardClauses;
using MarsRoversSolution.ConsoleApp.Helpers;
using MarsRoversSolution.Domain.Models;
using MarsRoversSolution.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversSolution.ConsoleApp
{
    class Program
    {
        private const string InputFilePath = "Input\\Input.txt";
        static void Main(string[] args)
        {
            // Getting input content from a Resource File. If one day this file gets huge in size,
            // a better approach would be reading it line by line from a folder, parsing its contents on demand.
            var marsRoversInputFileContent = File.ReadAllText(InputFilePath);

            Guard.Against.NullOrWhiteSpace(marsRoversInputFileContent,
                nameof(marsRoversInputFileContent));

            var marsRoverService = new MarsRoverService();

            foreach(var roverCase in MarsRoversCasesExtractor.Extract(marsRoversInputFileContent))
            {
                var rover = new MarsRover(roverCase.Terrain, roverCase.Position, roverCase.Heading);
                var roverOutput = marsRoverService.ProcessCommands(rover, roverCase.Commands);
                Console.WriteLine(roverOutput);
            }
        }
    }
}
