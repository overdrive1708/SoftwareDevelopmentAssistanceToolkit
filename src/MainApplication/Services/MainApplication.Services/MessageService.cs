using MainApplication.Services.Interfaces;

namespace MainApplication.Services
{
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
