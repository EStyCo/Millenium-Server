using static System.Net.WebRequestMethods;

namespace Client.MVVM.Model.Utilities
{
    public static class BaseUrl
    {
        private static string baseUrl = "http://147.45.75.109:80";

        public static string Get()
        { 
            return baseUrl;
        }

        public static void SwitchToLocal()
        { 
            baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "https://127.0.0.1:5000";
        }

        public static void SwitchToVPS()
        {
            baseUrl = "http://147.45.75.109:80";
        }
    }
}
