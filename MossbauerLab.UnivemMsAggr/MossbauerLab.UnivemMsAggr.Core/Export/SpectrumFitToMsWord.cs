using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MossbauerLab.UnivemMsAggr.Core.Data;
using Microsoft.Office.Interop.Word;
using MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public class SpectrumFitToMsWord : ISpectrumFitExport
    {
        public SpectrumFitToMsWord()
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
            try
            {
                Boolean doubletsOnly = data.Sextets == null || data.Sextets.Count == 0;
                Int32 rows = (!doubletsOnly) ? data.Sextets.Count + data.Doublets.Count + 1 : data.Doublets.Count + 1;
                Int32 columns = (!doubletsOnly) ? _tableHeaderMixedCompEn.Count : _tableHeaderDoubletsOnlyEn.Count;
                Table componentsTable = CreateDocTable(rows, columns);
                if (data.Sextets != null && data.Sextets.Count > 0)
                {
                    for (Int32 row = 1; row <= data.Sextets.Count + 1; row++)
                    {
                        for (Int32 column = 1; column <= _tableHeaderMixedCompEn.Count; column++)
                        {
                            if (row == 1)
                                componentsTable.Cell(row, column).Range.Text = _tableHeaderMixedCompEn[column - 1];
                            else
                            {
                                if (row == 2 && column == 1)
                                    componentsTable.Cell(row, column).Range.Text = data.SampleName;
                                else if (row == 2 && column == ChiSquareValueSextetIndex)
                                    componentsTable.Cell(row, column).Range.Text = data.Info.ChiSquareValue.ToString(CultureInfo.InvariantCulture);
                                else if (column == ComponentNameSextetIndex)
                                    componentsTable.Cell(row, column).Range.Text = "S" + (row - 1);
                                else
                                    componentsTable.Cell(row, column).Range.Text = GetComponentColumnValue(data.Sextets[row - 2], column,
                                                                                                           data.Info.VelocityStep,
                                                                                                           data.Info.HyperfineFieldPerMmS);
                            }
                        }
                    }
                }
                if (data.Doublets != null)
                {
                    Int32 startIndex = doubletsOnly ? 1 : data.Sextets.Count + 2;
                    for (Int32 row = startIndex; row <= rows; row++)
                    {
                        for (Int32 column = 1; column <= columns; column++)
                        {
                            if (row == 1)
                                componentsTable.Cell(row, column).Range.Text = _tableHeaderDoubletsOnlyEn[column - 1];
                            else
                            {
                                if (row == 2 && column == 1)
                                    componentsTable.Cell(row, column).Range.Text = data.SampleName;
                                else if (row == 2 && column == 6)
                                    componentsTable.Cell(row, column).Range.Text = data.Info.ChiSquareValue.ToString(CultureInfo.InvariantCulture);
                                else if (column == ComponentNameDoubletIndex)
                                    componentsTable.Cell(row, column).Range.Text = "D" + (row - startIndex);
                                else
                                    componentsTable.Cell(row, column).Range.Text = GetComponentColumnValue(data.Doublets[(startIndex > 1 ? 
                                                                                                                         row - startIndex: row - 2)], column,
                                                                                                           data.Info.VelocityStep,
                                                                                                           data.Info.HyperfineFieldPerMmS);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public Boolean Export(String destination, IList<SpectrumFit> data)
        {
            throw new NotImplementedException();
        }

        private String GetComponentColumnValue<T>(T component, Int32 index, Decimal velocityStep, Decimal hyperfineFieldError) where T : class
        {
            if (component is Sextet)
            {
                Sextet sextet = component as Sextet;
                switch (index)
                {
                    case LineWidthSextetIndex:
                         return GetParameter(sextet.LineWidth, sextet.LineWidthError, 2 * velocityStep, 3, _parametersFormatInfo);
                    case IsomerShiftSextetIndex:
                         return GetParameter(sextet.IsomerShift, sextet.IsomerShiftError, velocityStep, 3, _parametersFormatInfo);
                    case QuadrupolSplittingSextetIndex:
                         return GetParameter(sextet.QuadrupolShift, sextet.QuadrupolShiftError, velocityStep, 3, _parametersFormatInfo);
                    case HyperfineFieldSextetIndex:
                         return GetParameter(sextet.HyperfineField, sextet.HyperfineFieldError, hyperfineFieldError, 1, _hypFieldFormatInfo);
                    case RelativeAreaSextetIndex:
                         return GetParameter(sextet.RelativeArea, sextet.RelativeAreaError, 0, 2, _areaFormatInfo);
                }
            }
            else if (component is Doublet)
            {
                Doublet doublet = component as Doublet;
                switch (index)
                {
                    case LineWidthDoubletIndex:
                         return GetParameter(doublet.LineWidth, doublet.LineWidthError, 2 * velocityStep, 3, _parametersFormatInfo);
                    case IsomerShiftDoubletIndex:
                         return GetParameter(doublet.IsomerShift, doublet.IsomerShiftError, velocityStep, 3, _parametersFormatInfo);
                    case QuadrupolSplittingDoubletIndex:
                         return GetParameter(doublet.QuadrupolSplitting, doublet.QuadrupolSplittingError, velocityStep, 3, _parametersFormatInfo);
                    case RelativeAreaDoubletIndex:
                         return GetParameter(doublet.RelativeArea, doublet.RelativeAreaError, 0, 2, _areaFormatInfo);
                }
            }
            else throw new InvalidOperationException("component can't be only Doublet or Sextet");
            return String.Empty;
        }

        private String GetParameter(Decimal value, Decimal? error, Decimal comparator, Int32 round, NumberFormatInfo format)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(Decimal.Round(value, round).ToString(format));
            if (error != null)
            {
                builder.Append("±");
                Decimal errorValue = error > comparator ? error.Value : comparator;
                builder.Append(Decimal.Round(errorValue, round).ToString(format));
            }
            return builder.ToString();
        }

        private Table CreateDocTable(Int32 rows, Int32 columns)
        {
            _msWordApplication.Visible = true;
            _msWordDocument = _msWordApplication.Documents.Add(); // without template, create no template and others ...
            // creating bookmark
            Object missing = System.Reflection.Missing.Value;
            /* \endofdoc is a predefined bookmark */
            Object range = _msWordDocument.Bookmarks.get_Item(ref _endOfDoc).Range; //go to end of the page
            Paragraph paragraph = _msWordDocument.Content.Paragraphs.Add(ref range); //add paragraph at end of document
            paragraph.Range.Text = "Test Table Caption"; //add some text in paragraph
            paragraph.Format.SpaceAfter = 10; //define some style
            paragraph.Range.InsertParagraphAfter(); //insert paragraph
            Range wordRange = _msWordDocument.Bookmarks.get_Item(ref _endOfDoc).Range;
            // creating table
            // todo: umv: create private method returns table
            Table componentsTable = _msWordDocument.Tables.Add(wordRange, rows, columns, ref missing, ref missing);
            return componentsTable;
        }

        private const Int32 LineWidthSextetIndex = 2;
        private const Int32 IsomerShiftSextetIndex = 3;
        private const Int32 QuadrupolSplittingSextetIndex = 4;
        private const Int32 HyperfineFieldSextetIndex = 5;
        private const Int32 RelativeAreaSextetIndex = 6;
        private const Int32 ChiSquareValueSextetIndex = 7;
        private const Int32 ComponentNameSextetIndex = 8;

        private const Int32 LineWidthDoubletIndex = 2;
        private const Int32 IsomerShiftDoubletIndex = 3;
        private const Int32 QuadrupolSplittingDoubletIndex = 4;
        private const Int32 RelativeAreaDoubletIndex = 5;
        private const Int32 ComponentNameDoubletIndex = 7;

        private readonly NumberFormatInfo _parametersFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _hypFieldFormatInfo = new NumberFormatInfo();
        private readonly NumberFormatInfo _areaFormatInfo = new NumberFormatInfo();

        private readonly _Application _msWordApplication = new Application();
        private Object _endOfDoc = "\\endofdoc";
        private _Document _msWordDocument;
        private readonly IList<String> _tableHeaderMixedCompEn = new List<String>()
        {
            "Sample", "Γ, mm/s", "δ, mm/s", "2έ, mm/s", "Heff, kOe", "A, %", "χ2", "Component"
        };

        private readonly IList<String> _tableHeaderDoubletsOnlyEn = new List<String>()
        {
            "Sample", "Γ, mm/s", "δ, mm/s", "2έ, mm/s", "A, %", "χ2", "Component"
        };
    }
}
