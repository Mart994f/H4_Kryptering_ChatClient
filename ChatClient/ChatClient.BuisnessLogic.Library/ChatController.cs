using Microsoft.Extensions.Configuration;
using System;

namespace ChatClient.BuisnessLogic.Library
{
    public class ChatController
    {
        #region Private Fields

        private IConfiguration _configuration;

        #endregion

        #region Constructors

        public ChatController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Public Methods

        public void Run()
        {
            Console.WriteLine("Starting application..");
        }

        #endregion

        #region Private Helper Methods

        #endregion
    }
}
