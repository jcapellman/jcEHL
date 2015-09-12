using System;
using System.Linq;
using System.Reflection;

namespace jcEHL.JCON {
    public class JCONConvert {
        private const char dilmeter = '|';

        public static string SerializeObject<T>(T value) {
            var properties = value.GetType().GetRuntimeProperties().OrderBy(a => a.Name);

            return properties.Aggregate("",
                (current, property) => current + (property.GetValue(value) + dilmeter.ToString()));
        }

        public static T DeserializeObject<T>(string value) {
            var t = (T)Activator.CreateInstance(typeof (T));

            var properties = t.GetType().GetRuntimeProperties().OrderBy(a => a.Name);

            var idx = 0;

            var values = value.Split(dilmeter);

            foreach (var property in properties) {
                property.SetValue(t, values[idx]);

                idx++;
            }

            return t;
        }
    }
}