﻿using Anka2.Models;
using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Anka2.ViewModels
{
    public class IPAQSheetViewModel : NotifyObject, IStatusInfoService
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
        private bool _ipaqContentEnable = false;
        public bool IPAQContentEnable
        {
            get => _ipaqContentEnable;
            set
            {
                if (_ipaqContentEnable != value)
                {
                    _ipaqContentEnable = value;                    
                }
                RaisePropertyChanged(nameof(IPAQContentEnable));
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

        public int IPAQIndex { get; set; } = -1;

        public IPAQ IPAQContent
        {
            get
            {
                if (IPAQIndex >= 0)
                {
                    try
                    {
                        if (BasicInfo.PIPAQ is not null)
                            return BasicInfo.PIPAQ[IPAQIndex];
                        else
                            return null;
                    }
                    catch(IndexOutOfRangeException e)
                    {
                        MessageBox.Show("返回值错误！错误信息为:" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
                else
                    return null;
            }
            set
            {
                if (IPAQIndex >= 0)
                {
                    try
                    {
                        if (BasicInfo.PIPAQ is not null)
                            BasicInfo.PIPAQ[IPAQIndex] = value;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        MessageBox.Show("返回值错误！错误信息为:" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                RaisePropertyChanged(nameof(IPAQContent));
            }
        }

        private string _ipaqNumberText;
        public string IPAQNumberText
        {
            get
            {
                return _ipaqNumberText;
            }
            set
            {
                if (_ipaqNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _ipaqNumberText = value;

                }
                RaisePropertyChanged(nameof(IPAQNumberText));
            }
        }

        private CommandObject<SelectionChangedEventArgs> _selection_IPAQ_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_IPAQ_Executed
        {
            get
            {
                if (_selection_IPAQ_Executed == null)
                    _selection_IPAQ_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count > 0)
                            {
                                IPAQNumberText = e.AddedItems[0].ToString();
                            }
                            loadIPAQContent();
                        }));
                return _selection_IPAQ_Executed;
            }
        }

        private void loadIPAQContent()
        {
            if (!string.IsNullOrEmpty(IPAQNumberText))
            {
                if (BasicInfo is not null)
                {
                    IPAQContentEnable = true;
                    if (BasicInfo.PIPAQ is null)
                    {
                        List<IPAQ> ipaq = new List<IPAQ>();
                        BasicInfo.PIPAQ = ipaq;
                    }
                    IPAQIndex = BasicInfo.PIPAQ.FindIndex((IPAQ e) => e.IPAQNumber == BasicInfo.Number + "-" + IPAQNumberText);
                    if (IPAQIndex >= 0)
                    {
                        IPAQContent = BasicInfo.PIPAQ[IPAQIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "GAD记录加载成功。记录编号为：" + IPAQNumberText + "。");
                    }
                    else
                    {
                        var ipaqContent = new IPAQ { IPAQNumber = BasicInfo.Number + "-" + IPAQNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.PIPAQ.Add(ipaqContent);
                        BasicInfo = BasicInfo;
                        IPAQIndex = BasicInfo.PIPAQ.FindIndex((IPAQ e) => e.IPAQNumber == BasicInfo.Number + "-" + IPAQNumberText);
                        IPAQContent = BasicInfo.PIPAQ[IPAQIndex];

                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的IPAQ记录创建成功。记录编号为：" + IPAQNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private CommandObject<RoutedEventArgs> _load_IPAQ_Executed;
        public CommandObject<RoutedEventArgs> Load_IPAQ_Executed
        {
            get
            {
                if (_load_IPAQ_Executed == null)
                    _load_IPAQ_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadIPAQContent();
                        }));
                return _load_IPAQ_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _save_IPAQ_Executed;
        public CommandObject<RoutedEventArgs> Save_IPAQ_Executed
        {
            get
            {
                if (_save_IPAQ_Executed == null)
                    _save_IPAQ_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (IPAQContent != null)
                            {
                                bool saveInfo = DataUitls.SaveIPAQ(IPAQContent);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "的档案保存成功。档案编号为：" + IPAQContent.IPAQNumber);
                            }
                        }));
                return _save_IPAQ_Executed;
            }
        }
    }    
}
