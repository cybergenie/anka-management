using Anka2.Models;
using Anka2.Services;
using Anka2.ViewModels;
using System.Windows.Media;
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
            if (DataUitls.IsPersonId(this.txPersonID.Text))
            {
                NewPersonViewModel newPersonContext = this.DataContext as NewPersonViewModel;
                BasicInfo newPerson = new BasicInfo
                {
                    Number = newPersonContext.PersonId,
                    Male = newPersonContext.PersonGender,
                    Name = newPersonContext.PersonName,
                    Age = newPersonContext.PersonAge
                };
                NewPersonResult addInfo = DataUitls.AddNewPerson(ref newPerson);
                if (addInfo != NewPersonResult.Error)
                {
                    NotifyNewPerson(newPerson);
                    this.Close();
                }
            }
            else
                MessageBox.Show("档案号输入错误，请重新输入。","错误", MessageBoxButton.OK,MessageBoxImage.Error);

            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txPersonID_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (DataUitls.IsPersonId(this.txPersonID.Text))
            {
                NewPersonViewModel newPersonContext = this.DataContext as NewPersonViewModel;
                BasicInfo newPerson = new BasicInfo
                {
                    Number = newPersonContext.PersonId
                   
                };
                bool addInfo = DataUitls.CheckNewPerson(ref newPerson);
                if (addInfo == true)
                {
                    newPersonContext.PersonGender = newPerson.Male is null ? false : (bool)newPerson.Male;
                    newPersonContext.PersonName = newPerson.Name;
                    newPersonContext.PersonAge = newPerson.Age;                    
                    this.btNewPerson.Content = "读  取";
                    this.btNewPerson.Background = Brushes.PaleGreen;
                }
                else if(addInfo == false)
                {
                    newPersonContext.PersonGender = false;
                    newPersonContext.PersonName = string.Empty;
                    newPersonContext.PersonAge = string.Empty;                    
                    this.btNewPerson.Content = "新  建";
                    this.btNewPerson.Background = this.btCancel.Background;
                }
            }
        }
    }
}
