using MarsRoversSolution.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarsRoversSolution.Tests
{
    public class PositionTests
    {
        [Fact]
        public void CreatingWithPositiveCoordinatesShouldBeValid()
        {
            var eastUnits = 5;
            var northUnits = 5;
            var position = new Position(eastUnits, northUnits);

            Assert.True(position.EastUnits == eastUnits);
            Assert.True(position.NorthUnits == northUnits);
        }

        [Fact]
        public void CreatingWithNegativeCoordinatesShouldThrowException()
        {
            var eastUnits = -5;
            var northUnits = 5;

            Assert.Throws<ArgumentException>(() => new Position(eastUnits, northUnits));

            northUnits = -5;
            eastUnits = 5;
            Assert.Throws<ArgumentException>(() => new Position(eastUnits, northUnits));

            northUnits = -5;
            eastUnits = -5;
            Assert.Throws<ArgumentException>(() => new Position(eastUnits, northUnits));
        }
    }
}
