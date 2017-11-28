using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.Export;
using MossbauerLab.UnivemMsAggr.Core.UnivemMs.FilesProcessor;
using NUnit.Framework;

namespace MossbauerLab.UnivemMsAggr.Core.Tests.Export
{
    [TestFixture]
    public class TestSpectrumFitToTextExport
    {
        [SetUp]
        public void SetUp()
        {
            File.Delete(OutFile);
        }

        [TestCase(NickelFerriteNaCompFile)]
        [TestCase(Bioffer2CompFile)]
        [TestCase(FakeDoubletsCompFile)]
        public void TestExportSingleFit(String spectrumCompFile)
        {
            SpectrumFit fit = CompProcessor.Process(spectrumCompFile);
            _exportService.Export(OutFile, fit);
            IList<String> lines = File.ReadAllLines(OutFile);
            Assert.AreEqual(fit.Doublets.Count + fit.Sextets.Count + 1, lines.Count, "Checking export formally: by number of lines");
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String Bioffer2CompFile = @"..\..\CompFilesExamples\BIOFER2-1024_comp_7s1d.txt";
        private const String FakeDoubletsCompFile = @"..\..\CompFilesExamples\Fake_spec_comp.txt";
        private const String OutFile = @"Result.txt";

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToTextExport();
    }
}
;