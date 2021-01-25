using Anka2.Models;

namespace Anka2.ViewModels
{
    public class NewPersonViewModel : NotifyObject
    {
        /// <summary>
        /// 档案号
        /// </summary>
        private string _personId;
        public string PersonId
        {
            get => _personId;
            set
            {
                if (_personId != value)
                {
                    _personId = value;
                    RaisePropertyChanged(nameof(PersonId));
                }
            }

        }
        /// <summary>
        /// 姓名
        /// </summary>
        private string _personName;
        public string PersonName
        {
            get => _personName;
            set
            {
                if (_personName != value)
                {
                    _personName = value;
                    RaisePropertyChanged(nameof(PersonName));
                }
            }

        }

        private bool _personGender;
        public bool PersonGender
        {
            get => _personGender;
            set
            {
                if (_personGender != value)
                {
                    _personGender = value;
                    RaisePropertyChanged(nameof(PersonGender));
                }
            }

        }

        private string _personAge;
        public string PersonAge
        {
            get => _personAge;
            set
            {
                if (_personAge != value)
                {
                    _personAge = value;
                    RaisePropertyChanged(nameof(PersonAge));
                }
            }

        }
    }
}
