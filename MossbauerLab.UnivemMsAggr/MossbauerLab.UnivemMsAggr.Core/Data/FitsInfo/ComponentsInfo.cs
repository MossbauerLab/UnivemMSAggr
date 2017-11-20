using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MossbauerLab.UnivemMsAggr.Core.Data.FitsInfo
{
    /// <summary>
    ///    Bases on *_comp_.txt and *_line*.txt file 
    /// </summary>
    public class ComponentsInfo
    {
        public ComponentsInfo()
        {
        }

        public ComponentsInfo(Decimal chiSquareValue, Decimal velocityStep, Decimal hyperfimeFieldPerMmS)
        {
            ChiSquareValue = chiSquareValue;
            VelocityStep = velocityStep;
            HyperfineFieldPerMmS = hyperfimeFieldPerMmS;
        }

        public Decimal ChiSquareValue { get; set; }
        public Decimal VelocityStep { get; set; }
        public Decimal HyperfineFieldPerMmS { get; set; }
    }
}
