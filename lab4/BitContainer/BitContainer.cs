using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitContainer {
    class BitContainer {
        public List<byte> list;
        private int lenght { get; set; }
        private byte one = 1;

        public BitContainer() {
            list = new List<byte>();
            lenght = -1;
        }

        public int Lenght => lenght;

        public void pushBit(int bit) {
            int offset = ++lenght % 8;
            if (offset == 0) list.Add(0);
            setBit(lenght, bit);
        }

        public void pushBit(bool bit) {
            int _bit = (bit) ? 1 : 0;
            pushBit(_bit);
        }

        public void setBit(int position, int bit) {
            if (position > Lenght &&  position < 0)
                throw new Exception("Out of range");
            else {
                int place = position / 8;
                int offset = position % 8;
                int currByte = list[place];
                currByte &= ~(1 << offset); //cбрасываем бит на offset 
                list[place] = Convert.ToByte((currByte | (bit << offset)) & 0xff); //записываем новое значение
            }
        }

        public void setBit(int position, bool bit) {
            int _bit = (bit) ? 1 : 0;
            setBit(position, _bit);
        }

        public int getBit(int position) {
            if (position > Lenght && position < 0)
                throw new Exception("Out of range");
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

        }

        public void Insert(int place, bool bit) {

        }

        public void Remove(int place) {

        }

        public override string ToString() {
            return base.ToString();
        }

        public int this[int position] {
            get{
                return getBit(position);
            }
            set{
                if (value == 1 || value == 0)
                    setBit(position, value);
                else throw new Exception("Attempt to write wrong value");
            }
        }

    }
}
