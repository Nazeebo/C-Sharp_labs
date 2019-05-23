using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitContainer {

    class BitEnum : IEnumerator<bool>, IDisposable {
        BitContainer bits;
        int position = -1;
        public BitEnum(BitContainer bits) {
            this.bits = bits;
        }

        public bool Current {
            get {
                try {
                    return Convert.ToBoolean(bits[position]);
                }
                catch (IndexOutOfRangeException) {
                    throw new InvalidOperationException();
                }
            }
        }


        object IEnumerator.Current => Current;

        public bool MoveNext() {
            ++position;
            return (position < bits.Lenght);
        }

        public void Reset() {
            position = -1;  
        }

        public void Dispose() {
            bits.Clear();
        }
    }
}
