using MercuryMessenger.Interfaces;
using System.Windows.Input;

namespace MercuryMessenger.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ISubscriber _subscriber;

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

        private PrimaryViewModel _primaryDataContext;
        public PrimaryViewModel PrimaryDataContext
        {
            get { return _primaryDataContext; }
            set
            {
                if (_primaryDataContext != value)
                {
                    _primaryDataContext = value;
                    NotifyPropertyChanged(nameof(PrimaryDataContext));
                }
            }
        }

        private SecondaryViewModel _secondaryDataContext;
        public SecondaryViewModel SecondaryDataContext
        {
            get { return _secondaryDataContext; }
            set
            {
                if (_secondaryDataContext != value)
                {
                    _secondaryDataContext = value;
                    NotifyPropertyChanged(nameof(SecondaryDataContext));
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

        public MainWindowViewModel(ISubscriber subscriber)
        {
            this._subscriber = subscriber;

            SetupViewModelsForUserControls();
        }

        private void SetupViewModelsForUserControls()
        {
            this.PrimaryDataContext = new PrimaryViewModel(_subscriber);
            this.SecondaryDataContext = new SecondaryViewModel(_subscriber);
        }

        private void SendButton_Clicked(object str)
        {
            MessegingCenter.MercuryMessenger.Messenger.Send(str.ToString());
        }
    }
}
