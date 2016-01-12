using System;
using System.Linq;
using System.Reflection;

namespace jcEHL {
    public class ReflectionHelper {
        public T FindTInAssembly<T>(object implementationValue, string implementationProperty) where T : class {
            var assembly = (Assembly)typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly").Invoke(null, new object[0]);

            var types = assembly.DefinedTypes.ToList();

            foreach (var type in types) {
                if (type.IsSubclassOf(typeof(T)) && !type.IsAbstract) {
                    var obj = (T)Activator.CreateInstance(type.AsType());
                    
                    if (obj.GetType().GetRuntimeField(implementationProperty).GetValue(obj) == implementationValue) {
                        return obj;
                    }
                }
            }

            return null;
        }
    }
}