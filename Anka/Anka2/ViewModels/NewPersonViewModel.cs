using Anka2.Models;
using Anka2.Services;
using System;
using System.Windows;
using System.Windows.Media;

namespace Anka2.ViewModels
{
    public class NewPersonViewModel : NotifyObject
    {        
        public event SendNewPerson SendNewPerson;

        /// <summary>
        /// 档案号
        /// </summary>
        private string _personId;
        public string PersonId
        {
            get => _personId;
            set
            {
                if (_personId != value)
                {                  
                    _personId = value;
                    RaisePropertyChanged(nameof(PersonId));
                }
            }

        }
        /// <summary>
        /// 姓名
        /// </summary>
        private string _personName;
        public string PersonName
        {
            get => _personName;
            set
            {
                if (_personName != value)
                {
                    _personName = value;
                    RaisePropertyChanged(nameof(PersonName));
                }
            }

        }

        private string _personAge;
        public string PersonAge
        {
            get => _personAge;
            set
            {
                if (_personAge != value)
                {
                    _personAge = value;
                    RaisePropertyChanged(nameof(PersonAge));
                }
            }

        }

       

        /// <summary>
        /// 新建档案事件
        /// </summary>
        private CommandObject<RoutedEventArgs> _newPerson_Click;
        public CommandObject<RoutedEventArgs> NewPerson_Click
        {
            get
            {
                if (_newPerson_Click == null)
                    _newPerson_Click = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            BasicInfo newPerson = new BasicInfo { Number = PersonId, Name = PersonName, Age = PersonAge };
                            IDataService dataService = new DataService(); 
                            bool addInfo = dataService.AddPerson(newPerson);
                            if (addInfo == true)
                            {
                                DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject)e.Source);
                                while (parent != null)
                                {
                                    if (parent is Window)
                                    {
                                        break;
                                    }
                                    parent = VisualTreeHelper.GetParent(parent);
                                }                                
                                SendNewPerson(newPerson);
                                ((Window)parent).Close();
                            }
                        }));
                return _newPerson_Click;
            }
        }

        private CommandObject<RoutedEventArgs> _cancel_Click;
        public CommandObject<RoutedEventArgs> Cancel_Click
        {
            get
            { 
                if (_cancel_Click == null)
                    _cancel_Click = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            DependencyObject parent = VisualTreeHelper.GetParent((DependencyObject)e.Source);
                            while (parent != null)
                            {
                                if (parent is Window)
                                {
                                    break;
                                }
                                parent = VisualTreeHelper.GetParent(parent);
                            }

                           ((Window)parent).Close();
                        }));
                return _cancel_Click;
            }
        }
    }
}
