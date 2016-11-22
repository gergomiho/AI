using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Buildings
    {
        public struct Position
        {
            public int X;
            public int Y;
        }

        public enum Type { none, hospital, barrack, market };

        private int woodReq;
        private int stoneReq;
        private int moneyReq;
        private bool bIsBuilt;
        private Items wood;
        private Items stone;
        private Items money;
        private Position pos;
        private Type buildingType;

        public Type getBuildingType()
        {
            return buildingType;
        }

        public void addResource(Items res)
        {
            if (!bIsBuilt)
            {
                if (res.itemType() == Items.Type.money)
                    money += res;
                else if (res.itemType() == Items.Type.wood)
                    wood += res;
                else if (res.itemType() == Items.Type.stone)
                    stone += res;
            }
        }

        public bool isBuilt()
        {
            return bIsBuilt;
        }

        public void buildIfReady()
        {
            if(wood.Amount() >= woodReq && stone.Amount() >= stoneReq && money.Amount() >= moneyReq)
            {
                bIsBuilt = true;
            }
        }

        public Buildings(Type buildingType, int x, int y)
        {
            this.buildingType = buildingType;
            wood = new Items(Items.Type.wood);
            stone = new Items(Items.Type.stone);
            money = new Items(Items.Type.money);
            bIsBuilt = false;
            pos.X = x;
            pos.Y = y;

            switch (buildingType)
            {
                case Type.hospital:
                    woodReq = 5000;
                    stoneReq = 10000;
                    moneyReq = 2000;
                    break;
                case Type.barrack:
                    woodReq = 15000;
                    stoneReq = 10000;
                    moneyReq = 0;
                    break;
                case Type.market:
                    woodReq = 10;
                    stoneReq = 10;
                    moneyReq = 10;
                    break;
                default:
                    woodReq = 0;
                    stoneReq = 0;
                    moneyReq = 0;
                    break;
            }
        }

        ~Buildings() { }
    }
}
