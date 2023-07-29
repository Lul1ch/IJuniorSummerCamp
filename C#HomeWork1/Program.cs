using System;

namespace CSharpHomework1
{
    enum Fraction
    {
        neutral = 0, evil, good
    }
    class Unit
    {
        private string name;
        private Fraction unitFraction;
        private float healthPoints;
        private const float primaryUnitHealth = 100f;
        private int level, baseAttack, protection;

        public Unit(string name, int baseAttack, int protection, Fraction unitFraction)
        {
            this.name = name;
            this.baseAttack = baseAttack;
            this.protection = protection;
            this.unitFraction = unitFraction;
            this.healthPoints = 100f;
            this.level = 1;
        }

        public Fraction GetFraction()
        {
            return unitFraction;
        }

        public bool isUnitFractionNeutral()
        {
            return unitFraction == Fraction.neutral;
        }

        public bool isUnitDead()
        {
            return healthPoints == 0;
        }

        private void IncreaseLevel()
        {
            level++;
            healthPoints = level * primaryUnitHealth;
            Console.WriteLine(name + "'s new level is " + level);
        }

        public void TakeDamage(float takingDamage)
        {
            if (this.isUnitDead())
            {
                Console.WriteLine("Dead unit can not take damage\n");
                return;
            }
            takingDamage *= (1 - (float)protection / 100);
            if (takingDamage > healthPoints)
                takingDamage = healthPoints;
            Console.WriteLine(name + " is taking " + takingDamage + " damage\n");
            healthPoints -= takingDamage;
        }

        public void DealDamage(ref Unit enemy)
        {   
            if (enemy.isUnitDead())
            {
                Console.WriteLine("You can not hit dead enemy\n");
                return;
            } else if (this.isUnitDead())
            {
                Console.WriteLine("Dead unit can not fight\n");
                return;
            }
            float dealingDamage = ((healthPoints / primaryUnitHealth < 0.2f) ? 2 : 1) * baseAttack;
            if (this.GetFraction() == enemy.GetFraction() && !this.isUnitFractionNeutral())
            {
                dealingDamage *= 0.5f;
            } else if (this.GetFraction() != enemy.GetFraction() && !this.isUnitFractionNeutral() && !enemy.isUnitFractionNeutral())
            {
                dealingDamage *= 1.5f;
            }
            Console.WriteLine(name +" is dealing " + dealingDamage + " damage\n");
            enemy.TakeDamage(dealingDamage);
            if (enemy.isUnitDead())
            {
                this.IncreaseLevel();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Unit goblin = new Unit("John",15, 10, Fraction.evil);
            Unit human = new Unit("Uter",10, 20, Fraction.good);
            human.DealDamage(ref goblin);
            goblin.DealDamage(ref human);
        }
    }
}
