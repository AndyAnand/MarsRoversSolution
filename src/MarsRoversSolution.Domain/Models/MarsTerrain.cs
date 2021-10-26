using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRoversSolution.Domain.Models
{
    /// <summary>
    /// Will delimit the area where a rover can move
    /// </summary>
    public class MarsTerrain
    {
        public MarsTerrain(int width, int height)
        {
            Height = Guard.Against.NegativeOrZero(height, nameof(height));
            Width = Guard.Against.NegativeOrZero(width, nameof(width));
        }

        public int Height { get; private set; }
        public int Width { get; private set; }

        public bool ContainsPosition(Position position)
            => position != null
            && position.EastUnits <= Width
            && position.EastUnits >= 0
            && position.NorthUnits <= Height
            && position.NorthUnits >= 0;

        public override string ToString() 
            => $"{Width} {Height}";
    }
}
