using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitContainer {
    class BitContainer : IEnumerable<bool> {
        public List<byte> list;
        private int length { get; set; }

        public BitContainer() {
            list = new List<byte>();
            length = 0;
        }

        public int Lenght => length;


        public void pushBit(int bit) {
            if (bit != 0 && bit != 1) throw new ArgumentOutOfRangeException("Incorrect value");
            int offset = (++length - 1) % 8;
            if (offset == 0) list.Add(0);
            setBit(length - 1, bit);
        }

        public void pushBit(bool bit) {
            int _bit = (bit) ? 1 : 0;
            pushBit(_bit);
        }

        private void setBit(int position, int bit) {
            if (position >= Lenght ||  position < 0)
                throw new IndexOutOfRangeException("Out of range");
            if (bit != 0 && bit != 1) 
                throw new ArgumentOutOfRangeException("Incorrect value");

                int place = position / 8;
                int offset = position % 8;
                int currByte = list[place];
                currByte &= ~(1 << offset); //cбрасываем бит на offset 
                list[place] = Convert.ToByte((currByte | (bit << offset)) & 0xff); //записываем новое значение
        }

        private void setBit(int position, bool bit) {
            int _bit = (bit) ? 1 : 0;
            setBit(position, _bit);
        }

        private int getBit(int position) {
            if (position >= Lenght || position < 0)
                throw new IndexOutOfRangeException("Out of range");
            else {
                int place = position / 8;
                int offset = position % 8;

                int value = list[place] & (1 << offset);
                return (value > 0) ? 1 : 0;
            }

        }

        public void Clear() {
            length = 0;
            list.Clear();
        }

        public void Insert(int place, int bit) {
            if (place >= Lenght ||  place < 0)
                throw new IndexOutOfRangeException("Out of range");
            if (bit != 0 && bit != 1) 
                throw new ArgumentOutOfRangeException("Incorrect value");
            pushBit(this[Lenght-1]);
            for (int i = Lenght - 2; i > place; --i)
                this[i] = this[i - 1];
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
            --length;
        }

        public override string ToString() {
            StringBuilder str = new StringBuilder();
            int bytes = (Lenght - 1) / 8;
            int restInLast = (Lenght - 1) % 8;
            for (int i = 0; i <= bytes - 1; ++i) {
                for (int j = 7; j >= 0; --j)
                    str.Append(this[i*8 + j]);
                str.Append(" ");
            }

            for (int i = restInLast; i >= 0; --i)
                str.Append(this[bytes * 8 + i]); 
                
            return str.ToString();
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
            }
        }

    }
}
