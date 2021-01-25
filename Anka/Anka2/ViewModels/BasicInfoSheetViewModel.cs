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
                
        /// <summary>
        /// 危险状态其他项激活状态
        /// </summary>
        //private bool _risk13Actived = false;
        //public bool Risk13Actived
        //{
        //    get => _risk13Actived;
        //    set
        //    {
        //        if (_risk13Actived != value)
        //        {
        //            _risk13Actived = value;
        //            if(_risk13Actived == false)
        //            {
        //                Risk13 = "";
        //            }
        //            RaisePropertyChanged(nameof(Risk13Actived));
        //        }
        //    }
        //}
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
