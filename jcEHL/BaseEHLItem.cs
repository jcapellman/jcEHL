using System;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace jcEHL {
    public class BaseEHLItem<T> {
        public BaseEHLItem(T baseObject) {
            if (baseObject == null) {
                return;
            }

            Copy.Init(baseObject, this);
        }

        public string ToJSON() {
            return JsonConvert.SerializeObject(this);
        }

        public string ToBSON() {
            using (var ms = new MemoryStream()) {
                var serializer = new JsonSerializer();

                var writer = new BsonWriter(ms);
                serializer.Serialize(writer, this);

                return BitConverter.ToString(ms.ToArray());
            }
        }
    }
}