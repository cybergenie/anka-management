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

        private CommandObject<RoutedEventArgs> _export_Executed;
        public CommandObject<RoutedEventArgs> Export_Executed
        {
            get
            {
                if (_export_Executed == null)
                    _export_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            RootSource = DataUitls.GetParentWindow((DependencyObject)e.Source);
                            ExportPreview ExportPreview = new ExportPreview();
                            //newPerson.AddNewPerson(this.GetNewPersonInfo);
                            ExportPreview.Show();
                        }));
                return _export_Executed;
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
        /// <summary>
        /// 将创建的NewPerson值返回到MainWindow
        /// </summary>
        /// <param name="value"></param>
        private void GetNewPersonInfo(BasicInfo value)
        {
            NewPersonInfo = value;
            SheetsActived();
            NotifyStatusInfo(InfoType.Success, NewPersonInfo.Name + "的档案新建成功。档案编号为：" + NewPersonInfo.Number);
        }

        /// <summary>
        /// 激活各个Sheets可以录入具体信息
        /// </summary>
        private void SheetsActived()
        {
            StatusBarViewModel statusBarContext = ((MainWindow)RootSource).StatusBar.DataContext as StatusBarViewModel;

            this.UpDateStatusInfo(statusBarContext.SetTipInfo);

            BasicInfoSheetViewModel basicInfoSheetContext = ((MainWindow)RootSource).BasicInfoSheet.DataContext as BasicInfoSheetViewModel;
            basicInfoSheetContext.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定basicInfoSheet的状态栏信息更新
            basicInfoSheetContext.BasicInfoContentEnable = true;//激活basicInfoSheetContext信息录入
            basicInfoSheetContext.BasicInfo = NewPersonInfo;//传递NewPerson
        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }
    }
}
