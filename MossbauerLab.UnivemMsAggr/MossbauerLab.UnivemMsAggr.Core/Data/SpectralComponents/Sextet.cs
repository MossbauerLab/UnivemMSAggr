using System;

namespace MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents
{
    public class Sextet
    {
        public Sextet()
        {
        }

        public Sextet(Decimal lineWidth, Decimal? lineWidthError,
                      Decimal isomerShift, Decimal? isomerShiftError,
                      Decimal quadrupolShift, Decimal? quadrupolShiftError,
                      Decimal hyperfineField, Decimal? hyperfineFieldError,
                      Decimal relativeArea, Decimal? relativeAreaError)
        {
            LineWidth = lineWidth;
            LineWidthError = lineWidthError;
            IsomerShift = isomerShift;
            IsomerShiftError = isomerShiftError;
            QuadrupolShift = quadrupolShift;
            QuadrupolShiftError = quadrupolShiftError;
            HyperfineField = hyperfineField;
            HyperfineFieldError = hyperfineFieldError;
            RelativeArea = relativeArea;
            RelativeAreaError = relativeAreaError;
        }

        // Hyperfine parameters
        public Decimal LineWidth { get; set; }
        public Decimal? LineWidthError { get; set; }
        public Decimal IsomerShift { get; set; }
        public Decimal? IsomerShiftError { get; set; }
        public Decimal QuadrupolShift { get; set; }
        public Decimal? QuadrupolShiftError { get; set; }
        public Decimal HyperfineField { get; set; }
        public Decimal? HyperfineFieldError { get; set; }
        // Area
        public Decimal RelativeArea { get; set; }
        public Decimal? RelativeAreaError { get; set; }
    }
}
