

namespace Client.MVVM.Model.Utilities
{
    public static class BaseUrl
    {
        //private static string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://147.45.75.109:5000" : "http://127.0.0.1:5000";

        //private static string baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "http://147.45.75.109:5000" : "http://147.45.75.109:5000";

        public static string Get()
        { 
            return "http://147.45.75.109:5000";
        }
    }
}
