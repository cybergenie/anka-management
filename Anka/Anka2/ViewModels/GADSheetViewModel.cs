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
    public class GADSheetViewModel : NotifyObject, IStatusInfoService
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

        private bool _gadContentEnable = false;
        public bool GADContentEnable
        {
            get => _gadContentEnable;
            set
            {
                if (_gadContentEnable != value)
                {
                    _gadContentEnable = value;
                    RaisePropertyChanged(nameof(GADContentEnable));
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

        private int _gadIndex = -1;
        public int GADIndex
        {
            get
            {
                return _gadIndex;
            }
            set
            {
                _gadIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public GAD GADContent
        {
            get
            {
                if (GADIndex >= 0)
                {
                    return BasicInfo.PGAD[GADIndex];
                }
                else
                    return null;
            }
            set
            {
                if (GADIndex >= 0)
                {
                    BasicInfo.PGAD[GADIndex] = value;
                }
                RaisePropertyChanged(nameof(GADContent));
            }
        }


        private string _gadNumberText;
        public string GADNumberText
        {
            get
            {
                return _gadNumberText;
            }
            set
            {
                if (_gadNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _gadNumberText = value;

                }
                RaisePropertyChanged(nameof(GADNumberText));
            }
        }

        private CommandObject<SelectionChangedEventArgs> _selection_GAD_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_GAD_Executed
        {
            get
            {
                if (_selection_GAD_Executed == null)
                    _selection_GAD_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count > 0)
                            {
                                GADNumberText = e.AddedItems[0].ToString();
                            }
                            loadGADContent();
                        }));
                return _selection_GAD_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _load_GAD_Executed;
        public CommandObject<RoutedEventArgs> Load_GAD_Executed
        {
            get
            {
                if (_load_GAD_Executed == null)
                    _load_GAD_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadGADContent();
                        }));
                return _load_GAD_Executed;
            }
        }

        private void loadGADContent()
        {
            if (!string.IsNullOrEmpty(GADNumberText))
            {
                if (BasicInfo is not null)
                {
                    GADContentEnable = true;
                    if (BasicInfo.PGAD is null)
                    {
                        List<GAD> gad = new List<GAD>();
                        BasicInfo.PGAD = gad;
                    }
                    GADIndex = BasicInfo.PGAD.FindIndex((GAD e) => e.GADNumber == BasicInfo.Number + "-" + GADNumberText);
                    if (GADIndex >= 0)
                    {
                        GADContent = BasicInfo.PGAD[GADIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "GAD记录加载成功。记录编号为：" + GADNumberText + "。");
                    }
                    else
                    {
                        var gadContent = new GAD { GADNumber = BasicInfo.Number + "-" + GADNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.PGAD.Add(gadContent);
                        BasicInfo = BasicInfo;
                        GADIndex = BasicInfo.PGAD.FindIndex((GAD e) => e.GADNumber == BasicInfo.Number + "-" + GADNumberText);
                        GADContent = BasicInfo.PGAD[GADIndex];

                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的GAD记录创建成功。记录编号为：" + GADNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
