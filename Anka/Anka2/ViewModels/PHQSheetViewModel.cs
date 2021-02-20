﻿using Anka2.Models;
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
    public class PHQSheetViewModel : NotifyObject, IStatusInfoService
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

        private bool _phqContentEnable = false;
        public bool PHQContentEnable
        {
            get => _phqContentEnable;
            set
            {
                if (_phqContentEnable != value)
                {
                    _phqContentEnable = value;
                    RaisePropertyChanged(nameof(PHQContentEnable));
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

        private int _phqIndex = -1;
        public int PHQIndex
        {
            get
            {
                return _phqIndex;
            }
            set
            {
                _phqIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public PHQ PHQContent
        {
            get
            {
                if (PHQIndex >= 0)
                {
                    return BasicInfo.PPHQ[PHQIndex];
                }
                else
                    return null;
            }
            set
            {
                if (PHQIndex >= 0)
                {
                    BasicInfo.PPHQ[PHQIndex] = value;
                }
                RaisePropertyChanged(nameof(PHQContent));
            }
        }


        private string _phqNumberText;
        public string PHQNumberText
        {
            get
            {
                return _phqNumberText;
            }
            set
            {
                if (_phqNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _phqNumberText = value;

                }
                RaisePropertyChanged(nameof(PHQNumberText));
            }
        }

        private CommandObject<RoutedEventArgs> _load_PHQ_Executed;
        public CommandObject<RoutedEventArgs> Load_PHQ_Executed
        {
            get
            {
                if (_load_PHQ_Executed == null)
                    _load_PHQ_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (!string.IsNullOrEmpty(PHQNumberText))
                            {
                                if (BasicInfo is not null)
                                {
                                    PHQContentEnable = true;
                                    if (BasicInfo.PPHQ is null)
                                    {
                                        List<PHQ> phq = new List<PHQ>();
                                        BasicInfo.PPHQ = phq;
                                    }
                                    PHQIndex = BasicInfo.PPHQ.FindIndex((PHQ e) => e.PHQNumber == BasicInfo.Number + "-" + PHQNumberText);
                                    if (PHQIndex >= 0)
                                    {
                                        PHQContent = BasicInfo.PPHQ[PHQIndex];
                                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "PHQ记录加载成功。记录编号为：" + PHQNumberText + "。");
                                    }
                                    else
                                    {
                                        var phqContent = new PHQ { PHQNumber = BasicInfo.Number + "-" + PHQNumberText, basicinfoNumber = BasicInfo.Number };
                                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                                        BasicInfo.PPHQ.Add(phqContent);
                                        BasicInfo = BasicInfo;
                                        PHQIndex = BasicInfo.PPHQ.FindIndex((PHQ e) => e.PHQNumber == BasicInfo.Number + "-" + PHQNumberText);
                                        PHQContent = BasicInfo.PPHQ[PHQIndex];

                                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的PHQ记录创建成功。记录编号为：" + PHQNumberText + "。");
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                            }
                        }));
                return _load_PHQ_Executed;
            }
        }
    }
}