using System;

namespace MercuryMessenger.Interfaces
{
    public interface ISubscriber
    {
        void Subscribe(object recipient, Action<object> action);

        void Unsubscribe(object recipient);
    }
}
