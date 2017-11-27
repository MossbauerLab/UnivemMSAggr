using System;
using System.Collections.Generic;
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
        [TestCase(NickelFerriteNaCompFile)]
        public void TestExport(String spectrumCompFile)
        {
            SpectrumFit fit = CompProcessor.Process(spectrumCompFile);
            _exportService.Export(OutFile, fit);
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String OutFile = @"Result.txt";

        private readonly ISpectrumFitExport _exportService = new SpectrumFitToTextExport();
    }
}
;