using System.Linq;
using System.Reflection;
using jcEHL.JCON;

namespace jcEHL {
    public static class Copy {
        public static TK Init<T, TK>(T baseItem, TK newItem, bool publicOnly = true) {
            var fields = baseItem.GetType().GetRuntimeFields().Where(a => (publicOnly && a.IsPublic) || (!publicOnly)).ToList();

            foreach (var field in fields) {
                var val = field.GetValue(baseItem);

                field.SetValue(newItem, val);
            }

            return newItem;
        }

        public static void InitT<T>(T objValue, string value) {
            var converted = JCONConvert.DeserializeObject<T>(value);

            var fields = objValue.GetType().GetRuntimeProperties().OrderBy(a => a.Name);

            foreach (var field in fields) {
                var val = field.GetValue(converted);

                field.SetValue(objValue, val);
            }
        }
    }
}