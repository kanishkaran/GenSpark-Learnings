
using _5_DependencyInversion.Interfaces;
using _5_DependencyInversion.Models;
using _5_DependencyInversion.Repository;

namespace _5_DependencyInversion.Services
{
    public class OrderProcessor
    {
        private readonly OrderRepository _repo;
        private readonly IEnumerable<INotifier> _notifiers;

        // Dependencies are injected via constructor
        public OrderProcessor(OrderRepository repo, IEnumerable<INotifier> notifiers)
        {
            _repo = repo;
            _notifiers = notifiers;
        }

        public void Process(Order order)
        {
            _repo.Save(order);
            foreach (var n in _notifiers)
            {
                n.SendNotification(order);
            }
        }
    }
}