
using SOLID.Interfaces;
using SOLID.models;
using SOLID.repositories;
using SOLID.Services;

namespace SOLID
{
    public class OrderProcessor
    {
        private OrderRepository repo = new OrderRepository();                       //Also for Dependency In
        private Notifier notifier = new Notifier();

        
        public void Process(Order order)
        {
            repo.Save(order);

            notifier.SendNotification(order);


        }


        // Depencency Inversion
        private readonly IOrderRepository _repo;
        private readonly IEnumerable<INotifier> _notifiers;

        // Dependencies are injected via constructor
        // public OrderProcessor(IOrderRepository repo, IEnumerable<INotifier> notifiers)
        // {
        //     _repo = repo;
        //     _notifiers = notifiers;
        // }

        public void Processes(Order order)
        {
            _repo.Save(order);

            foreach (var notifier in _notifiers)
            {
                notifier.SendNotification(order);
            }
        }
    }
}