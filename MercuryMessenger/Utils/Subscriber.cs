using MercuryMessenger.Interfaces;
using System;

namespace MercuryMessenger.Utils
{
    public class Subscriber : ISubscriber
    {
        public void Subscribe(object recipient, Action<object> action)
        {
            MessegingCenter.MercuryMessenger.Messenger.Register<object>(recipient, action);
        }

        public void Unsubscribe(object recipient)
        {
            MessegingCenter.MercuryMessenger.Messenger.Unregister<object>(recipient);
        }
    }
}
