using System;
using System.Text;
using Disruptor;
using OrderManagementEngine.Core.Messages;
using OrderManagementEngine.Core.Utils;
using QuickFix;
using QuickFix.FIX44;
using Message = QuickFix.Message;

namespace OrderManagementEngine.Core.Networks
{
    public class NetworkRequestHandler : MessageCracker, IApplication
    {
        private readonly IFix44MessageHandler fix44FixMessageHandler;

        public NetworkRequestHandler(
            IFix44MessageHandler fix44FixMessageHandler)
        {
            this.fix44FixMessageHandler = fix44FixMessageHandler;
        }

        #region QuickFix methods
        public void ToAdmin(Message message, SessionID sessionID)
        {
            
        }

        public void FromAdmin(Message message, SessionID sessionID)
        {
            
        }

        public void ToApp(Message message, SessionID sessionId)
        {
            Console.WriteLine("OUT: " + message);
        }

        public void FromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("IN:  " + message);
            try
            {
                Crack(message, sessionID);
            }
            catch (UnsupportedMessageType)
            {
                Console.WriteLine("Unsupported message type: {0}", message.GetType());
            }
        }

        public void OnCreate(SessionID sessionID)
        {
           
        }

        public void OnLogout(SessionID sessionID)
        {
            Console.WriteLine("Log OUT");
        }

        public void OnLogon(SessionID sessionID)
        {
            Console.WriteLine("Log IN");
        }

        #endregion

        #region FIX 44 Messages

        public void OnMessage(NewOrderSingle message, SessionID sessionId)
        {
            fix44FixMessageHandler.ProcessMessage(message, sessionId);
        }

        public void OnMessage(News n, SessionID sessionId)
        {
           
        }

        public void OnMessage(OrderCancelRequest msg, SessionID sessionId)
        {
        }

        public void OnMessage(OrderCancelReplaceRequest msg, SessionID sessionId)
        {
        }

        #endregion
    }
}