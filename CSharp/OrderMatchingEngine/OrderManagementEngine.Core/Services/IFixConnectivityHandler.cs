using QuickFix;

namespace OrderManagementEngine.Core.Services
{
    public interface IFixConnectivityHandler
    {
        bool SendMessageToTarget(Message message, SessionID sessionId);
    }
}