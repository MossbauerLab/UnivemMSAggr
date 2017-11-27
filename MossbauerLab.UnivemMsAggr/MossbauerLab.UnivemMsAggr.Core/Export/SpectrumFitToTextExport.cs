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

            AppendData(builder, sextet.LineWidth, sextet.LineWidthError, velocityStep * 2, 3, _parametersFormatInfo);
            AppendData(builder, sextet.IsomerShift, sextet.IsomerShiftError, velocityStep, 3, _parametersFormatInfo);
            AppendData(builder, sextet.QuadrupolShift, sextet.QuadrupolShiftError, velocityStep, 3, _parametersFormatInfo);
            AppendData(builder, sextet.HyperfineField, sextet.HyperfineFieldError, hyperfineFieldError, 1, _hypFieldFormatInfo);
            AppendData(builder, sextet.RelativeArea, sextet.RelativeAreaError, 0, 2, _hypFieldFormatInfo);

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

        private void AppendData(StringBuilder builder, Decimal value, Decimal? error, Decimal comparator, 
                                Int32 round, NumberFormatInfo format, String spacing = "\t")
        {
            builder.Append(Decimal.Round(value, round).ToString(format));
            if (error != null)
            {
                builder.Append("±");
                Decimal errorValue = error > comparator ? error.Value : comparator;
                builder.Append(Decimal.Round(errorValue, round).ToString(format));
            }
            builder.Append(spacing);
        }

        private const String TableHeaderEn = "Sample\t\tΓ, mm/s\tδ, mm/s\t\t2έ, mm/s\tHeff, kOe\tA, %\t\tχ2\tComponent";

        private readonly NumberFormatInfo _parametersFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _hypFieldFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _areaFormatInfo = new NumberFormatInfo();
    }
}
