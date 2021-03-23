using Anka2.Models;
using Anka2.Services;
using Anka2.Views;
using System;
using System.IO;
using System.Threading.Tasks;
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

        private CommandObject<RoutedEventArgs> _preview_Executed;
        public CommandObject<RoutedEventArgs> Preview_Executed
        {
            get
            {
                if (_preview_Executed == null)
                    _preview_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            RootSource = DataUitls.GetParentWindow((DependencyObject)e.Source);
                            ExportPreview ExportPreview = new ExportPreview();
                            //newPerson.AddNewPerson(this.GetNewPersonInfo);
                            ExportPreview.Show();
                        }));
                return _preview_Executed;
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
                            if (NewPersonInfo != null)
                            {
                                bool saveInfo = DataUitls.SavePersonInfo(NewPersonInfo);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, NewPersonInfo.Name + "的档案保存成功。档案编号为：" + NewPersonInfo.Number);
                            }
                        }));
                return _save_Executed;
            }
        }


       


        private CommandObject<RoutedEventArgs> _backup_Executed;
        public CommandObject<RoutedEventArgs> Backup_Executed
        {
            get
            {
                if (_backup_Executed == null)
                    _backup_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>( e =>
                        {
                            RootSource = DataUitls.GetParentWindow((DependencyObject)e.Source);
                            StatusBarViewModel statusBarContext = ((MainWindow)RootSource).StatusBar.DataContext as StatusBarViewModel;
                            this.UpDateStatusInfo(statusBarContext.SetTipInfo);

                            NotifyStatusInfo(InfoType.Info, "开始备份数据库。");
                            var backupResult = DataUitls.BackupFile("anka.db", ".\\DataBase\\");
                            if(backupResult is not null)
                            {
                                NotifyStatusInfo(InfoType.Info, "数据文件" + backupResult + "备份完成。");
                            }
                            else
                            {
                                NotifyStatusInfo(InfoType.Info, "数据文件备份失败。");
                            }                                                                             
                            
                        }));
                return _backup_Executed;
            }
        }


        private CommandObject<RoutedEventArgs> _repair_Executed;
        public CommandObject<RoutedEventArgs> Repair_Executed
        {
            get
            {
                if (_repair_Executed == null)
                    _repair_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(async e =>
                        {
                            RootSource = DataUitls.GetParentWindow((DependencyObject)e.Source);
                            StatusBarViewModel statusBarContext = ((MainWindow)RootSource).StatusBar.DataContext as StatusBarViewModel;
                            this.UpDateStatusInfo(statusBarContext.SetTipInfo);
                            RepairInfo repairInfo;
                            try
                            {
                                NotifyStatusInfo(InfoType.Info, "开始备份数据库。");
                                var backupFile = DataUitls.BackupFile("anka.db", ".\\DataBase\\");
                                if (backupFile is not null)
                                {
                                    NotifyStatusInfo(InfoType.Info, "数据文件" + backupFile + "备份成功。");
                                    repairInfo = new RepairInfo();
                                    await Task.Run(() =>
                                    {
                                        App.Current.Dispatcher.Invoke((Action)(() =>
                                        {
                                            repairInfo.Show();
                                        }));                                       

                                    });

                                   
                                }
                                else
                                {
                                    NotifyStatusInfo(InfoType.Info, "数据文件备份失败，无法进行数据修复。");
                                }
                                
                               
                            }
                            catch (Exception ex)
                            {                                
                                MessageBox.Show("数据修复失败,失败信息:\n" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            

                        }));
                return _repair_Executed;
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

            ExerciseSheetViewModel exerciseSheetViewModel = ((MainWindow)RootSource).ExerciseSheet.DataContext as ExerciseSheetViewModel;
            exerciseSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            exerciseSheetViewModel.ExerciseContent = null;
            exerciseSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            GADSheetViewModel gadSheetViewModel = ((MainWindow)RootSource).GADSheet.DataContext as GADSheetViewModel;
            gadSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            gadSheetViewModel.GADContent = null;
            gadSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            PHQSheetViewModel phqSheetViewModel = ((MainWindow)RootSource).PHQSheet.DataContext as PHQSheetViewModel;
            phqSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            phqSheetViewModel.PHQContent = null;
            phqSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            IPAQSheetViewModel ipaqSheetViewModel = ((MainWindow)RootSource).IPAQSheet.DataContext as IPAQSheetViewModel;
            ipaqSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            ipaqSheetViewModel.IPAQContent = null;
            ipaqSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            OHQSheetViewModel ohqSheetViewModel = ((MainWindow)RootSource).OHQSheet.DataContext as OHQSheetViewModel;
            ohqSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            ohqSheetViewModel.OHQContent = null;
            ohqSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            SPPBSheetViewModel sppbSheetViewModel = ((MainWindow)RootSource).SPPBSheet.DataContext as SPPBSheetViewModel;
            sppbSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            sppbSheetViewModel.SPPBContent = null;
            sppbSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

            PhysiqueSheetViewModel physiqueSheetViewModel = ((MainWindow)RootSource).PhysiqueSheet.DataContext as PhysiqueSheetViewModel;
            physiqueSheetViewModel.UpDateStatusInfo(statusBarContext.SetTipInfo);//绑定exerciseSheet的状态栏信息更新
            physiqueSheetViewModel.PhysiqueContent = null;
            physiqueSheetViewModel.BasicInfo = NewPersonInfo;//传递NewPerson

        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }
    }
}
