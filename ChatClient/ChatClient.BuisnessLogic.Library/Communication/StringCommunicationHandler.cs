using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading;

namespace ChatClient.BuisnessLogic.Library.Communication
{
    public class StringCommunicationHandler : BaseCommunicationHandler, ICommunicationHandler
    {
        #region Fields

        private IConfiguration _configuration;
        private Thread _readThread;

        #endregion

        #region Constructors

        public StringCommunicationHandler(IConfiguration configuration) : base()
        {
            _configuration = configuration;
            ServerIp = IPAddress.Parse(_configuration.GetSection("ServerIp").Value);
            Port = int.Parse(_configuration.GetSection("StringCommunicationPort").Value);

            Connect();

            _readThread = new Thread(new ThreadStart(Read));
            _readThread.Start();
        }

        #endregion

        #region Methods

        public void SendMessage(string message)
        {
            Write(message);
        }

        public string GetMessage()
        {
            return RecivedMessage;
        }

        #endregion
    }
}
