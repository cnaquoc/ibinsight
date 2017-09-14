using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBI.FileSystem.Helpers
{
    public class RemoteServer
    {
        public static string OpenConnection()
        {
            string remoteUNC =  SettingInfo.RemoteServer;            
            string username = SettingInfo.RemoteUsername;
            string password = SettingInfo.RemotePassword;

            try
            {
                PinvokeWindowsNetworking.disconnectRemote(remoteUNC);
            }
            catch 
            {

               
            }

            try
            {
                string connectionResult = PinvokeWindowsNetworking.connectToRemote(remoteUNC, username, password);
            }
            catch (Exception)
            {
                
            }           
            
            return "";
        }


        public static void CloseConnection()
        {
            string remoteUNC = SettingInfo.RemoteServer;
            try
            {
                PinvokeWindowsNetworking.disconnectRemote(remoteUNC);
            }
            catch
            {


            }
        }


        public static string GetConfigValue(string key)
        {
            try
            {
                string result = ConfigurationSettings.AppSettings[key].ToString();
                return result;
            }
            catch (Exception)
            {
            }
            return "";
        }
    }
}
