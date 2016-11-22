using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Entities
    {
        public struct Position
        {
            public int X;
            public int Y;
        }

        Random rnd;
        private int itsAge;
        private int maxAge;
        private bool itsGender;
        private double itsBreedingRate;
        private double itsFightingRate;
        private double itsWorkRate;
        private int itsStrength;
        private int itsHealth;
        private int maxChildren;
        private int maxFight;
        private bool bIsAlive;
        private List<Items> Items;
        private Position pos;

        public void doMove(String direction, World CurrentWorld)
        {
            Position previous = pos;
            switch(direction)
            {
                case "left":
                    pos.X--;
                    break;
                case "right":
                    pos.X++;
                    break;
                case "up":
                    pos.Y--;
                    break;
                case "down":
                    pos.Y++;
                    break;
                default:
                    break;
            }
            if (!CurrentWorld.MoveEntity(previous.X, previous.Y, pos.X, pos.Y))
                pos = previous;
        }

        public Position getPosition()
        {
            return pos;
        }

        public List<Items> itsItems()
        {
            return Items;
        }

        public bool isAlive()
        {
            return bIsAlive;
        }

        public bool isMale()
        {
            return itsGender;
        }

        public void doAge()
        {
            if (this.bIsAlive)
            {
                this.itsAge++;
                if (this.itsAge > this.maxAge)
                    bIsAlive = false;
            }
        }

        public Entities doBreed(Entities context)
        {
            if (this.bIsAlive && context.bIsAlive)
            {
                if (this.itsGender != context.itsGender)
                {
                    if (this.itsBreedingRate >= rnd.NextDouble() && context.itsBreedingRate >= context.rnd.NextDouble())
                    {
                        Console.WriteLine("Breeding !");
                        return new Entities();
                    }
                }
            }
            
            return null;
        }

        public void doFight(Entities context)
        {
            if (this.bIsAlive && context.bIsAlive)
            {
                if (this.itsFightingRate >= rnd.NextDouble() && context.itsFightingRate >= context.rnd.NextDouble())
                {
                    Console.WriteLine("Fighting !");
                    if (this.itsStrength > context.itsStrength)
                        context.doInjury(this.itsStrength - context.itsStrength);
                    else if (this.itsStrength > context.itsStrength)
                        this.doInjury(context.itsStrength - this.itsStrength);
                    else if (this.itsStrength == context.itsStrength)
                    {
                        this.doInjury(rnd.Next(0, this.itsHealth));
                        context.doInjury(context.rnd.Next(0, context.itsHealth));
                    }
                }
            }
        }

        private void doInjury(int damage)
        {
            Console.WriteLine("Damage done : {0} !",damage);
            this.itsHealth -= damage;
            if (this.itsHealth <= 0)
            {
                bIsAlive = false;
                Console.WriteLine("Entity died !");
            }
        }

        public void doWork()
        {
            if (bIsAlive)
            {
                if (itsWorkRate > this.rnd.NextDouble())
                {
                    Items.Type Type = (Items.Type)rnd.Next(0, 4);
                    Items[(int)Type]++;
                    Console.WriteLine("Gathered one of {0}", Type);
                }
            }
        }

        public Entities()
        {
            rnd = new Random(Guid.NewGuid().GetHashCode());
            itsAge = 0;
            maxAge = rnd.Next(60, 80);
            maxChildren = rnd.Next(0, 5);
            maxFight = rnd.Next(0, 10);
            itsGender = rnd.Next(0, 100) >= 50 ? true : false;
            itsBreedingRate = rnd.NextDouble()/maxAge*maxChildren;
            itsFightingRate = rnd.NextDouble()/maxAge*maxFight;
            itsWorkRate = rnd.NextDouble();
            itsStrength = rnd.Next(0, 100);
            itsHealth = rnd.Next(80, 100);
            bIsAlive = true;
            Items = new List<Items>() { new AI.Items(AI.Items.Type.money), new AI.Items(AI.Items.Type.wood), new AI.Items(AI.Items.Type.stone), new AI.Items(AI.Items.Type.meat) };
            pos.X = rnd.Next(0, 11);
            pos.Y = rnd.Next(0, 11);

            Console.WriteLine("Entity has born !");
        }

        ~Entities()
        {
            Console.WriteLine("Entity has died !");
        }
    }
}
