using ChatClient.BuisnessLogic.Library.Communication;
using ChatClient.BuisnessLogic.Library.Observer;
using Microsoft.Extensions.Configuration;
using System;

namespace ChatClient.BuisnessLogic.Library
{
    public class ChatController : IObserver
    {
        #region Private Fields

        private IConfiguration _configuration;

        private ICommunicationHandler _communicationHandler;

        #endregion

        #region Constructors

        public ChatController(IConfiguration configuration, ICommunicationHandler communicationHandler)
        {
            _configuration = configuration;
            _communicationHandler = communicationHandler;
        }

        #endregion

        #region Public Methods

        public void Run()
        {
            Console.WriteLine("Starting application..", Console.ForegroundColor = ConsoleColor.Red);

            _communicationHandler.Attach(this);

            _communicationHandler.Connect();

            Console.WriteLine("Application stated.\n", Console.ForegroundColor = ConsoleColor.Green);
        }

        #endregion

        #region Observer Methods

        public void Update(ISubject subject)
        {
            HandleNewMessage();
        }

        #endregion

        #region Private Helper Methods

        private void HandleNewMessage()
        {
            Console.WriteLine(_communicationHandler.GetMessage(), Console.ForegroundColor = ConsoleColor.White);
        }

        #endregion
    }
}
