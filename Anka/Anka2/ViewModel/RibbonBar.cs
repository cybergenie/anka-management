using Anka2.MVVM;
using Anka2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Anka2.ViewModel
{
    public class RibbonBar
    {
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
