using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    /// <summary>
    ///  This export implementation is not primary for us
    /// </summary>
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

        public Boolean Export(String destination, IList<SpectrumFit> data)
        {
            if (!File.Exists(Path.GetFullPath(destination)))
            {
                File.Create(destination).Close();
            }
            // todo: umv: think about should we delete or not if file already exists
            List<String> content = new List<String>();
            content.Add(data.Any(fit => fit != null && fit.Sextets.Count > 0) ? TableHeaderMixCompEn : TableHeaderDoubletsOnlyEn);
            try
            {
                foreach (SpectrumFit fit in data)
                    content.AddRange(GetExportingLines(fit));
            }
            catch (Exception e)
            {
                return false;
            }
            File.AppendAllLines(destination, content);
            return true;
        }

        public Boolean Export(String destination, SpectrumFit data)
        {
            if (!File.Exists(Path.GetFullPath(destination)))
            {
                File.Create(destination).Close();
            }
            // todo: umv: think about should we delete or not if file already exists
            List<String> content = new List<String>();
            content.Add(data!= null && data.Sextets.Count > 0 ? TableHeaderMixCompEn : TableHeaderDoubletsOnlyEn);
            try
            {
                content.AddRange(GetExportingLines(data));
            }
            catch (Exception e)
            {
                return false;
            }
            File.AppendAllLines(destination, content);
            return true;
        }

        private IList<String> GetExportingLines(SpectrumFit data)
        {
            IList<String> lines = new List<String>();
            if (data.Sextets != null)
            {
                for (Int32 i = 0; i < data.Sextets.Count; i++)
                {
                    lines.Add(i == 0 ? ConvertSextet(data.SampleName, data.Sextets[i], data.Info.HyperfineFieldPerMmS, data.Info.VelocityStep, data.Info.ChiSquareValue, i)
                                     : ConvertSextet(null, data.Sextets[i], data.Info.HyperfineFieldPerMmS, data.Info.VelocityStep, null, i));
                }
            }
            if (data.Doublets != null)
            {
                Boolean doubletsOnly = data.Sextets == null || data.Sextets.Count == 0;
                for (Int32 i = 0; i < data.Doublets.Count; i++)
                {

                    lines.Add(i == 0 && doubletsOnly
                                  ? ConvertDoublet(data.SampleName, data.Doublets[i], data.Info.VelocityStep, data.Info.ChiSquareValue, i, doubletsOnly)
                                  : ConvertDoublet(null, data.Doublets[i], data.Info.VelocityStep, null, i, doubletsOnly));
                }
            }
            return lines;
        }

        private String ConvertSextet(String sample, Sextet sextet, Decimal hyperfineFieldError, Decimal velocityStep, Decimal? chiSquare, Int32 sextetNumber)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(!String.IsNullOrWhiteSpace(sample) ? sample : "\t\t");

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

        private String ConvertDoublet(String sample, Doublet doublet, Decimal velocityStep, Decimal? chiSquare, Int32 doubletNumber, Boolean doubletsOnly = false)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(!String.IsNullOrWhiteSpace(sample) ? sample : "\t\t");

            AppendData(builder, doublet.LineWidth, doublet.LineWidthError, velocityStep * 2, 3, _parametersFormatInfo);
            AppendData(builder, doublet.IsomerShift, doublet.IsomerShiftError, velocityStep, 3, _parametersFormatInfo);
            AppendData(builder, doublet.QuadrupolSplitting, doublet.QuadrupolSplittingError, velocityStep, 3, _parametersFormatInfo);
            if(!doubletsOnly)
                builder.Append("\t\t");
            AppendData(builder, doublet.RelativeArea, doublet.RelativeAreaError, 0, 2, _hypFieldFormatInfo);

            if (chiSquare != null)
                builder.Append(chiSquare);
            builder.Append("\t");

            builder.Append("D" + doubletNumber);

            return builder.ToString();
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

        private const String TableHeaderMixCompEn = "Sample\t\tΓ, mm/s\t\tδ, mm/s\t\t2έ, mm/s\tHeff, kOe\tA, %\t\tχ2\tComponent";
        private const String TableHeaderDoubletsOnlyEn = "Sample\t\tΓ, mm/s\t\tδ, mm/s\t\t2έ, mm/s\tA, %\t\tχ2\tComponent";

        private readonly NumberFormatInfo _parametersFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _hypFieldFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _areaFormatInfo = new NumberFormatInfo();
    }
}
