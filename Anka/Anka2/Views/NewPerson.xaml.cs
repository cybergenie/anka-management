using Anka2.Models;
using Anka2.Services;
using Anka2.ViewModels;
using System.Windows;

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
        public void AddNewPerson(NotifyNewPersonHandler notify)
        {
            NotifyNewPerson += notify;
        }

        private void NewPerson_Click(object sender, RoutedEventArgs e)
        {
            NewPersonViewModel newPersonContext = this.DataContext as NewPersonViewModel;
            BasicInfo newPerson = new BasicInfo { Number = newPersonContext.PersonId,Male = newPersonContext.PersonGender, Name = newPersonContext.PersonName, Age = newPersonContext.PersonAge };
            IDataUitls dataService = new DataUitls();
            bool addInfo = dataService.AddPerson(newPerson);
            if (addInfo == true)
            {
                NotifyNewPerson(newPerson);               
                this.Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
