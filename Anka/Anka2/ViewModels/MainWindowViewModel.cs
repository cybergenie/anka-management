using Anka2.Models;
using Anka2.Services;
using Anka2.Views;
using System;
using System.Windows;

namespace Anka2.ViewModels
{   
    public delegate void NotifyNewPersonHandler(BasicInfo newPerson);
    public delegate void NotifyStatusInfoHandler(InfoType Type, string Info);

    class MainWindowViewModel : NotifyObject, IStatusInfoService
    {
        public NotifyStatusInfoHandler NotifyStatusInfo;
        public BasicInfo NewPersonInfo { set; get; }
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
                            IDataService dataService = new DataService();
                            RootSource = dataService.GetParentWindow((DependencyObject)e.Source);
                            NewPerson newPerson = new NewPerson();
                            SheetsActived();
                            newPerson.AddNewPerson(this.GetNewPersonInfo);                            
                            newPerson.Show();
                        }));
                return _new_Executed;
            }
        }

        private void GetNewPersonInfo(BasicInfo value)
        {
            NewPersonInfo = value;           
            NotifyStatusInfo(InfoType.Success, "新建档案成功。");
        }

        private void SheetsActived()
        {
            StatusBarViewModel statusBarContext = ((MainWindow)RootSource).StatusBar.DataContext as StatusBarViewModel;

            this.UpDateStatusInfo(statusBarContext.SetTipInfo);

            BasicInfoSheetViewModel basicInfoSheetContext = ((MainWindow)RootSource).BasicInfoSheet.DataContext as BasicInfoSheetViewModel;
            basicInfoSheetContext.UpDateStatusInfo(statusBarContext.SetTipInfo);
            basicInfoSheetContext.BasicInfoContentEnable = true;
            
            
        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }
    }
}
