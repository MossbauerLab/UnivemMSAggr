using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.UnivemMs.FilesProcessor;
using NUnit.Framework;

namespace MossbauerLab.UnivemMsAggr.Core.Tests.UnivemMs.FileProcessor
{
    
    [TestFixture]
    public class TestCompProcessor
    {
        [Test]
        public void TestProcessorOnNickelFerrites()
        {
            SpectrumFit fit = CompProcessor.Process(NickelFerriteNaCompFile);
            Assert.IsNotNull(fit, "Checking that nickel ferrite NA fit is not a null");
            Assert.IsNotNull(fit.Info, "Checking that nickel ferrite NA fit Info is not a null");
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String NickelFerriteNbCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NB-2-4096_comp.10s-2017-3.txt";
    }
}
