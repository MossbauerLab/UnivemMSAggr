using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public class SpectrumFitToTextExport : ISpectrumFitExport
    {
        public SpectrumFitToTextExport()
        {
            _parametersFormatInfo.NumberDecimalSeparator = ".";
            _parametersFormatInfo.NumberDecimalDigits = 3;
            _hypFieldFormatInfo.NumberDecimalSeparator = ".";
            _hypFieldFormatInfo.NumberDecimalDigits = 1;
            _areaFormatInfo.NumberDecimalSeparator = ".";
            _areaFormatInfo.NumberDecimalDigits = 2;
        }

        public Boolean Export(String destination, SpectrumFit data)
        {
            if (!File.Exists(Path.GetFullPath(destination)))
            {
                File.Create(destination).Close();
            }
            // todo: umv: think about should we delete or not if file already exists
            IList<String> dataRepresentation = new List<String>();
            dataRepresentation.Add(TableHeaderEn);
            if (data.Sextets != null)
            {
                for (Int32 i = 0; i < data.Sextets.Count; i++)
                {
                    dataRepresentation.Add(i == 0 ? GetSextetString(null, data.Sextets[i], data.Info.HyperfineFieldPerMmS, data.Info.VelocityStep, data.Info.ChiSquareValue, i)
                                                  : GetSextetString(null, data.Sextets[i], data.Info.HyperfineFieldPerMmS, data.Info.VelocityStep, null, i));
                }
            }
            File.AppendAllLines(destination, dataRepresentation);
            return true;
        }

        private String GetSextetString(String sample, Sextet sextet, Decimal hyperfineFieldError, Decimal velocityStep, Decimal? chiSquare, Int32 sextetNumber)
        {
            StringBuilder builder = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(sample))
                builder.Append(sample);
            else builder.Append("\t\t");

            builder.AppendFormat(Decimal.Round(sextet.LineWidth, 3).ToString(_parametersFormatInfo));
            if (sextet.LineWidthError != null)
            {
                builder.Append("±");
                Decimal errorValue = sextet.LineWidthError > velocityStep * 2 ? sextet.LineWidthError.Value : velocityStep * 2;
                builder.Append(Decimal.Round(errorValue).ToString(_parametersFormatInfo));
            }
            builder.Append("\t");

            builder.Append(Decimal.Round(sextet.IsomerShift, 3).ToString(_parametersFormatInfo));
            if (sextet.IsomerShiftPError != null)
            {
                builder.Append("±");
                Decimal errorValue = sextet.IsomerShiftPError > velocityStep ? sextet.IsomerShiftPError.Value : velocityStep;
                builder.Append(Decimal.Round(errorValue, 3).ToString(_parametersFormatInfo));
            }
            builder.Append("\t");

            builder.Append(Decimal.Round(sextet.QuadrupolShift, 3).ToString(_parametersFormatInfo));
            if (sextet.QuadrupolShiftError != null)
            {
                builder.Append("±");
                Decimal errorValue = sextet.QuadrupolShiftError > velocityStep ? sextet.QuadrupolShiftError.Value : velocityStep;
                builder.Append(Decimal.Round(errorValue, 3).ToString(_parametersFormatInfo));
            }
            builder.Append("\t");

            builder.Append(Decimal.Round(sextet.HyperfineField, 1).ToString(_hypFieldFormatInfo));
            if (sextet.HyperfineFieldError != null)
            {
                builder.Append("±");
                Decimal errorValue = sextet.HyperfineFieldError > hyperfineFieldError ? sextet.HyperfineFieldError.Value : hyperfineFieldError;
                builder.Append(Decimal.Round(errorValue, 1).ToString(_hypFieldFormatInfo));
            }
            builder.Append("\t");

            builder.Append(Decimal.Round(sextet.RelativeArea, 2).ToString(_areaFormatInfo));
            builder.Append("±");
            // ReSharper disable once PossibleInvalidOperationException
            builder.Append(Decimal.Round(sextet.RelativeAreaError.Value, 2).ToString(_areaFormatInfo));
            builder.Append("\t");

            if (chiSquare != null)
                builder.Append(chiSquare);
            builder.Append("\t");

            builder.Append("S" + sextetNumber);

            return builder.ToString();
        }

        private String GetDoubletString(Doublet doublet)
        {
            return String.Empty;
        }

        private const String TableHeaderEn = "Sample\t\tΓ, mm/s\tδ, mm/s\t\t2έ mm/s\t\tHeff kOe\tA, %\t\tχ2\tComponent";

        private readonly NumberFormatInfo _parametersFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _hypFieldFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _areaFormatInfo = new NumberFormatInfo();
    }
}
