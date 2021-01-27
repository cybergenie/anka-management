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

        public bool Risk1IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 0);
                else
                    return false;
            }
            set
            {
                if (Risk1IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 0, value);                    
                    RaisePropertyChanged(nameof(Risk1IsChecked));
                }
            }
        }

        public bool Risk2IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 1);
                else
                    return false;
            }
            set
            {
                if (Risk2IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 1, value);
                    RaisePropertyChanged(nameof(Risk2IsChecked));
                }
            }
        }

        public bool Risk3IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 2);
                else
                    return false;
            }
            set
            {
                if (Risk3IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 2, value);
                    RaisePropertyChanged(nameof(Risk3IsChecked));
                }
            }
        }
        public bool Risk4IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 3);
                else
                    return false;
            }
            set
            {
                if (Risk4IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 3, value);
                    RaisePropertyChanged(nameof(Risk4IsChecked));
                }
            }
        }

        public bool Risk5IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 4);
                else
                    return false;
            }
            set
            {
                if (Risk5IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 4, value);
                    RaisePropertyChanged(nameof(Risk5IsChecked));
                }
            }
        }

        public bool Risk6IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 5);
                else
                    return false;
            }
            set
            {
                if (Risk6IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 5, value);
                    RaisePropertyChanged(nameof(Risk6IsChecked));
                }
            }
        }

        public bool Risk7IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 6);
                else
                    return false;
            }
            set
            {
                if (Risk7IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 6, value);
                    RaisePropertyChanged(nameof(Risk7IsChecked));
                }
            }
        }

        public bool Risk8IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 7);
                else
                    return false;
            }
            set
            {
                if (Risk8IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 7, value);
                    RaisePropertyChanged(nameof(Risk8IsChecked));
                }
            }
        }

        public bool Risk9IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 8);
                else
                    return false;
            }
            set
            {
                if (Risk9IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 8, value);
                    RaisePropertyChanged(nameof(Risk9IsChecked));
                }
            }
        }

        public bool Risk10IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 9);
                else
                    return false;
            }
            set
            {
                if (Risk10IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 9, value);
                    RaisePropertyChanged(nameof(Risk10IsChecked));
                }
            }
        }

        public bool Risk11IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 10);
                else
                    return false;
            }
            set
            {
                if (Risk11IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 10, value);
                    RaisePropertyChanged(nameof(Risk11IsChecked));
                }
            }
        }

        public bool Risk12IsChecked
        {
            get
            {
                if (BasicInfo != null)
                    return DataUitls.BasicRisk2Checked(BasicInfo.BasicRisk, 11);
                else
                    return false;
            }
            set
            {
                if (Risk12IsChecked != value)
                {
                    BasicInfo.BasicRisk = DataUitls.Checked2BasicRisk(BasicInfo.BasicRisk, 11, value);
                    RaisePropertyChanged(nameof(Risk12IsChecked));
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
