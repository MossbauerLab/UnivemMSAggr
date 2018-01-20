using System;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public class ProcessedSpectrumFitEventArgs : EventArgs
    {
        public ProcessedSpectrumFitEventArgs()
        {
        }

        public ProcessedSpectrumFitEventArgs(String processedSpectrumFitFile)
        {
            ProcessedSpectrumFitFile = processedSpectrumFitFile;
        }

        public String ProcessedSpectrumFitFile { get; set; }
    }
}
