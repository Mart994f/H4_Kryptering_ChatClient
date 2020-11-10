using ChatClient.BuisnessLogic.Library.Messageing;

namespace ChatClient.BuisnessLogic.Library.Communication
{
    public interface ICommunication
    {
        void SendMessage(string message);
        string GetMessage();
    }
}
