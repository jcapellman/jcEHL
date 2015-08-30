using System.Linq;
using System.Reflection;

namespace jcEHL {
    public static class Copy {
        public static TK Init<T, TK>(T baseItem, TK newItem) {
            var fields = baseItem.GetType().GetRuntimeFields().ToList();

            foreach (var field in fields) {
                var val = field.GetValue(baseItem);

                field.SetValue(newItem, val);
            }

            return newItem;
        }
    }
}