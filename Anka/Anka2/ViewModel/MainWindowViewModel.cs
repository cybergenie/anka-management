using Anka2.Model;
using Anka2.MVVM;
using Anka2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.ViewModel
{
    class MainWindowViewModel:NotifyObject
    {
        private ObservableCollection<DbAdapter> _dbAdapter;
        public ObservableCollection<DbAdapter> DbAdapter
        {
            get => _dbAdapter;
            set
            {
                if (_dbAdapter != value)
                {
                    _dbAdapter = value;
                    RaisePropertyChanged(nameof(DbAdapter));
                }
            }
        }
        private NewPersonViewModel _newPerson;
        public NewPersonViewModel NewPerson
        {
            get => _newPerson;
            set
            {
                if (_newPerson != value)
                {
                    _newPerson = value;
                    RaisePropertyChanged(nameof(NewPerson));
                }
            }
        }
        private StatusBar _statusBar;
        public StatusBar StatusBar 
        {
            get => _statusBar;
            set
            {
                if (_statusBar != value)
                {
                    _statusBar = value;
                    RaisePropertyChanged(nameof(StatusBar));
                }
            }
        }

        public MainWindowViewModel()
        {
            DbAdapter = new ObservableCollection<DbAdapter>();
            NewPerson = new NewPersonViewModel();
        }

        private CommandObject<RoutedEventArgs> _new_Executed;
        public CommandObject<RoutedEventArgs> New_Executed
        {
            get
            {
                if (_new_Executed == null)
                    _new_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            NewPerson newPerson = new NewPerson();                           
                            newPerson.ShowDialog();
                        }));
                return _new_Executed;
            }
        }
    }
}
