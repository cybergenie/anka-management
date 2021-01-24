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
        /// <summary>
        /// Description:基本信息诊断
        /// </summary>
        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if(_description!=value)
                {
                    _description = value;
                    NotifyStatusInfo(InfoType.Success, "基本信息-诊断为："+value);
                    RaisePropertyChanged(nameof(Description));                   
                }
            }
        }
        /// <summary>
        /// 危险状态其他项激活状态
        /// </summary>
        private bool _risk13Actived = false;
        public bool Risk13Actived
        {
            get => _risk13Actived;
            set
            {
                if (_risk13Actived != value)
                {
                    _risk13Actived = value;
                    if(_risk13Actived == false)
                    {
                        Risk13 = "";
                    }
                    RaisePropertyChanged(nameof(Risk13Actived));
                }
            }
        }
        /// <summary>
        /// 心功能Killip/NYHA项文本
        /// </summary>
        private string _killip;
        public string Killip
        {
            get => _killip;
            set
            {
                if (_killip != value)
                {
                    _killip = value;
                    RaisePropertyChanged(nameof(Killip));
                }
            }
        }
        /// <summary>
        /// 危险状态其他项文本
        /// </summary>
        private string _risk13;        
        public string Risk13
        {
            get => _risk13;
            set
            {
                if (_risk13 != value)
                {
                    _risk13 = value;
                    RaisePropertyChanged(nameof(Risk13));
                }
            }
        }        
        /// <summary>
        /// 危险状态其他项选中，激活输入框
        /// </summary>
        private CommandObject<RoutedEventArgs> _risk13Checked;
        public CommandObject<RoutedEventArgs> Risk13Checked
        {
            get
            {
                if (_risk13Checked == null)
                    _risk13Checked = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            Risk13Actived = true;
                        }));
                return _risk13Checked;
            }
        }
        /// <summary>
        /// 危险状态其他项取消选中，冻结输入框
        /// </summary>
        private CommandObject<RoutedEventArgs> _risk13Unchecked;
        public CommandObject<RoutedEventArgs> Risk13Unchecked
        {
            get
            {
                if (_risk13Unchecked == null)
                    _risk13Unchecked = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            Risk13Actived = false;
                        }));
                return _risk13Unchecked;
            }
        }
        /// <summary>
        /// 侧枝循环项选中，激活输入框
        /// </summary>
        private CommandObject<RoutedEventArgs> _cCChecked;
        public CommandObject<RoutedEventArgs> CCChecked
        {
            get
            {
                if (_cCChecked == null)
                    _cCChecked = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                           ((CheckBox)e.Source).Content= "侧枝循环：有";
                        }));
                return _cCChecked;
            }
        }
        /// <summary>
        /// 侧枝循环项取消选中，冻结输入框
        /// </summary>
        private CommandObject<RoutedEventArgs> _cCUnchecked;
        public CommandObject<RoutedEventArgs> CCUnchecked
        {
            get
            {
                if (_cCUnchecked == null)
                    _cCUnchecked = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            ((CheckBox)e.Source).Content = "侧枝循环：无";
                        }));
                return _cCUnchecked;
            }
        }
        /// <summary>
        /// 危险状态其他项文本
        /// </summary>
        private string _pCI;
        public string PCI
        {
            get => _pCI;
            set
            {
                if (_pCI != value)
                {
                    _pCI = value;

                    RaisePropertyChanged(nameof(PCI));
                }
            }
        }

        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }
    }
}
