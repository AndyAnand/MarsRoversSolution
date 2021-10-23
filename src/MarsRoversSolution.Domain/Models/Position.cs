using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Models
{
    /// <summary>
    /// A position on Mars given by positive East and North coordinates (equivalent to
    /// the first quadrant of Cartesian Plane)
    /// </summary>
    public class Position
    {
        public Position(int eastUnits, int northUnits)
        {
            EastUnits = Guard.Against.Negative(eastUnits, nameof(eastUnits));
            NorthUnits = Guard.Against.Negative(northUnits, nameof(northUnits));
        }

        private int _eastUnits;
        public int EastUnits
        {
            get
            {
                return _eastUnits;
            }
            set
            {
                _eastUnits = Guard.Against.Negative(value, nameof(value));
            }
        }

        private int _northUnits;
        public int NorthUnits
        {
            get
            {
                return _northUnits;
            }
            set
            {
                _northUnits = Guard.Against.Negative(value, nameof(value));
            }
        }

        public override string ToString() => $"{EastUnits} {NorthUnits}";
    }
}
