using Microsoft.Azure;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanGavel.Common.Configuration
{
    public static partial class ApplicationConfigurationManager
    {
        public static String GetSetting(String name)
        {
            try
            {
                if (RoleEnvironment.IsAvailable)
                {
                    return CloudConfigurationManager.GetSetting(name);
                }
            }
            catch { }

            var setting = ConfigurationManager.AppSettings[name];

            // Try connection string
            if (setting == null)
            {
                try
                {
                    return ConfigurationManager.ConnectionStrings[name].ConnectionString;
                }
                catch
                {
                    return name;
                }
            }
            else
                return setting;
        }
    }
}
