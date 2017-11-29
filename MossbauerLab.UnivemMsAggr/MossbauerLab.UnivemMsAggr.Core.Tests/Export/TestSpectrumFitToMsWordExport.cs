using System;
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
    public class TestSpectrumFitToMsWordExport
    {
        [SetUp]
        public void SetUp()
        {
            File.Delete(OutFile);
        }

        [TestCase(NickelFerriteNaCompFile)]
        [TestCase(Bioffer2CompFile)]
        [TestCase(FakeDoubletsCompFile)]
        public void TestExportSingleFit(String componentsFile)
        {
            SpectrumFit fit = CompProcessor.Process(componentsFile);
            Boolean result =_exportService.Export(OutFile, fit);
            Assert.IsTrue(result, "check if result is true");
        }

        [TestCase(NickelFerriteNaCompFile, NickelFerriteNbCompFile)]
        public void TestExportMultipleFits(String componentsFile1, String componentsFile2)
        {
            SpectrumFit fit1 = CompProcessor.Process(componentsFile1);
            SpectrumFit fit2 = CompProcessor.Process(componentsFile2);
            Boolean result = _exportService.Export(OutFile, new[] {fit1, fit2});
            Assert.IsTrue(result, "check if result is true");
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String NickelFerriteNbCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NB-2-4096_comp.10s-2017-3.txt";
        private const String Bioffer2CompFile = @"..\..\CompFilesExamples\BIOFER2-1024_comp_7s1d.txt";
        private const String FakeDoubletsCompFile = @"..\..\CompFilesExamples\Fake_spec_comp.txt";
        private const String OutFile = @"Result.doc";

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToMsWord();
    }
}
