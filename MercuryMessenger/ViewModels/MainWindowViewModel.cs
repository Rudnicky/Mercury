using System.Windows.Input;

namespace MercuryMessenger.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _messageToSend;
        public string MessageToSend
        {
            get { return _messageToSend; }
            set
            {
                if (_messageToSend != value)
                {
                    _messageToSend = value;
                    NotifyPropertyChanged(nameof(MessageToSend));
                    IsSendButtonEnabled = !string.IsNullOrEmpty(_messageToSend);
                }
            }
        }

        private bool _isSendButtonEnabled;
        public bool IsSendButtonEnabled
        {
            get { return _isSendButtonEnabled; }
            set
            {
                if (_isSendButtonEnabled != value)
                {
                    _isSendButtonEnabled = value;
                    NotifyPropertyChanged(nameof(IsSendButtonEnabled));
                }
            }
        }

        private ICommand _sendButtonClickedCommand;
        public ICommand SendButtonClickedCommand
        {
            get
            {
                return _sendButtonClickedCommand ?? (_sendButtonClickedCommand = new RelayCommand.RelayCommand(x =>
                {
                    SendButton_Clicked(x);
                }));
            }
        }

        public MainWindowViewModel()
        {

        }

        private void SendButton_Clicked(object str)
        {
            MessegingCenter.MercuryMessenger.Messenger.Send(str.ToString());
        }
    }
}
