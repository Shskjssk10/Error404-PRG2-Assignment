using System.Collections.Generic;

namespace PRG_Assignment
{
    class Cone : IceCream
    {
        private bool dipped;

        public bool Dipped
        {
            get { return dipped; }
            set { dipped = value; }
        }

        public Cone()
        {
            
        }

        public Cone(string o, int s, List<Flavour> f, List<Topping> t) :base(o,s,f,t)
        {
            
        }

        public override double CalculatePrice()
        {
            return 5.5;
        }

        public override string ToString()
        {
            return $"cone class working";
        }
    }
}