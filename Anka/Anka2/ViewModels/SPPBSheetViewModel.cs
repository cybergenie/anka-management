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
    public class SPPBSheetViewModel : NotifyObject, IStatusInfoService
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

        private bool _sppbContentEnable = false;
        public bool SPPBContentEnable
        {
            get => _sppbContentEnable;
            set
            {
                if (_sppbContentEnable != value)
                {
                    _sppbContentEnable = value;
                    RaisePropertyChanged(nameof(SPPBContentEnable));
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

        private int _sppbIndex = -1;
        public int SPPBIndex
        {
            get
            {
                return _sppbIndex;
            }
            set
            {
                _sppbIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public SPPB SPPBContent
        {
            get
            {
                if (SPPBIndex >= 0)
                {
                    try
                    {
                        return BasicInfo.PSPPB[SPPBIndex];
                    }
                    catch(Exception)
                    {
                        MessageBox.Show("返回值错误！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                }
                else
                    return null;
            }
            set
            {
                if (SPPBIndex >= 0)
                {
                    try
                    {
                        BasicInfo.PSPPB[SPPBIndex] = value;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("返回值错误！错误信息为:" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                RaisePropertyChanged(nameof(SPPBContent));
            }
        }


        private string _sppbNumberText;
        public string SPPBNumberText
        {
            get
            {
                return _sppbNumberText;
            }
            set
            {
                if (_sppbNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _sppbNumberText = value;

                }
                RaisePropertyChanged(nameof(SPPBNumberText));
            }
        }

        private CommandObject<SelectionChangedEventArgs> _selection_SPPB_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_SPPB_Executed
        {
            get
            {
                if (_selection_SPPB_Executed == null)
                    _selection_SPPB_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count > 0)
                            {
                                SPPBNumberText = e.AddedItems[0].ToString();
                            }
                            loadSPPBContent();
                        }));
                return _selection_SPPB_Executed;
            }
        }

        private void loadSPPBContent()
        {
            if (!string.IsNullOrEmpty(SPPBNumberText))
            {
                if (BasicInfo is not null)
                {
                    SPPBContentEnable = true;
                    if (BasicInfo.PSPPB is null)
                    {
                        List<SPPB> sppb = new List<SPPB>();
                        BasicInfo.PSPPB = sppb;
                    }
                    SPPBIndex = BasicInfo.PSPPB.FindIndex((SPPB e) => e.SPPBNumber == BasicInfo.Number + "-" + SPPBNumberText);
                    if (SPPBIndex >= 0)
                    {
                        SPPBContent = BasicInfo.PSPPB[SPPBIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "SPPB/身体平衡记录加载成功。记录编号为：" + SPPBNumberText + "。");
                    }
                    else
                    {
                        var sppbContent = new SPPB { SPPBNumber = BasicInfo.Number + "-" + SPPBNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.PSPPB.Add(sppbContent);
                        BasicInfo = BasicInfo;
                        SPPBIndex = BasicInfo.PSPPB.FindIndex((SPPB e) => e.SPPBNumber == BasicInfo.Number + "-" + SPPBNumberText);
                        SPPBContent = BasicInfo.PSPPB[SPPBIndex];

                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的SPPB/身体平衡记录创建成功。记录编号为：" + SPPBNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private CommandObject<RoutedEventArgs> _load_SPPB_Executed;
        public CommandObject<RoutedEventArgs> Load_SPPB_Executed
        {
            get
            {
                if (_load_SPPB_Executed == null)
                    _load_SPPB_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadSPPBContent();
                        }));
                return _load_SPPB_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _save_SPPB_Executed;
        public CommandObject<RoutedEventArgs> Save_SPPB_Executed
        {
            get
            {
                if (_save_SPPB_Executed == null)
                    _save_SPPB_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (SPPBContent != null)
                            {
                                bool saveInfo = DataUitls.SaveSPPB(SPPBContent);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "的档案保存成功。档案编号为：" + SPPBContent.SPPBNumber);
                            }
                        }));
                return _save_SPPB_Executed;
            }
        }
    }
}
