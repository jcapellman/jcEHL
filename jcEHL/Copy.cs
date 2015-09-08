using System.Linq;
using System.Reflection;

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
    }
}