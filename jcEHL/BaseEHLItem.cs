﻿using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using jcEHL.JCON;
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

        public string ToJSON(bool compress = true) {
            return JsonConvert.SerializeObject(this);
        }

        public string ToBSON(bool compress = true) {
            return BitConverter.ToString(ToBSONArray(compress));
        }

        public byte[] ToJCON(bool compress = true) {
            var data = JCONSerialize.Serialize(this.ToByte());

            return (compress ? BaseEHLItem<T>.compress(BitConverter.ToString(data)) : data);
        }

        public void FromJCON(byte[] data) {
            var unJcon = JCONDeserialize.Deserialize(data);

            using (var ms = new MemoryStream(unJcon)) {
                using (var brs = new BinaryReader(ms)) {
                    var br = new BsonReader(brs);
                    {


                    }
                }
            }
        }

        public byte[] ToByte() {
            using (var ms = new MemoryStream()) {
                var serializer = new JsonSerializer();

                var writer = new BsonWriter(ms);
                serializer.Serialize(writer, this);

                return ms.ToArray();
            }
        }

        public byte[] ToBSONArray(bool compress = true) {
            using (var ms = new MemoryStream()) {
                var serializer = new JsonSerializer();

                var writer = new BsonWriter(ms);
                serializer.Serialize(writer, this);

                return (compress ? BaseEHLItem<T>.compress(BitConverter.ToString(ms.ToArray())) : ms.ToArray());
            }
        }

        private static byte[] compress(string str) {
            byte[] compressed;

            using (var ms = new MemoryStream()) {
                using (var gzStream = new GZipStream(ms, CompressionMode.Compress)) {
                    using (var mStream = new MemoryStream(Encoding.UTF8.GetBytes(str))) {
                        mStream.CopyTo(gzStream);
                    }

                    compressed = ms.ToArray();
                }
            }

            return compressed;
        }
    }
}