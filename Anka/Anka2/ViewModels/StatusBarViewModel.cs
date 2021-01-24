using Anka2.Models;
using Anka2.Services;
using System.Windows;

namespace Anka2.ViewModels
{
    class StatusBarViewModel : NotifyObject
    {        
        private string _connectedDataBase;
        public string ConnectedDataBase 
        {
            get => _connectedDataBase;
            set
            {
                if (_connectedDataBase != value)
                {
                    _connectedDataBase = value;
                    RaisePropertyChanged(nameof(ConnectedDataBase));
                }
            }
        }

        private string _tipInfo;
        public string TipInfo
        {
            get => _tipInfo;
            set
            {
                if (_tipInfo != value)
                {
                    _tipInfo = value;
                    RaisePropertyChanged(nameof(TipInfo));
                }
            }
        }

        public void SetTipInfo(InfoType Type,string Value)
        {
            TipInfo = Value;            
        }
    }
}
