using StockChat.Domain.Entity;
using StockChatBot.Interface;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace StockChatBot.Service
{
    public class ProcessMessageService : IProcessMessageService
    {
        private readonly ISendMessageService _sendMessageService;
        public ProcessMessageService(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }

        public void ProcessMessage(Message receivedMessage)
        {
            _sendMessageService.DefaultMessage(receivedMessage.ChatId);

            var stockCode = receivedMessage.Content.ToLower().Split('=')[1];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string price = string.Empty;
            bool success = false;
            using (var ms = new MemoryStream())
            {
                // Copy the response stream to the memory stream.
                response.GetResponseStream().CopyTo(ms);
                var stringFromByteArray = Encoding.UTF8.GetString(ms.ToArray());
                if (!string.IsNullOrEmpty(stringFromByteArray))
                {
                    var splitedstring = stringFromByteArray.Split(',');
                    if (!string.IsNullOrEmpty(splitedstring[13]) && !splitedstring[13].Equals("N/D"))
                    {
                        price = splitedstring[13];
                        success = true;
                    }
                }
            }

            _sendMessageService.SendMessage(success, receivedMessage.ChatId, stockCode, price);
        }
    }
}
