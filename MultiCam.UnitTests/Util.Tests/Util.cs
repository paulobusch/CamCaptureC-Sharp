using System;
using System.IO;
using System.Reflection;

namespace MultiCam.UnitTests {
    public static class Util {
        private static Random _random = new Random();

        #region Randomize
        public static int RandInt() => _random.Next(int.MinValue, int.MaxValue);
        public static int RandInt(int start, int end) => _random.Next(start, end);
        public static bool RandBool() => _random.NextDouble() >= 0.5;
        public static float RandFloat() => (float)_random.NextDouble();
        public static string RandString() => RandInt().ToString("String: {00000000}");
        #endregion

        #region Properties
        public static T GetProperty<T>(Type type, string property) {
            var prop = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            return (T)prop.GetValue(type);
        }
        public static void SetProperty(Type type, string property, object newValue) {
            var prop = type.GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            prop.SetValue(type, newValue);
        }
        public static T GetProperty<T>(object obj, string property) {
            var prop = obj.GetType().GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            return (T)prop.GetValue(obj);
        }
        public static void SetProperty(object obj, string property, object newValue) {
            var prop = obj.GetType().GetField(property, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            prop.SetValue(obj, newValue);
        }
        public static string GetCurrentPath (){
            return Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory.ToString());
        }
        #endregion
    }
}
