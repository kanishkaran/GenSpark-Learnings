



using _5_DependencyInversion.Models;

namespace _5_DependencyInversion.Interfaces
{
    public interface INotifier
    {
        void SendNotification(Order order);
    }
}