using Anka2.Models;
using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Anka2.ViewModels
{
    public class ExerciseSheetViewModel : NotifyObject, IStatusInfoService
    {
        public NotifyStatusInfoHandler NotifyStatusInfo;
        public void UpDateStatusInfo(NotifyStatusInfoHandler notify)
        {
            NotifyStatusInfo += notify;
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
                    return BasicInfo.PExercise[ExerciseIndex];
                }
                else
                    return null;
            }
            set
            {
                if (ExerciseIndex >= 0)
                {
                    BasicInfo.PExercise[ExerciseIndex] = value;                    
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
                    _exerciseNumberText = value;
                }
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
                            if (!string.IsNullOrEmpty(ExerciseNumberText))
                            {
                                if (BasicInfo is not null)
                                {
                                    ExerciseContentEnable = true;
                                    //if (BasicInfo.PExercise is not null)
                                    //{
                                        ExerciseIndex = BasicInfo.PExercise.FindIndex((Exercise e) => e.ExerciseNumber == BasicInfo.Number + "-" + ExerciseNumberText);
                                    if (ExerciseIndex >= 0)
                                    {
                                        ExerciseContent = BasicInfo.PExercise[ExerciseIndex];
                                    }
                                    else
                                    {
                                        var exerciseContent = new Exercise { ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText, basicinfoNumber= BasicInfo.Number };
                                        //ExerciseContent.ExerciseNumber = BasicInfo.Number + "-" + ExerciseNumberText;
                                        BasicInfo.PExercise.Add(exerciseContent);
                                        BasicInfo = BasicInfo;
                                        ExerciseIndex = BasicInfo.PExercise.FindIndex((Exercise e) => e.ExerciseNumber == BasicInfo.Number + "-" + ExerciseNumberText);
                                        ExerciseContent = BasicInfo.PExercise[ExerciseIndex];
                                    }
                                    //}
                                }
                            }
                        }));
                return _load_Exercise_Executed;
            }
        }
    }
}
