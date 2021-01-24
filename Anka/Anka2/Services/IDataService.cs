using Anka2.Models;
using System.Windows;

namespace Anka2.Services
{
    interface IDataService
    {
        public bool AddPerson(BasicInfo newPerson);
        public Window GetParentWindow(DependencyObject Source);
       // public void SetStatusInfo();

        
    }
    public enum SheetItems
    {
        BasicInfo,
        Exercise,
        PHQ,
        GAD,
        IPAQ,
        OHQ,
        SPPB,
        Physique
    };
    public enum InfoType
    {
        Success,
        Warning,
        Error
    }
}
