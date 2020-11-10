namespace ChatClient.BuisnessLogic.Library.Messageing
{
    public class Message
    {
        public string Nickname { get; set; }

        public string SenderHostName { get; set; }

        public string SenderIp { get; set; }

        public string ReciverHostName { get; set; }

        public string ReciverIp { get; set; }

        public string MessageBody { get; set; }

        public Message(string senderHostName, string senderIp, string reciverHostName, string reciverIp,
                       string messageBody)
        {
            SenderHostName = senderHostName;
            SenderIp = senderIp;
            ReciverHostName = reciverHostName;
            ReciverIp = reciverIp;
            MessageBody = messageBody;
        }

        public override string ToString()
        {
            return $"{SenderHostName}:{SenderIp}:{ReciverHostName}:{ReciverIp}:{MessageBody}\r\n";
        }
    }
}
