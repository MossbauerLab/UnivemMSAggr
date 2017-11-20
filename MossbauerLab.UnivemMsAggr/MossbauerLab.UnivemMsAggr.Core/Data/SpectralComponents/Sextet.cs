using System;

namespace MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents
{
    public class Sextet
    {
        public Sextet()
        {
        }

        public Sextet(Decimal lineWidth, Decimal lineWidthPrecision,
                      Decimal isomerShift, Decimal isomerShiftPrecision,
                      Decimal quadrupolShift, Decimal quadrupolShiftPrecision,
                      Decimal hyperfineField, Decimal hyperfineFieldPrecision,
                      Decimal relativeArea, Decimal relativeAreaPrecision)
        {
            LineWidth = lineWidth;
            LineWidthPrecision = lineWidthPrecision;
            IsomerShift = isomerShift;
            IsomerShiftPrecision = isomerShiftPrecision;
            QuadrupolShift = quadrupolShift;
            QuadrupolShiftPrecision = quadrupolShiftPrecision;
            HyperfineField = hyperfineField;
            HyperfineFieldPrecision = hyperfineFieldPrecision;
            RelativeArea = relativeArea;
            RelativeAreaPrecision = relativeAreaPrecision;
        }

        // Hyperfine parameters
        public Decimal LineWidth { get; set; }
        public Decimal LineWidthPrecision { get; set; }
        public Decimal IsomerShift { get; set; }
        public Decimal IsomerShiftPrecision { get; set; }
        public Decimal QuadrupolShift { get; set; }
        public Decimal QuadrupolShiftPrecision { get; set; }
        public Decimal HyperfineField { get; set; }
        public Decimal HyperfineFieldPrecision { get; set; }
        // Area
        public Decimal RelativeArea { get; set; }
        public Decimal RelativeAreaPrecision { get; set; }
    }
}
