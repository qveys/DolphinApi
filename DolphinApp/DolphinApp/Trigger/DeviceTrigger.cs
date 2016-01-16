using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DolphinApp.Trigger
{
    public class DeviceTrigger : StateTriggerBase
    {

        public bool MobileDevice { get;
            set; }

        public DeviceTrigger()
        {
            SetActive(MobileDevice = (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile"));
        }

    }
}
