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
            get=>_basicInfo;
            set
            {
                if(_basicInfo!=value)
                {
                    _basicInfo = value;
                    RaisePropertyChanged(nameof(BasicInfo));
                }
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
    }
}
