using Anka2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anka2.Services
{
    interface IStatusInfoService
    {
        void UpDateStatusInfo(NotifyStatusInfoHandler notify);
    }
}
