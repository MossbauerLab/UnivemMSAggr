using System;
using System.Collections.Generic;
using MossbauerLab.UnivemMsAggr.Core.Data;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public interface ISpectrumFitExport
    {
        Boolean Export(String destination, SpectrumFit data);
        Boolean Export(String destination, IList<SpectrumFit> data);
    }
}
