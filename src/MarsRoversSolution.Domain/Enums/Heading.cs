using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MarsRoversSolution.Domain.Enums
{
    public enum Heading
    {
        [Description("N")]
        North,
        [Description("E")]
        East,
        [Description("S")]
        South,
        [Description("W")]
        West
    }
}
