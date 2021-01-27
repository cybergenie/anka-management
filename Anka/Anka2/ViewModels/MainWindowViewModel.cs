using Anka2.Models;
using Anka2.Services;
using Anka2.Views;
using System;
using System.Windows;

namespace Anka2.ViewModels
{
    public delegate void NotifyNewPersonHandler(BasicInfo newPerson);
    public delegate void NotifyStatusInfoHandler(InfoType Type, string Info);

    public class MainWindowViewModel : NotifyObject, IStatusInfoService
    {
        public NotifyStatusInfoHandler NotifyStatusInfo;
        private BasicInfo NewPersonInfo { set; get; }
        private Window RootSource { set; get; }


        private CommandObject<RoutedEventArgs> _new_Executed;
        public CommandObject<RoutedEventArgs> New_Executed
        {
            get
            {
                if (_new_Executed == null)
                    _new_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {                           
                            RootSource = DataUitls.GetParentWindow((DependencyObject)e.Source);
                            NewPerson newPerson = new NewPerson();                            
                            newPerson.AddNewPerson(this.GetNewPersonInfo);                            
                            newPerson.Show();
                        }));
                return _new_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _save_Executed;
        public CommandObject<RoutedEventArgs> Save_Executed
        {
            get
            {
                if (_save_Executed == null)
                    _save_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            bool saveInfo = DataUitls.SavePersonInfo(NewPersonInfo);
                            if (saveInfo == true)
                                NotifyStatusInfo(InfoType.Success, NewPersonInfo.Name+"的档案保存成功。档案编号为："+NewPersonInfo.Number);
                        }));
                return _save_Executed;
            }
        }

        private void GetNewPersonInfo(BasicInfo value)
        {
            NewPersonInfo = value;
            SheetsActived();
            NotifyStatusInfo(InfoType.Success, NewPersonInfo.Name + "的档案新建成功。档案编号为：" + NewPersonInfo.Number);
        }

        private void SheetsActived()
        {
            StatusBarViewModel statusBarContext = ((MainWindow)RootSource).StatusBar.DataContext as StatusBarViewModel;

            this.UpDateStatusInfo(statusBarContext.SetTipInfo);

            BasicInfoSheetViewModel basicInfoSheetContext = ((MainWindow)RootSource).BasicInfoSheet.DataContext as BasicInfoSheetViewModel;
            basicInfoSheetContext.UpDateStatusInfo(statusBarContext.SetTipInfo);
            basicInfoSheetContext.BasicInfoContentEnable = true;
            basicInfoSheetContext.BasicInfo = NewPersonInfo;
        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }
    }
}
