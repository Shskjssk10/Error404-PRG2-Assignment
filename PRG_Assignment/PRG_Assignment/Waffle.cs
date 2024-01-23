using System.Collections.Generic;

namespace PRG_Assignment
{
    class Waffle : IceCream
    {
        private string waffleFlavour;

        public string WaffleFlavour
        {
            get { return waffleFlavour; }
            set { waffleFlavour = value; }
        }

        public Waffle()
        {
            
        }

        public Waffle(string o, int s, List<Flavour> f, List<Topping> t, string wf):base(o,s,f,t)
        {
            waffleFlavour = wf;
        }

        public override double CalculatePrice()
        {
            return 6.5;
        }

        public override string ToString()
        {
            return $"waffle class working";
        }
    }
}