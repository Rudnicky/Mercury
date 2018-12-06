using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MercuryMessenger.MessegingCenter
{
    sealed public class MercuryMessenger
    {
        #region Private Fields
        private static MercuryMessenger _instance = null;
        private static readonly object _lock = new object();
        private static readonly ConcurrentDictionary<MessengerKey, object> _dictionary = new ConcurrentDictionary<MessengerKey, object>();
        #endregion

        #region Singleton Instance
        public static MercuryMessenger Messenger
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new MercuryMessenger();
                    }
                    return _instance;
                }
            }
        }
        #endregion

        #region Public Methods
        public void Register<T>(object recipient, Action<T> action)
        {
            Register(recipient, action, null);
        }

        public void Register<T>(object recipient, Action<T> action, object context)
        {
            var key = new MessengerKey(recipient, context, typeof(T));
            _dictionary.TryAdd(key, action);
        }

        public void Unregister<T>(object recipient)
        {
            Unregister<T>(recipient, null);
        }

        public void Unregister<T>(object recipient, object context)
        {
            object action;
            var key = new MessengerKey(recipient, context, typeof(T));
            _dictionary.TryRemove(key, out action);
        }

        public void Send<T>(T message)
        {
            Send(message, null);
        }

        public void Send<T>(T message, object context)
        {
            IEnumerable<KeyValuePair<MessengerKey, object>> result;

            if (context == null)
            {
                // Get all recipients where the context is null.
                result = from r in _dictionary where r.Key.Context == null select r;
            }
            else
            {
                // Get all recipients where the context is matching.
                result = from r in _dictionary where r.Key.Context != null && r.Key.Context.Equals(context) select r;
            }

            foreach (var action in result.Select(x => x.Value).OfType<Action<T>>())
            {
                // Send the message to all recipients.
                action(message);
            }
        }
        #endregion

        protected class MessengerKey : IEquatable<MessengerKey>
        {
            public object Recipient { get; }
            public object Context { get; }
            public Type Type { get; }

            public MessengerKey(object recipient, object context, Type type)
            {
                Recipient = recipient;
                Context = context;
                Type = type;
            }
            
            public bool Equals(MessengerKey other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;

                return Equals(Recipient, other.Recipient) && Equals(Context, other.Context) && Equals(Type, other.Type);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;

                return Equals((MessengerKey)obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = Recipient?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ (Context?.GetHashCode() ?? 0);
                    hashCode = (hashCode * 397) ^ (Type?.GetHashCode() ?? 0);
                    return hashCode;
                }
            }

            public static bool operator ==(MessengerKey left, MessengerKey right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(MessengerKey left, MessengerKey right)
            {
                return !Equals(left, right);
            }
        }
    }
}
