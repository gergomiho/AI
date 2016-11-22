using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Items
    {
        public enum Type { money, wood, stone, meat }
        private Type itsType;
        private int itsAmount;

        public void gatherItem(int x)
        {
            this.itsAmount += x;
        }

        public void destroyItem(int x)
        {
            this.itsAmount -= x;
        }

        public Items(Type type, int Amount = 0)
        {
            itsType = type;
            itsAmount = Amount;
        }

        public static Items operator ++(Items item)
        {
            item.itsAmount++;
            return item;
        }

        public static Items operator +(Items item, Items item2)
        {
            item.itsAmount += item2.Amount();
            return item;
        }

        public int Amount()
        {
            return itsAmount;
        }

        public Type itemType()
        {
            return itsType;
        }

        ~Items() { }
    }
}
