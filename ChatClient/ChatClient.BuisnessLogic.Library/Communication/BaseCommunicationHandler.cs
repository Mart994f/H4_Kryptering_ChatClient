using ChatClient.BuisnessLogic.Library.Observer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatClient.BuisnessLogic.Library.Communication
{
    public class BaseCommunicationHandler : ISubject
    {
        #region Private Fields

        private TcpClient _tcpClient;

        private IPAddress _serverIp;

        private int _port;

        private byte[] _readBuffer;

        private string _recivedMessage = "";

        private List<IObserver> _observers;

        #endregion

        #region Properties

        protected NetworkStream NetworkStream { get { return _tcpClient.GetStream(); } }

        protected IPAddress ServerIp { get { return _serverIp; } set { _serverIp = value; } }

        protected int Port { get { return _port; } set { _port = value; } }

        protected string RecivedMessage { get { return _recivedMessage; } }

        #endregion

        #region Constructors

        protected BaseCommunicationHandler()
        {
            _tcpClient = new TcpClient();
            _observers = new List<IObserver>();
        }

        #endregion

        #region Public Methods

        public void Connect()
        {
            if (!_tcpClient.Connected)
            {
                _tcpClient.Connect(_serverIp, _port);
            }
        }

        public void Write(string message)
        {
            if (message != null)
            {
                byte[] writeBuffer = Encoding.UTF8.GetBytes(message);

                NetworkStream.BeginWrite(writeBuffer, 0, writeBuffer.Length, new AsyncCallback(WriteCallback), NetworkStream);
            }
        }

        public void Read()
        {
            while (_tcpClient.Connected && NetworkStream.CanRead)
            {
                if (NetworkStream.DataAvailable)
                {
                    _readBuffer = new byte[_tcpClient.Available];

                    NetworkStream.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(ReadCallback), NetworkStream);
                }
            }
        }

        #endregion

        #region Observer Methods

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this);
            }
        }

        #endregion

        #region Private Helper Methods

        private void ReadCallback(IAsyncResult asyncResult)
        {
            NetworkStream networkStream = (NetworkStream)asyncResult.AsyncState;
            int numperOfBytesRead;

            numperOfBytesRead = networkStream.EndRead(asyncResult);

            _recivedMessage = string.Concat(_recivedMessage, Encoding.UTF8.GetString(_readBuffer, 0, _readBuffer.Length));

            while (networkStream.DataAvailable)
            {
                networkStream.BeginRead(_readBuffer, 0, _readBuffer.Length, new AsyncCallback(ReadCallback), networkStream);
            }

            Notify();
        }

        private void WriteCallback(IAsyncResult asyncResult)
        {
            NetworkStream networkStream = (NetworkStream)asyncResult.AsyncState;
            networkStream.EndWrite(asyncResult);
        }

        #endregion
    }
}
