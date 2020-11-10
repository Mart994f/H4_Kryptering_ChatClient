using ChatClient.BuisnessLogic.Library.Observer;

namespace ChatClient.BuisnessLogic.Library.Communication
{
    public interface ICommunicationHandler : ICommunication, IConnetable, ISubject
    {
    }
}
