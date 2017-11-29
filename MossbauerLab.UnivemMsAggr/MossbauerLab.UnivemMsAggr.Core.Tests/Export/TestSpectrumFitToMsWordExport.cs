using System;
using System.IO;
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
        [TestCase(Bioffer2CompFile, NickelFerriteNbCompFile)]
        [TestCase(FakeDoubletsCompFile, Bioffer2CompFile)]
        public void TestExportMultipleFits(String componentsFile1, String componentsFile2)
        {
            SpectrumFit fit1 = CompProcessor.Process(componentsFile1);
            fit1.SampleName = "SAMP#1";
            SpectrumFit fit2 = CompProcessor.Process(componentsFile2);
            fit2.SampleName = "SAMP#2";
            Boolean result = _exportService.Export(OutFile, new[] {fit1, fit2});
            Assert.IsTrue(result, "check if result is true");
        }


        [Test]
        public void TestExportRunTwice()
        {
            SpectrumFit fit1 = CompProcessor.Process(NickelFerriteNaCompFile);
            fit1.SampleName = "SAMP#1";
            SpectrumFit fit2 = CompProcessor.Process(NickelFerriteNbCompFile);
            fit2.SampleName = "SAMP#2";
            Boolean result = _exportService.Export(OutFile, fit1);
            Assert.IsTrue(result, "check if result is true");
            File.Delete(OutFile);
            result = _exportService.Export(OutFile, fit2);
            Assert.IsTrue(result, "check if result is true");
        }

        [Test]
        public void TestExportManyFits()
        {
            SpectrumFit fit1 = CompProcessor.Process(NickelFerriteNaCompFile);
            fit1.SampleName = "SAMP#1";
            SpectrumFit fit2 = CompProcessor.Process(NickelFerriteNbCompFile);
            fit2.SampleName = "SAMP#2";
            SpectrumFit fit3 = CompProcessor.Process(FakeDoubletsCompFile);
            fit3.SampleName = "SAMP#3";
            SpectrumFit fit4 = CompProcessor.Process(Bioffer2CompFile);
            fit4.SampleName = "SAMP#4";
            Boolean result = _exportService.Export(OutFile, new[] { fit1, fit2, fit3, fit4 });
            Assert.IsTrue(result, "check if result is true");
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String NickelFerriteNbCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NB-2-4096_comp.10s-2017-3.txt";
        private const String Bioffer2CompFile = @"..\..\CompFilesExamples\BIOFER2-1024_comp_7s1d.txt";
        private const String FakeDoubletsCompFile = @"..\..\CompFilesExamples\Fake_spec_comp.txt";
        private const String OutFile = @"Result.docx";

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToMsWord();
    }
}
