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
            List<Entities> EntityList = SpawnEntities(20);
            int counter = 0;


            while (true)
            {
                List<Entities> born = new List<Entities>();
                List<Entities> died = new List<Entities>();

                foreach (Entities current in EntityList)
                {
                    current.doAge();
                    if (current.isAlive())
                    {
                        foreach (Entities next in EntityList)
                        {
                            if (current != next)
                            {
                                born.Add(current.doBreed(next));
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

                counter++;
                Console.WriteLine("Round {0} has finished ! Total entities : {1} male, {2} female, ({3})!", counter, EntityList.Count(x => x.isMale()), EntityList.Count(x => !x.isMale()), EntityList.Count);
                System.Threading.Thread.Sleep(1000);
            }
        }

        static List<Entities> SpawnEntities(int Amount)
        {
            List<Entities> EntityList = new List<Entities>();
            for(int i = 0; i<Amount; i++)
            {
                EntityList.Add(new Entities());
            }

            return EntityList;
        }
    }
}
