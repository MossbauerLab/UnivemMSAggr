using System;

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

        public ComponentsInfo(Decimal chiSquareValue, Decimal velocityStep, Decimal hyperfimeFieldPerMmS, UInt16 channelsNumber)
        {
            ChannelsNumber = channelsNumber;
            ChiSquareValue = chiSquareValue;
            VelocityStep = velocityStep;
            HyperfineFieldPerMmS = hyperfimeFieldPerMmS;
        }

        public UInt16 ChannelsNumber { get; set; }
        public Decimal ChiSquareValue { get; set; }
        public Decimal VelocityStep { get; set; }
        public Decimal HyperfineFieldPerMmS { get; set; }
    }
}
