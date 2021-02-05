using Anka2.Models;
using Anka2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    RaisePropertyChanged(nameof(BasicInfo));
                }
            }
        }

        private int _exerciseIndex = 0;
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

        private Exercise _exerciseContent;
        public Exercise ExerciseContent
        {
            get
            {                
                return _exerciseContent;
            }
            set
            {                     
                _exerciseContent = value;
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
                _exerciseNumberText = value;
            }
        }

        private string bedUpBloodPressureLower_0 = null;
        public string BedUpBloodPressureLower_0
        {
            get => bedUpBloodPressureLower_0;
            set
            {
                if (_basicInfo.PExercise[0].BloodPressureLower != value)
                {
                    _basicInfo.PExercise[0].BloodPressureLower = value;
                    RaisePropertyChanged(nameof(BasicInfo));
                }
            }
        }
    }
}
