using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MossbauerLab.UnivemMsAggr.Core.Data.FitsInfo.CalculatedValues
{
    public static class EmpiricCalculations
    {
        public static Decimal CalculateHyperfineFieldError(Decimal velocityStep)
        {
            return KOeInMmS * velocityStep;
        }

        // ReSharper disable once InconsistentNaming
        // kOe per 1 mm/s
        public const Decimal KOeInMmS = 31.0517399295m;
        public const Decimal RelativeAreaRelativeError = 0.1m;
    }
}
