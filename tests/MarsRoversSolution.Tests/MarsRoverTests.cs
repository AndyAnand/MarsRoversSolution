using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversSolution.Tests
{
    public class MarsRoverTests
    {
        [Fact]
        public void PositionIsRequired()
        {
            Position nullPosition = null;
            var marsTerrain = new MarsTerrain(5, 5);

            Assert.Throws<ArgumentNullException>(() 
                => new MarsRover(marsTerrain, nullPosition, Domain.Enums.Heading.East));
        }

        [Fact]
        public void TerrainIsRequired()
        {
            var position = new Position(2,3);
            MarsTerrain nullTerrain = null;

            Assert.Throws<ArgumentNullException>(()
                => new MarsRover(nullTerrain, position, Domain.Enums.Heading.East));
        }

        public void ShouldHaveAlwaysAStateForEachHeadingValue()
        {

        }
    }
}
