using System;
using System.Collections.Generic;
using MossbauerLab.UnivemMsAggr.GUI.Models;
using MossbauerLab.UnivemMsAggr.GUI.Utils;

namespace MossbauerLab.UnivemMsAggr.GUI.ViewModels.Utils
{
    public class ViewModelsMediator : IMediator<CompSelectionModel>
    {
        public Boolean AddParticipant(Guid id, IMessageRecipient<CompSelectionModel> participant)
        {
            if (_participants.ContainsKey(id))
                return false;
            _participants.Add(id, participant);
            return true;
        }

        public Boolean Send(CompSelectionModel message, Guid id)
        {
            if (!_participants.ContainsKey(id))
                return false;
            _participants[id].TransferMessage(message);
            return true;
        }

        private readonly IDictionary<Guid, IMessageRecipient<CompSelectionModel>> _participants = new Dictionary<Guid, IMessageRecipient<CompSelectionModel>>();
    }
}
