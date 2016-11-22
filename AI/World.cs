using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class World
    {
        int[,] Zone;
        int xMaxSize;
        int yMaxSize;

        public bool SpawnEntity(int x, int y)
        {
            if (Enumerable.Range(0, xMaxSize - 1).Contains(x) && Enumerable.Range(0, yMaxSize - 1).Contains(y))
            {
                Zone[x, y]++;
                return true;
            }
            return false;
        }

        public bool MoveEntity(int xFrom, int yFrom, int xTo, int yTo)
        {
            if (Enumerable.Range(0, xMaxSize-1).Contains(xFrom) && Enumerable.Range(0, xMaxSize-1).Contains(xTo) &&
                Enumerable.Range(0, yMaxSize-1).Contains(yFrom) && Enumerable.Range(0, yMaxSize-1).Contains(yTo))
            {
                Zone[xFrom, yFrom]--;
                Zone[xTo, yTo]++;
                return true;
            }
            return false;
        }

        public bool DeSpawnEntity(int x, int y)
        {
            if (Enumerable.Range(0, xMaxSize - 1).Contains(x) && Enumerable.Range(0, yMaxSize - 1).Contains(y))
            {
                Zone[x, y]--;
                return true;
            }
            return false;
        }

        public World(int xSize = 10, int ySize = 10)
        {
            xMaxSize = xSize;
            yMaxSize = ySize;
            Zone = new int[xMaxSize, yMaxSize];
        }

        ~World() { }
    }
}
