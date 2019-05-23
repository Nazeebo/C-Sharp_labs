using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitContainer {
    class BitContainer : IEnumerable<bool> {
        public List<byte> list;
        private int lenght { get; set; }

        public BitContainer() {
            list = new List<byte>();
            lenght = 0;
        }

        public int Lenght => lenght;


        public void pushBit(int bit) {
            if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException("Incorrect value");
            int offset = (++lenght - 1) % 8;
            if (offset == 0) list.Add(0);
            setBit(lenght - 1, bit);
        }

        public void pushBit(bool bit) {
            int _bit = (bit) ? 1 : 0;
            pushBit(_bit);
        }

        private void setBit(int position, int bit) {
            if (position >= Lenght &&  position < 0)
                throw new Exception("Out of range");
            else {
                int place = position / 8;
                int offset = position % 8;
                int currByte = list[place];
                currByte &= ~(1 << offset); //cбрасываем бит на offset 
                list[place] = Convert.ToByte((currByte | (bit << offset)) & 0xff); //записываем новое значение
            }
        }

        private void setBit(int position, bool bit) {
            int _bit = (bit) ? 1 : 0;
            setBit(position, _bit);
        }

        private int getBit(int position) {
            if (position >= Lenght && position < 0)
                throw new IndexOutOfRangeException("Out of range");
            else {
                int place = position / 8;
                int offset = position % 8;

                int value = list[place] & (1 << offset);
                return (value > 0) ? 1 : 0;
            }

        }

        public void Clear() {
            lenght = 0;
            list.Clear();
        }

        public void Insert(int place, int bit) {
            ++lenght;
            for (int i = place; i < Lenght - 1; ++i)
                this[i + 1] = this[i];
            this[place] = bit;
        }

        public void Insert(int place, bool bit) {
            int _bit = (bit) ? 1 : 0;
            Insert(place, _bit);
        }

        public void Remove(int place) {
            if (place >= Lenght || place < 0) throw new IndexOutOfRangeException("Out of range");
            for (int i = place; i < Lenght - 1; ++i)
                this[i] = this[i + 1];
            --lenght;
        }

        public override string ToString() {
            string str = "";
            int bytes = (Lenght - 1) / 8;
            int restInLast = (Lenght - 1) % 8;
            for (int i = 0; i <= bytes - 1; ++i) {
                for (int j = 7; j >= 0; --j)
                    str += this[i*8 + j];
                str += " ";
            }

            for (int i = restInLast; i >= 0; --i)
                str += this[bytes * 8 + i];
                
            return str;
        }

        public IEnumerator<bool> GetEnumerator() {
            return new BitEnum(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)GetEnumerator();
        }

        public int this[int position] {
            get{
                return getBit(position);
            }
            set{
                if (value == 1 || value == 0)
                    setBit(position, value);
                else throw new ArgumentOutOfRangeException("Incorrect to write wrong value");
            }
        }

    }
}
