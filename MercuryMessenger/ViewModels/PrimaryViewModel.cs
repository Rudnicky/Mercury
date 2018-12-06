﻿using MercuryMessenger.Interfaces;
using System.Windows.Input;

namespace MercuryMessenger.ViewModels
{
    public class PrimaryViewModel : BaseViewModel
    {
        private readonly ISubscriber _subscriber;

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

        private string _buttonContent;
        public string ButtonContent
        {
            get { return _buttonContent; }
            set
            {
                if (_buttonContent != value)
                {
                    _buttonContent = value;
                    NotifyPropertyChanged(nameof(ButtonContent));
                }
            }
        }

        private ICommand _sendButtonClickedCommand;
        public ICommand RegisterButtonClickedCommand
        {
            get
            {
                return _sendButtonClickedCommand ?? (_sendButtonClickedCommand = new RelayCommand.RelayCommand(x =>
                {
                    RegisterButton_Clicked();
                }));
            }
        }

        public PrimaryViewModel(ISubscriber subscriber)
        {
            this._subscriber = subscriber;
            this._subscriber.Subscribe(this, MessageReceived);
            this.ButtonContent = "Unregister";
        }

        private void RegisterButton_Clicked()
        {
            if (this.ButtonContent.Equals("Register"))
            {
                _subscriber.Subscribe(this, MessageReceived);
                this.ButtonContent = "Unregister";
            }
            else
            {
                _subscriber.Unsubscribe(this);
                this.ButtonContent = "Register";
            }
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
