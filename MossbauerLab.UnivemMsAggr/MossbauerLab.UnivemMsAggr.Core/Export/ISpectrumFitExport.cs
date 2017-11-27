using System;
using MossbauerLab.UnivemMsAggr.Core.Data;

namespace MossbauerLab.UnivemMsAggr.Core.Export
{
    public interface ISpectrumFitExport
    {
        Boolean Export(String destination, SpectrumFit data);
    }
}
