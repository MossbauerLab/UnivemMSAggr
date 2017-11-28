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
            _exportService.Export(OutFile, fit);
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String NickelFerriteNbCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NB-2-4096_comp.10s-2017-3.txt";
        private const String Bioffer2CompFile = @"..\..\CompFilesExamples\BIOFER2-1024_comp_7s1d.txt";
        private const String FakeDoubletsCompFile = @"..\..\CompFilesExamples\Fake_spec_comp.txt";
        private const String OutFile = @"Result.doc";

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToMsWord();
    }
}
