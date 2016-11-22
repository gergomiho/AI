using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class World
    {
        private Random rnd;
        private int[,] Zone;
        private int xMaxSize;
        private int yMaxSize;
        private Items.Type[,] ZoneType;
        private List<Entities>[,] SpawnedEntities;

        public void DisplayEntityCount()
        {
            for (int x = 0; x < xMaxSize; x++)
            {
                for (int y = 0; y < yMaxSize; y++)
                {
                    switch (ZoneType[x, y])
                    {
                        case Items.Type.money:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case Items.Type.stone:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            break;
                        case Items.Type.wood:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            break;
                        case Items.Type.meat:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        default:
                            break;
                    }
                    Console.Write("[{0}]   ", Zone[x, y]);
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public Items.Type getTerrainType(int x, int y)
        {
            return ZoneType[x, y];
        }

        public bool SpawnEntity(int x, int y, Entities context)
        {
            if (0 <= x && x < xMaxSize && 0 <= y && y < yMaxSize)
            {
                Zone[x, y]++;
                SpawnedEntities[x, y].Add(context);
                return true;
            }
            return false;
        }

        public bool MoveEntity(int xFrom, int yFrom, int xTo, int yTo, Entities context)
        {
            if (0 <= xFrom && xFrom < xMaxSize && 0 <= xTo && xTo < xMaxSize &&
                0 <= yFrom && yFrom < yMaxSize && 0 <= yTo && yTo < yMaxSize)
            {
                if (Zone[xFrom, yFrom] > 0 && SpawnedEntities[xFrom, yFrom].Count > 0)
                {
                    Zone[xFrom, yFrom]--;
                    Zone[xTo, yTo]++;
                    SpawnedEntities[xFrom, yFrom].Remove(context);
                    SpawnedEntities[xTo, yTo].Add(context);
                    return true;
                }
            }
            return false;
        }

        public bool DeSpawnEntity(int x, int y, Entities context)
        {
            if (0 <= x && x < xMaxSize && 0 <= y && y < yMaxSize)
            {
                if (Zone[x, y] > 0 && SpawnedEntities[x, y].Count > 0)
                {
                    Zone[x, y]--;
                    SpawnedEntities[x, y].Remove(context);
                    return true;
                }
            }
            return false;
        }

        public World(int xSize = 10, int ySize = 10)
        {
            rnd = new Random(Guid.NewGuid().GetHashCode());
            xMaxSize = xSize;
            yMaxSize = ySize;
            Zone = new int[xMaxSize, yMaxSize];
            ZoneType = new Items.Type[xMaxSize, yMaxSize];
            SpawnedEntities = new List<Entities>[xMaxSize, yMaxSize];
            for (int x = 0; x < xMaxSize; x++)
            {
                for (int y = 0; y < yMaxSize; y++)
                {
                    ZoneType[x, y] = (Items.Type)rnd.Next(0, 4);
                    SpawnedEntities[x, y] = new List<Entities>();
                }
            }
        }

        ~World() { }
    }
}
