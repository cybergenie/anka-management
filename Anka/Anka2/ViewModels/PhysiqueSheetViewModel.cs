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
    public class PhysiqueSheetViewModel : NotifyObject, IStatusInfoService
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

        private bool _physiqueContentEnable = false;
        public bool PhysiqueContentEnable
        {
            get => _physiqueContentEnable;
            set
            {
                if (_physiqueContentEnable != value)
                {
                    _physiqueContentEnable = value;
                    RaisePropertyChanged(nameof(PhysiqueContentEnable));
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

        private int _physiqueIndex = -1;
        public int PhysiqueIndex
        {
            get
            {
                return _physiqueIndex;
            }
            set
            {
                _physiqueIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public Physique PhysiqueContent
        {
            get
            {
                
                if (PhysiqueIndex >= 0)
                {
                    try
                    {
                        if (BasicInfo.PPhysique is not null)
                            return BasicInfo.PPhysique[PhysiqueIndex];
                        else
                            return null;

                    }
                    catch(IndexOutOfRangeException e )
                    {
                        MessageBox.Show("返回值错误！错误信息为:"+e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }
                    
                }
                else
                    return null;
            }
            set
            {
                
                if (PhysiqueIndex >= 0)
                {
                    try
                    {
                        if (BasicInfo.PPhysique is not null)
                            BasicInfo.PPhysique[PhysiqueIndex] = value;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        MessageBox.Show("返回值错误！错误信息为:" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                RaisePropertyChanged(nameof(PhysiqueContent));
            }
        }

       


        private string _physiqueNumberText;
        public string PhysiqueNumberText
        {
            get
            {
                return _physiqueNumberText;
            }
            set
            {
                if (_physiqueNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }

                    _physiqueNumberText = value;

                }
                RaisePropertyChanged(nameof(PhysiqueNumberText));
            }
        }

        private CommandObject<SelectionChangedEventArgs> _selection_Physique_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_Physique_Executed
        {
            get
            {
                if (_selection_Physique_Executed == null)
                    _selection_Physique_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count > 0)
                            {
                                PhysiqueNumberText = e.AddedItems[0].ToString();
                            }
                            loadPhysiqueContent();
                        }));
                return _selection_Physique_Executed;
            }
        }

        private void loadPhysiqueContent()
        {
            if (!string.IsNullOrEmpty(PhysiqueNumberText))
            {
                if (BasicInfo is not null)
                {
                    PhysiqueContentEnable = true;
                    if (BasicInfo.PPhysique is null)
                    {
                        List<Physique> physique = new List<Physique>();
                        BasicInfo.PPhysique = physique;
                    }
                    PhysiqueIndex = BasicInfo.PPhysique.FindIndex((Physique e) => e.PhysiqueNumber == BasicInfo.Number + "-" + PhysiqueNumberText);
                    if (PhysiqueIndex >= 0)
                    {
                        PhysiqueContent = BasicInfo.PPhysique[PhysiqueIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "Physique/身体平衡记录加载成功。记录编号为：" + PhysiqueNumberText + "。");
                    }
                    else
                    {
                        var physiqueContent = new Physique { PhysiqueNumber = BasicInfo.Number + "-" + PhysiqueNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.PPhysique.Add(physiqueContent);
                        BasicInfo = BasicInfo;
                        PhysiqueIndex = BasicInfo.PPhysique.FindIndex((Physique e) => e.PhysiqueNumber == BasicInfo.Number + "-" + PhysiqueNumberText);
                        PhysiqueContent = BasicInfo.PPhysique[PhysiqueIndex];

                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的Physique/身体平衡记录创建成功。记录编号为：" + PhysiqueNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private CommandObject<RoutedEventArgs> _load_Physique_Executed;
        public CommandObject<RoutedEventArgs> Load_Physique_Executed
        {
            get
            {
                if (_load_Physique_Executed == null)
                    _load_Physique_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadPhysiqueContent();
                        }));
                return _load_Physique_Executed;
            }
        }

        private CommandObject<RoutedEventArgs> _save_Physique_Executed;
        public CommandObject<RoutedEventArgs> Save_Physique_Executed
        {
            get
            {
                if (_save_Physique_Executed == null)
                    _save_Physique_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (PhysiqueContent != null)
                            {
                                bool saveInfo = DataUitls.SavePhysique(PhysiqueContent);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "的档案保存成功。档案编号为：" + PhysiqueContent.PhysiqueNumber);
                            }
                        }));
                return _save_Physique_Executed;
            }
        }
    }
}
