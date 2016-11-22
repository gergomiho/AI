using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Program
    {
        static void Main(string[] args)
        {
            World CurrentWorld = new World();
            List<Entities> EntityList = SpawnEntities(100,CurrentWorld);
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            

            int counter = 0;
            int sumMoney = 0;
            int sumStone = 0;
            int sumWood = 0;
            int sumMeat = 0;


            while (true)
            {
                List<Entities> born = new List<Entities>();
                List<Entities> died = new List<Entities>();

                foreach (Entities current in EntityList)
                {
                    current.doAge();
                    current.doWork(CurrentWorld);
                    current.doMove((Entities.Direction)rnd.Next(0,5),CurrentWorld);
                    if (current.isAlive())
                    {
                        foreach (Entities next in EntityList)
                        {
                            if (current != next)
                            {
                                //born.Add(current.doBreed(next));
                                current.doFight(next);
                            }
                        }
                    }
                    else
                        died.Add(current);
                }

                born.RemoveAll(x => x == null);

                foreach (Entities current in born)
                    EntityList.Add(current);

                foreach (Entities current in died)
                    EntityList.Remove(current);

                born.Clear();
                died.Clear();

                foreach (Entities context in EntityList)
                {
                    sumMoney += context.itsItems()[(int)Items.Type.money].Amount();
                    sumStone += context.itsItems()[(int)Items.Type.stone].Amount();
                    sumWood += context.itsItems()[(int)Items.Type.wood].Amount();
                    sumMeat += context.itsItems()[(int)Items.Type.meat].Amount();
                }

                counter++;
                //Console.WriteLine("Round {0} has finished ! Total entities : {1} male, {2} female !", counter, EntityList.Count(x => x.isMale()), EntityList.Count(x => !x.isMale()), EntityList.Count);
                Console.Clear();
                CurrentWorld.DisplayEntityCount();
                
                Console.Title = String.Format("Population : {0} | Money : {1} | Wood : {2} | Stone : {3} | Meat : {4}", EntityList.Count, sumMoney, sumStone, sumWood, sumMeat);
                System.Threading.Thread.Sleep(1000);
            }
        }

        static List<Entities> SpawnEntities(int Amount, World CurrentWorld)
        {
            List<Entities> EntityList = new List<Entities>();
            for(int i = 0; i<Amount; i++)
            {
                Entities temp = new Entities();
                if(CurrentWorld.SpawnEntity(temp.getPosition().X, temp.getPosition().Y, temp))
                    EntityList.Add(temp);
            }

            return EntityList;
        }
    }
}
