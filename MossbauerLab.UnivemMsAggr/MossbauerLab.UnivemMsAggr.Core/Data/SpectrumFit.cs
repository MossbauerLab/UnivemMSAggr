using System.Collections.Generic;
using MossbauerLab.UnivemMsAggr.Core.Data.FitsInfo;
using MossbauerLab.UnivemMsAggr.Core.Data.SpectralComponents;

namespace MossbauerLab.UnivemMsAggr.Core.Data
{
    public class SpectrumFit
    {
        public SpectrumFit()
        {
        }

        public SpectrumFit(IList<Sextet> sextets, IList<Doublet> doublets, ComponentsInfo info)
        {
            Sextets = sextets;
            Doublets = doublets;
            Info = info;
        }

        public IList<Sextet> Sextets { get; set; }
        public IList<Doublet> Doublets { get; set; }
        public ComponentsInfo Info { get; set; }
    }
}
