using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MossbauerLab.UnivemMsAggr.Core.Data;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public class SpectrumFitToMsWord : ISpectrumFitExport
    {
        public SpectrumFitToMsWord()
        {
        }

        public Boolean Export(String destination, SpectrumFit data)
        {
            try
            {
                _msWordApplication.Visible = true;
                _msWordDocument = _msWordApplication.Documents.Add(); // without template, create no template and others ...
                // creating bookmark
                Object missing = System.Reflection.Missing.Value;
                Object endOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */
                Object range = _msWordDocument.Bookmarks.get_Item(ref endOfDoc).Range; //go to end of the page
                Paragraph paragraph = _msWordDocument.Content.Paragraphs.Add(ref range); //add paragraph at end of document
                paragraph.Range.Text = "Test Table Caption"; //add some text in paragraph
                paragraph.Format.SpaceAfter = 10; //define some style
                paragraph.Range.InsertParagraphAfter(); //insert paragraph
                Range wordRange = _msWordDocument.Bookmarks.get_Item(ref endOfDoc).Range;
                // creating table
                // todo: umv: create private method return table
                Table componentsTable = _msWordDocument.Tables.Add(wordRange, 3, 8, ref missing, ref missing);
                for (Int32 row = 1; row <= 3; row++)
                {
                    for (Int32 column = 1; column <= 8; column++)
                    {
                        if (row == 1)
                            componentsTable.Cell(row, column).Range.Text = _tableHeaderMixedCompEn[column - 1]; //todo: depends on sextet presence
                        else componentsTable.Cell(row, column).Range.Text = String.Format("{0}:{1}", row, column);
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

        private _Application _msWordApplication = new Application();
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
