using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversSolution.Tests
{
    public class MarsTerrainTests
    {
        [Fact]
        public void CreatingWithPositiveDimensionsShouldBeValid()
        {
            var width = 5;
            var heigth = 5;
            var marsTerrain = new MarsTerrain(width, heigth);

            Assert.True(marsTerrain.Width == width);
            Assert.True(marsTerrain.Height == heigth);
        }

        [Fact]
        public void CreatingWithNegativeDimensionsShouldThrowException()
        {
            var width = -5;
            var heigth = 5;

            Assert.Throws<ArgumentException>(() => new Position(width, heigth));

            width = -5;
            heigth = 5;
            Assert.Throws<ArgumentException>(() => new Position(width, heigth));

            width = -5;
            heigth = -5;
            Assert.Throws<ArgumentException>(() => new Position(width, heigth));
        }

        [Fact]
        public void PositionIsInsideTerrain()
        {
            var width = 5;
            var heigth = 5;

            var terrain = new MarsTerrain(width, heigth);

            Assert.True(terrain.ContainsPosition(new Position(0, 0)));
            Assert.True(terrain.ContainsPosition(new Position(5, 5)));
            Assert.True(terrain.ContainsPosition(new Position(1, 5)));
            Assert.True(terrain.ContainsPosition(new Position(5, 1)));
        }

        [Fact]
        public void PositionIsNotInsideTerrain()
        {
            var width = 5;
            var heigth = 5;

            var terrain = new MarsTerrain(width, heigth);

            Assert.False(terrain.ContainsPosition(new Position(6, 5)));
            Assert.False(terrain.ContainsPosition(new Position(5, 6)));
            Assert.False(terrain.ContainsPosition(new Position(6, 6)));
        }
    }
}
