using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jcEHL.JCON {
    public class JCONSerialize : BaseJCON {

        private static string writeStr(Dictionary<int, byte> header, List<int> content) {
            var sb = new StringBuilder();

            foreach (var key in header.Keys) {
                sb.Append($"{key}|{header[key]}");
            }

            sb.Append(string.Join(";", content));

            return sb.ToString();
        }

        private static byte[] writeByte(Dictionary<int, byte> header, List<int> content) {
            using (var ms = new MemoryStream()) {
                BinaryWriter bw = new BinaryWriter(ms);

                foreach (var key in header.Keys) {
                    bw.Write($"{key}|{header[key]}");
                }

                foreach (var idx in content) {
                    bw.Write(idx);
                }

                return ms.ToArray();
            }
        }

        public static byte[] Serialize(byte[] baseEhlItem, COMPRESSION_LEVEL compression = COMPRESSION_LEVEL.SIZE) {
            var header = new Dictionary<int, byte>();
            var content = new List<int>();
            var curIndex = 0;

            var isEven = baseEhlItem.Length % 2 == 0;

            var iterator = (isEven ? 1 : 1);

            var lastMatch = string.Empty;

            for (var x = 0; x < baseEhlItem.Length; x += iterator) {
                if (x == baseEhlItem.Length || x + iterator > baseEhlItem.Length) {
                    continue;
                }

                var newMatch = baseEhlItem[x];

                var index = 0;

                if (!header.ContainsValue(newMatch)) {
                    index = curIndex;

                    header.Add(curIndex++, newMatch);
                } else {
                    for (var y = 0; y < header.Keys.Count; y++) {
                        if (header[y] == newMatch) {
                            index = y;
                            break;
                        }
                    }
                }

                content.Add(index);
            }


            return writeByte(header, content);
        }
    }
}
