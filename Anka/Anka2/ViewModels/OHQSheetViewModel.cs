using Anka2.Models;
using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Anka2.ViewModels
{
    public class OHQSheetViewModel : NotifyObject, IStatusInfoService
    {
        public NotifyStatusInfoHandler NotifyStatusInfo;
        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
        }

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

        private bool _ohqContentEnable = false;
        public bool OHQContentEnable
        {
            get => _ohqContentEnable;
            set
            {
                if (_ohqContentEnable != value)
                {
                    _ohqContentEnable = value;
                    RaisePropertyChanged(nameof(OHQContentEnable));
                }
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

        private int _ohqIndex = -1;
        public int OHQIndex
        {
            get
            {
                return _ohqIndex;
            }
            set
            {
                _ohqIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public OHQ OHQContent
        {
            get
            {
                if (OHQIndex >= 0)
                {
                    return BasicInfo.POHQ[OHQIndex];
                }
                else
                    return null;
            }
            set
            {
                if (OHQIndex >= 0)
                {
                    BasicInfo.POHQ[OHQIndex] = value;
                }
                RaisePropertyChanged(nameof(OHQContent));
            }
        }


        private string _ohqNumberText;
        public string OHQNumberText
        {
            get
            {
                return _ohqNumberText;
            }
            set
            {
                if (_ohqNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _ohqNumberText = value;

                }
                RaisePropertyChanged(nameof(OHQNumberText));
            }
        }

        private CommandObject<SelectionChangedEventArgs> _selection_OHQ_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_OHQ_Executed
        {
            get
            {
                if (_selection_OHQ_Executed == null)
                    _selection_OHQ_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count > 0)
                            {
                                OHQNumberText = e.AddedItems[0].ToString();
                            }
                            loadOHQContent();
                        }));
                return _selection_OHQ_Executed;
            }
        }

        private void loadOHQContent()
        {
            if (!string.IsNullOrEmpty(OHQNumberText))
            {
                if (BasicInfo is not null)
                {
                    OHQContentEnable = true;
                    if (BasicInfo.POHQ is null)
                    {
                        List<OHQ> ohq = new List<OHQ>();
                        BasicInfo.POHQ = ohq;
                    }
                    OHQIndex = BasicInfo.POHQ.FindIndex((OHQ e) => e.OHQNumber == BasicInfo.Number + "-" + OHQNumberText);
                    if (OHQIndex >= 0)
                    {
                        OHQContent = BasicInfo.POHQ[OHQIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "口腔卫生记录加载成功。记录编号为：" + OHQNumberText + "。");
                    }
                    else
                    {
                        var ohqContent = new OHQ { OHQNumber = BasicInfo.Number + "-" + OHQNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.POHQ.Add(ohqContent);
                        BasicInfo = BasicInfo;
                        OHQIndex = BasicInfo.POHQ.FindIndex((OHQ e) => e.OHQNumber == BasicInfo.Number + "-" + OHQNumberText);
                        OHQContent = BasicInfo.POHQ[OHQIndex];

                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的口腔卫生记录创建成功。记录编号为：" + OHQNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private CommandObject<RoutedEventArgs> _load_OHQ_Executed;
        public CommandObject<RoutedEventArgs> Load_OHQ_Executed
        {
            get
            {
                if (_load_OHQ_Executed == null)
                    _load_OHQ_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadOHQContent();
                        }));
                return _load_OHQ_Executed;
            }
        }
    }
}
