
namespace MossbauerLab.UnivemMsAggr.GUI.Utils
{
    public interface IMessageRecipient<T>
        where T: class
    {
        void TransferMessage(T message);
    }
}
