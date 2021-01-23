using Anka2.Models;
using Anka2.Views;
using System;
using System.Windows;

namespace Anka2.ViewModels
{
    public delegate void SetTipInfo(string str);
    public delegate void SendNewPerson(BasicInfo newPerson);
    class MainWindowViewModel : NotifyObject
    {
        public BasicInfo NewPerson { set; get; }

        private CommandObject<RoutedEventArgs> _new_Executed;
        public CommandObject<RoutedEventArgs> New_Executed
        {
            get
            {
                if (_new_Executed == null)
                    _new_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            GetNewPerson(NewPerson);
                            NewPerson newPerson = new NewPerson();
                            ((NewPersonViewModel)newPerson.DataContext).SendNewPerson += this.GetNewPerson;
                            newPerson.Show();
                        }));
                return _new_Executed;
            }
        }

        private void GetNewPerson(BasicInfo newPerson)
        {
            NewPerson = newPerson;
        }
    }
}
