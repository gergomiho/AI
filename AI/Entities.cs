using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI
{
    class Entities
    {
        Random rnd;
        private int itsAge;
        private int maxAge;
        private bool itsGender;
        private double itsBreedingRate;
        private double itsFightingRate;
        private int itsStrength;
        private int itsHealth;
        private int maxChildren;
        private int maxFight;
        private bool bIsAlive;

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
            itsStrength = rnd.Next(0, 100);
            itsHealth = rnd.Next(80, 100);
            bIsAlive = true;

            Console.WriteLine("Entity has born !");
        }

        ~Entities()
        {
            Console.WriteLine("Entity has died !");
        }
    }
}
