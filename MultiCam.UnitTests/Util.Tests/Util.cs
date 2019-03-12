using System;
using System.IO;
using System.Reflection;

namespace MultiCam.UnitTests {
    public static class Util {
        public static T GetProperty<T>(Type type, string property) {
            var prop = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            return (T)prop.GetValue(type);
        }
        public static void SetProperty(Type type, string property, object newValue) {
            var prop = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            prop.SetValue(type, newValue);
        }
        public static string GetCurrentPath (){
            return Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
        }
    }
}
