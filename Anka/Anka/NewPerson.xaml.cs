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
            bool isClose = true;
            string Male = null;

            if(this.txName.Text.Trim().Length<=0)
            {
                MessageBox.Show("请确认输入姓名。");
                isClose = false;
            }

            if (this.txNumber.Text.Trim().Length != 8|| DataAdapter.IsNumber(this.txAge.Text) == false)
            {
                MessageBox.Show("请确认输入八位数字。");
                isClose = false;
            }
            
            else if (Convert.ToInt32(this.txAge.Text) <= 0 || Convert.ToInt32(this.txAge.Text) >= 150)
            {
                MessageBox.Show("请确认输入年龄正确。");
                isClose = false;
            }

            if (rbMale.IsChecked==true)
            {
                Male = "男";
            }
            else if(rbFemale.IsChecked==true)
            {
                Male = "女";
            }
            else
            {
                MessageBox.Show("请选择性别。");
                isClose = false;
            }
            if (isClose == true)
            {
                DataAdapter.Number = this.txNumber.Text;
                DataAdapter.Age = Convert.ToInt32(this.txAge.Text);
                DataAdapter.Male = Male;

                DataAdapter.Name = this.txName.Text.ToString().Trim(' ');
            }

            if(isClose==true)
            {
                this.Close();
            }
            
        }
    }
}
