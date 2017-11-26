using System;

namespace MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents
{
    public class Doublet
    {
        public Doublet()
        {
        }

        public Doublet(Decimal lineWidth, Decimal? lineWidthError,
                      Decimal isomerShift, Decimal? isomerShiftError,
                      Decimal quadrupolSplitting, Decimal? quadrupolSplittingError,
                      Decimal relativeArea, Decimal? relativeAreaError)
        {
            LineWidth = lineWidth;
            LineWidthError = lineWidthError;
            IsomerShift = isomerShift;
            IsomerShiftError = isomerShiftError;
            QuadrupolSplitting = quadrupolSplitting;
            QuadrupolSplittingError = quadrupolSplittingError;
            RelativeArea = relativeArea;
            RelativeAreaError = relativeAreaError;
        }

        // Hyperfine parameters
        public Decimal LineWidth { get; set; }
        public Decimal? LineWidthError { get; set; }
        public Decimal IsomerShift { get; set; }
        public Decimal? IsomerShiftError { get; set; }
        public Decimal QuadrupolSplitting { get; set; }
        public Decimal? QuadrupolSplittingError { get; set; }
        // Area
        public Decimal RelativeArea { get; set; }
        public Decimal? RelativeAreaError { get; set; }
    }
}
