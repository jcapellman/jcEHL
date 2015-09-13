using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace jcEHL.JCON {
    public class JCONConvert {
        private const char dilmeter = ',';

        public static string SerializeObject<T>(T value) {
            var properties = value.GetType().GetRuntimeProperties().OrderBy(a => a.Name);

            var str = properties.Aggregate("",
                (current, property) => current + "\"" + (property.GetValue(value) + "\"" + dilmeter.ToString()));

            return str.Remove(str.Length - 1);
        }

        public static string SerializeObject<T>(List<T> value) {
            var properties = typeof(T).GetRuntimeProperties().OrderBy(a => a.Name);

            var strList = string.Empty;

            foreach (var item in value) {
                var itemStr = "";

                foreach (var property in properties) {
                    itemStr += property.GetValue(item) + dilmeter.ToString();
                }

                strList += itemStr.Remove(itemStr.Length - 1) + System.Environment.NewLine;
            }

            return strList;
        }

        public static T DeserializeObject<T>(string value) {
            var t = (T)Activator.CreateInstance(typeof(T));

            var isList = t.GetType().GetGenericTypeDefinition() == typeof(List<>);

            var properties = isList ? t.GetType().GenericTypeArguments[0].GetRuntimeProperties() : t.GetType().GetRuntimeProperties();

            var idx = 0;

            var regexObj = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)");
            
            if (isList) {
                var items = Regex.Split(value, "[\r\n]+");
                
                var typeo = t.GetType().GenericTypeArguments[0];
                
                foreach (var item in items) {
                    if (string.IsNullOrEmpty(item)) {
                        continue;
                    }

                    var itemT = Activator.CreateInstance(typeo);

                    var values = (from Match match in regexObj.Matches(item) select match.Value.TrimStart(',')).ToList();

                    idx = 0;

                    foreach (var property in properties) {
                        var val = values[idx];

                        if (property.PropertyType == typeof(string)) {
                            val = val.TrimStart('"');
                            val = val.TrimEnd('"');
                        }

                        property.SetValue(itemT, val);

                        idx++;
                    }

                    t.GetType().GetRuntimeMethod("Add", new[] { itemT.GetType() }).Invoke(t, new[] { itemT });
                }
            } else {
                var values = (from Match match in regexObj.Matches(value) select match.Value.TrimStart(',')).ToList();

                foreach (var property in properties) {
                    var val = values[idx];

                    if (property.PropertyType == typeof(string)) {
                        val = val.TrimStart('"');
                        val = val.TrimEnd('"');
                    }

                    property.SetValue(t, val);

                    idx++;
                }
            }

            return t;
        }
    }
}