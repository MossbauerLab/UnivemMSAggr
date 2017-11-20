using System;

namespace MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents
{
    public class Doublet
    {
        public Doublet()
        {
        }

        public Doublet(Decimal lineWidth, Decimal lineWidthPrecision,
                      Decimal isomerShift, Decimal isomerShiftPrecision,
                      Decimal quadrupolSplitting, Decimal quadrupolSplittingPrecision,
                      Decimal relativeArea, Decimal relativeAreaPrecision)
        {
            LineWidth = lineWidth;
            LineWidthPrecision = lineWidthPrecision;
            IsomerShift = isomerShift;
            IsomerShiftPrecision = isomerShiftPrecision;
            QuadrupolSplitting = quadrupolSplitting;
            QuadrupolSplittingPrecision = quadrupolSplittingPrecision;
            RelativeArea = relativeArea;
            RelativeAreaPrecision = relativeAreaPrecision;
        }

        // Hyperfine parameters
        public Decimal LineWidth { get; set; }
        public Decimal LineWidthPrecision { get; set; }
        public Decimal IsomerShift { get; set; }
        public Decimal IsomerShiftPrecision { get; set; }
        public Decimal QuadrupolSplitting { get; set; }
        public Decimal QuadrupolSplittingPrecision { get; set; }
        // Area
        public Decimal RelativeArea { get; set; }
        public Decimal RelativeAreaPrecision { get; set; }
    }
}
