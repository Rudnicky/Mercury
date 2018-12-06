using System;

namespace MercuryMessenger.ViewModels
{
    public class PrimaryViewModel : BaseViewModel
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    NotifyPropertyChanged(nameof(Message));
                }
            }
        }

        public PrimaryViewModel()
        {
            MessegingCenter.MercuryMessenger.Messenger.Register<object>(this, MessageReceived);
        }

        private void MessageReceived(object obj)
        {
            if (obj != null && obj is string)
            {
                Message = obj.ToString();
            }
        }
    }
}
