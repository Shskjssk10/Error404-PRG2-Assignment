using System.Collections.Generic;

namespace PRG_Assignment
{
    class Cup : IceCream
    {
        public Cup()
        {
            
        }

        public Cup(string o, int s, List<Flavour> f, List<Topping> t) : base(o,s,f,t)
        {

        }

        public override double CalculatePrice()
        {
            return 4.5;
        }

        public override string ToString()
        {
            return $"Type: {Option} Scoops: {Scoops}";
        }
    }
}