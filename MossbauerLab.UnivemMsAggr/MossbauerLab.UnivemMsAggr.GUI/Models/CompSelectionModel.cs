using System;

namespace MossbauerLab.UnivemMsAggr.GUI.Models
{
    public class CompSelectionModel
    {
        public CompSelectionModel()
        {
        }

        public CompSelectionModel(String sampleName, String spectrumComponentFile)
        {
            SampleName = sampleName;
            SpectrumComponentFile = spectrumComponentFile;
        }

        public String SampleName { get; set; }
        public String SpectrumComponentFile { get; set; }
    }
}
