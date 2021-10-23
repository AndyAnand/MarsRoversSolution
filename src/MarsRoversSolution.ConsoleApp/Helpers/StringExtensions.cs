using Ardalis.GuardClauses;
using MarsRoversSolution.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MarsRoversSolution.ConsoleApp.Helpers
{
    public static class StringExtensions
    {
        private static readonly Dictionary<string, Heading> _stringToHeadingMapper
            = new()
            {
                { "N", Heading.North },
                { "W", Heading.West },
                { "S", Heading.South },
                { "E", Heading.East }
            };

        public static Heading ToHeading(this string headingString)
        {
            Guard.Against.NullOrWhiteSpace(headingString, nameof(headingString));

            if (!_stringToHeadingMapper.ContainsKey(headingString))
                throw new ArgumentException($"'{headingString}' is not a valid Heading");

            return _stringToHeadingMapper[headingString];
        }
    }
}
