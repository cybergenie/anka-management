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

namespace Anka
{
    /// <summary>
    /// NewPerson.xaml 的交互逻辑
    /// </summary>
    public partial class NewPerson : Window
    {
        public NewPerson()
        {
            InitializeComponent();
        }

        private void BtNew_Click(object sender, RoutedEventArgs e)
        {
            Boolean isClose = true;
            int Number = 0;
            int Age = 0;
            Boolean Male = false;//false表示女，True表示男

            if (DataAdapter.ToInt(this.txNumber.Text,out Number)==false)
            {
                MessageBox.Show("请确认输入八位数字。");
                isClose = false;
            }

            if (DataAdapter.ToInt(this.txAge.Text, out Age) == false)
            {
                MessageBox.Show("请确认输入的是数字。");
                isClose = false;
            }
            else if(Age<=0||Age>=150)
            {
                MessageBox.Show("请确认输入年龄正确。");
                isClose = false;
            }

            if(rbMale.IsChecked==true)
            {
                Male = true;
            }
            else if(rbFemale.IsChecked==true)
            {
                Male = false;
            }
            else
            {
                MessageBox.Show("请选择性别。");
                isClose = false;
            }

            DataAdapter.Number = Number;
            DataAdapter.Age = Age;
            DataAdapter.Male = Male;
            
            DataAdapter.Name = this.txName.Text.ToString().Trim(' ');  

            if(isClose==true)
            {
                this.Close();
            }
            
        }
    }
}
