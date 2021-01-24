using Anka2.Models;
using Anka2.Services;
using Anka2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anka2.Views
{
    /// <summary>
    /// NewPerson.xaml 的交互逻辑
    /// </summary>
    public partial class NewPerson : Window
    {
        public NotifyNewPersonHandler NotifyNewPerson;
        public NewPerson()
        {
            InitializeComponent(); 
        }
        public void AddNewPerson(NotifyNewPersonHandler notify )
        {
            NotifyNewPerson += notify;
        }        

        private void NewPerson_Click(object sender, RoutedEventArgs e)
        {
            NewPersonViewModel newPersonContext = this.DataContext as NewPersonViewModel;
            BasicInfo newPerson = new BasicInfo { Number = newPersonContext.PersonId, Name = newPersonContext.PersonName, Age = newPersonContext.PersonAge };
            IDataService dataService = new DataService();
            bool addInfo = dataService.AddPerson(newPerson);
            if (addInfo == true)
            {
                NotifyNewPerson(newPerson);
                var rootWindow = dataService.GetParentWindow((DependencyObject)e.Source);
                rootWindow.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
