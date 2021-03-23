using Anka2.Models;
using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Anka2.ViewModels
{
    public class ExerciseSheetViewModel : NotifyObject, IStatusInfoService
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

        private bool _exerciseContentEnable = false;
        public bool ExerciseContentEnable
        {
            get => _exerciseContentEnable;
            set
            {
                if (_exerciseContentEnable != value)
                {
                    _exerciseContentEnable = value;
                    RaisePropertyChanged(nameof(ExerciseContentEnable));
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

        private int _exerciseIndex = -1;
        public int ExerciseIndex
        {
            get
            {
                return _exerciseIndex;
            }
            set
            {
                _exerciseIndex = value;
            }
        }

        //private Exercise _exerciseContent;
        public Exercise ExerciseContent
        {
            get
            {
                if (ExerciseIndex >= 0)
                {
                    try
                    {
                        return BasicInfo.PExercise[ExerciseIndex];
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
                if (ExerciseIndex >= 0)
                {
                    try
                    {
                        if (BasicInfo.PExercise is not null)
                            BasicInfo.PExercise[ExerciseIndex] = value;
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        MessageBox.Show("返回值错误！错误信息为:" + e.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                RaisePropertyChanged(nameof(ExerciseContent));
            }
        }


        private string _exerciseNumberText;
        public string ExerciseNumberText
        {
            get
            {
                return _exerciseNumberText;
            }
            set
            {
                if (_exerciseNumberText != value)
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        var endNumber = value.Substring(value.Length - 3, 3);
                        value = value.Remove(value.Length - 3, 3);
                        value = Regex.Replace(value, @"[^0-9]+", "/");
                        value += endNumber;
                    }
                    _exerciseNumberText = value;

                }
                RaisePropertyChanged(nameof(ExerciseNumberText));
            }
        }



        private CommandObject<SelectionChangedEventArgs> _selection_Exercise_Executed;
        public CommandObject<SelectionChangedEventArgs> Selection_Exercise_Executed
        {
            get
            {
                if (_selection_Exercise_Executed == null)
                    _selection_Exercise_Executed = new CommandObject<SelectionChangedEventArgs>(
                        new Action<SelectionChangedEventArgs>(e =>
                        {
                            if (e.AddedItems.Count>0)
                            {
                                ExerciseNumberText = e.AddedItems[0].ToString();
                            }
                            loadExerciseContent();
                        }));
                return _selection_Exercise_Executed;
            }
        }
        private CommandObject<RoutedEventArgs> _load_Exercise_Executed;
        public CommandObject<RoutedEventArgs> Load_Exercise_Executed
        {
            get
            {
                if (_load_Exercise_Executed == null)
                    _load_Exercise_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            loadExerciseContent();
                        }));
                return _load_Exercise_Executed;
            }
        }

        private void loadExerciseContent()
        {
            if (!string.IsNullOrEmpty(ExerciseNumberText))
            {
                if (BasicInfo is not null)
                {
                    ExerciseContentEnable = true;
                    if (BasicInfo.PExercise is null)
                    {
                        List<Exercise> exercises = new List<Exercise>();
                        BasicInfo.PExercise = exercises;
                    }
                    ExerciseIndex = BasicInfo.PExercise.FindIndex((Exercise e) => e.ExerciseNumber == BasicInfo.Number + "-" + ExerciseNumberText);
                    if (ExerciseIndex >= 0)
                    {
                        ExerciseContent = BasicInfo.PExercise[ExerciseIndex];
                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "运动负荷记录加载成功。记录编号为：" + ExerciseNumberText + "。");
                    }
                    else
                    {
                        var exerciseContent = new Exercise { ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText, basicinfoNumber = BasicInfo.Number };
                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                        BasicInfo.PExercise.Add(exerciseContent);

                        BasicInfo = BasicInfo;
                        ExerciseIndex = BasicInfo.PExercise.FindIndex((Exercise e) => e.ExerciseNumber == BasicInfo.Number + "-" + ExerciseNumberText);
                        ExerciseContent = BasicInfo.PExercise[ExerciseIndex];


                        NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "新的运动负荷记录创建成功。记录编号为：" + ExerciseNumberText + "。");
                    }

                }
                else
                {
                    MessageBox.Show("当前档案不存在，请新建档案信息。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private CommandObject<RoutedEventArgs> _save_Exercise_Executed;
        public CommandObject<RoutedEventArgs> Save_Exercise_Executed
        {
            get
            {
                if (_save_Exercise_Executed == null)
                    _save_Exercise_Executed = new CommandObject<RoutedEventArgs>(
                        new Action<RoutedEventArgs>(e =>
                        {
                            if (ExerciseContent != null)
                            {
                                bool saveInfo = DataUitls.SaveExercise(ExerciseContent);
                                if (saveInfo == true)
                                    NotifyStatusInfo(InfoType.Success, BasicInfo.Name + "的档案保存成功。档案编号为：" + ExerciseContent.ExerciseNumber);
                            }
                        }));
                return _save_Exercise_Executed;
            }
        }


    }
}
