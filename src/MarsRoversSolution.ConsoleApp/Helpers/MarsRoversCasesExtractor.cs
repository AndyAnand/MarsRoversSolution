using Ardalis.GuardClauses;
using MarsRoversSolution.ConsoleApp.Models;
using MarsRoversSolution.Domain.Enums;
using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversSolution.ConsoleApp.Helpers
{
    public static class MarsRoversCasesExtractor
    {
        private const string PositionAndHeadingLineRegex = "\\d+ \\d+ [A-Z]";
        private const string TerrainLineRegex = "\\d+ \\d+";
        private const string CommandsLineRegex = "[LRM]+";
        private const char ValuesSeparator = ' ';
        private const int NumberOfLinesPerRover = 2;

        public static IEnumerable<MarsRoversCase> Extract(string marsRoversInputContent)
        {
            var contentLines = SplitContentByLines(marsRoversInputContent);

            // Besides the terrain dimensions, each pair of new lines will contain the rover's
            // position, heading and commands to execute.
            var totalRoversDataLines = (contentLines.Length - 1);
            var totalRoversInFile = totalRoversDataLines / NumberOfLinesPerRover;

            Guard.Against.NegativeOrZero(totalRoversInFile,
                nameof(totalRoversInFile), $"No rovers were found on {marsRoversInputContent}");

            var marsTerrain = ExtractTerrainFromLine(contentLines[0]);

            for (int roverNumber = 1; roverNumber <= totalRoversDataLines; roverNumber += NumberOfLinesPerRover)
            {
                var position = ExtractPositionFromLine(contentLines[roverNumber]);
                var heading = ExtractHeadingFromLine(contentLines[roverNumber]);
                var commands = ExtractCommandsFromLine(contentLines[roverNumber + 1]);

                yield return new MarsRoversCase
                {
                    Commands = commands,
                    Heading = heading,
                    Position = position,
                    Terrain = marsTerrain  // Same terrain is assigned to one or more rovers in the input file
                };
            }
        }

        private static string[] SplitContentByLines(string content)
        {
            Guard.Against.NullOrWhiteSpace(content, nameof(content));

            return content.Split(Environment.NewLine);
        }

        private static Position ExtractPositionFromLine(string line)
        {
            line = line?.Trim();
            Guard.Against.InvalidFormat(line, nameof(line), PositionAndHeadingLineRegex);

            var lineParts = line.Split(ValuesSeparator);
            var eastUnits = int.Parse(lineParts[0]);
            var northUnits = int.Parse(lineParts[1]);

            return new Position(eastUnits, northUnits);
        }

        private static string ExtractCommandsFromLine(string line)
        {
            line = line?.Trim();
            Guard.Against.InvalidFormat(line, nameof(line), CommandsLineRegex);

            return line;
        }

        private static Heading ExtractHeadingFromLine(string line)
        {
            line = line?.Trim();
            Guard.Against.InvalidFormat(line, nameof(line), PositionAndHeadingLineRegex);

            var lineParts = line.Split(ValuesSeparator);
            var headingString = lineParts[2];

            return headingString.ToHeading();
        }

        private static MarsTerrain ExtractTerrainFromLine(string line)
        {
            line = line?.Trim();
            Guard.Against.InvalidFormat(line, nameof(line), TerrainLineRegex);

            var lineParts = line.Split(ValuesSeparator);
            var width = int.Parse(lineParts[0]);
            var height = int.Parse(lineParts[1]);

            return new MarsTerrain(width, height);
        }
    }
}
