using Anka2.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.ViewModel
{
    class StatusBar:NotifyObject
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

    }
}
