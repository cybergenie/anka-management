using Anka2.Models;
using Anka2.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Anka2.ViewModels
{
    public class BasicInfoSheetViewModel : NotifyObject, IStatusInfoService
    {
        public NotifyStatusInfoHandler NotifyStatusInfo;
        /// <summary>
        /// Enter键改变选框状态
        /// </summary>
        private CommandObject<KeyEventArgs> _keyDownCommand;
        public CommandObject<KeyEventArgs> KeyDownCommand
        {
            get
            {
                if (_keyDownCommand == null)
                    _keyDownCommand = new CommandObject<KeyEventArgs>(
                        new Action<KeyEventArgs>(e =>
                        {
                            if (e.Key == Key.Enter)
                            {
                                IInputElement focusedElement = Keyboard.FocusedElement;
                                if (focusedElement.GetType().Name == "CheckBox" || focusedElement.GetType().Name == "RadioButton")
                                {
                                    ((ToggleButton)focusedElement).IsChecked = !((ToggleButton)focusedElement).IsChecked;
                                }

                            }

                            if (e.Key == Key.Escape)
                            {
                                IInputElement focusedElement = Keyboard.FocusedElement;
                                ((Control)focusedElement).MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                            }

                        }));
                return _keyDownCommand;
            }
        }
        private BasicInfo _basicInfo = null;
        public BasicInfo BasicInfo
        {
            get => _basicInfo;
            set
            {
                if (_basicInfo != value)
                { 
                    _basicInfo = value;                    
                }
                RaisePropertyChanged(nameof(BasicInfo));
            }
        }

        /// <summary>
        /// 激活基本信息输入区域
        /// </summary>
        private bool _basicInfoContentEnable = false;
        public bool BasicInfoContentEnable
        {
            get => _basicInfoContentEnable;
            set
            {
                if (_basicInfoContentEnable != value)
                {
                    _basicInfoContentEnable = value;
                    RaisePropertyChanged(nameof(BasicInfoContentEnable));
                }
            }
        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }


        private void loadBasicInfoContent()
        {

            if (BasicInfo is not null)
            {
                var NewPersonContent = new BasicInfo { Number = BasicInfo.Number, Name = BasicInfo.Name, Age = BasicInfo.Age, Male = BasicInfo.Male };
                BasicInfo = NewPersonContent;
                NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的基本信息记录创建成功。记录编号为：" + BasicInfo.Number + "。");
            }
            else
            {
                MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private CommandObject<RoutedEventArgs> _load_BasicInfo_Executed;
        public CommandObject<RoutedEventArgs> Load_BasicInfo_Executed
        {
            get
            {
                if (_load_BasicInfo_Executed == null)
                    _load_BasicInfo_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadBasicInfoContent();
                        }));
                return _load_BasicInfo_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _save_BasicInfo_Executed;
        public CommandObject<RoutedEventArgs> Save_BasicInfo_Executed
        {
            get
            {
                if (_save_BasicInfo_Executed == null)
                    _save_BasicInfo_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (BasicInfo != null)
                            {
                                bool saveInfo = DataUitls.SaveBasicInfo(BasicInfo);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "的档案保存成功。档案编号为：" + BasicInfo.Number);
                            }
                        }));
                return _save_BasicInfo_Executed;
            }
        }

    }
}
