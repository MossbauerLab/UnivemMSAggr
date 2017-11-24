using System;
using System.Linq;
using MossbauerLab.UnivemMsAggr.Core.Data;
using MossbauerLab.UnivemMsAggr.Core.UnivemMs.FilesProcessor;
using NUnit.Framework;

namespace MossbauerLab.UnivemMsAggr.Core.Tests.UnivemMs.FileProcessor
{
    
    [TestFixture]
    public class TestCompProcessor
    {
        [TestCase(NickelFerriteNaCompFile, 4096, 0.006, 0.2, 1.106, 10)]
        [TestCase(NickelFerriteNbCompFile, 4096, 0.006, 0.2, 1.011, 10)]
        public void TestProcessorOnNickelFerrites(String spectrumCompFile, Int32 channelsNumber, Decimal velocityStep, 
                                                  Decimal hyperfineFieldError, Decimal chiValue, Int32 sextetsNumber)
        {
            SpectrumFit fit = CompProcessor.Process(spectrumCompFile);
            Assert.IsNotNull(fit, "Checking that spectrum fit is not a null");
            Assert.IsNotNull(fit.Info, "Checking that spectrum fit Info is not a null");

            Assert.AreEqual(channelsNumber, fit.Info.ChannelsNumber, "Checking number of channels");
            Assert.AreEqual(velocityStep, Decimal.Round(fit.Info.VelocityStep, 3), "Checking velocity step");
            Assert.AreEqual(hyperfineFieldError, Decimal.Round(fit.Info.HyperfineFieldPerMmS, 1), "Checking hyperfine field per mm/s");
            Assert.AreEqual(chiValue, fit.Info.ChiSquareValue, "Checking chi squeare values");

            Assert.AreEqual(sextetsNumber, fit.Sextets.Count, "Checking that sextets number is equal to expected");
            Decimal maxField = fit.Sextets.Max(item => item.HyperfineField);
            Decimal minField = fit.Sextets.Min(item => item.HyperfineField);
            Assert.AreEqual(fit.Sextets[0].HyperfineField, maxField, "Checking that subspectra with the highest field at index 0");
            Assert.AreEqual(fit.Sextets[sextetsNumber - 1].HyperfineField, minField, String.Format("Checking that subspectra with the lowest field at index {0}", sextetsNumber - 1));
        }

        private const String NickelFerriteNaCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NA-2-4096_comp.10s-2017-3.txt";
        private const String NickelFerriteNbCompFile = @"..\..\CompFilesExamples\Indian.NiFe2.O4-NB-2-4096_comp.10s-2017-3.txt";
    }
}
