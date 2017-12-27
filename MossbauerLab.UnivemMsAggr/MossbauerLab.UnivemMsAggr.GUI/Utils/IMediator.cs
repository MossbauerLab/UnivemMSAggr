using System;

namespace MossbauerLab.UnivemMsAggr.GUI.Utils
{
    public interface IMediator<TM> 
        where TM : class
    {
        Boolean AddParticipant(Guid id, IMessageRecipient<TM> participant);
        Boolean Send(TM message, Guid id);
    }
}
