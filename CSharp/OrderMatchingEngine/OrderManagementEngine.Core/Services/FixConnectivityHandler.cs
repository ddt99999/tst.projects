using QuickFix;

namespace OrderManagementEngine.Core.Services
{
    public class FixConnectivityHandler : IFixConnectivityHandler
    {
        public bool SendMessageToTarget(Message message, SessionID sessionId)
        {
            return Session.SendToTarget(message, sessionId);
        }
    }
}