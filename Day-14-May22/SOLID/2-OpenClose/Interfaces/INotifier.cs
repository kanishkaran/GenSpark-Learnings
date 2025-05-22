

using _2_OpenClose.Models;

namespace _2_OpenClose.Interfaces
{
    public interface INotifier
    {
        void SendNotification(Order order);
    }
}