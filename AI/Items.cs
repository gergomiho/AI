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

        public Items(Type type)
        {
            itsType = type;
            itsAmount = 0;
        }

        public static Items operator ++(Items item)
        {
            item.itsAmount++;
            return item;
        }

        public int Amount()
        {
            return itsAmount;
        }

        ~Items() { }
    }
}
