//==========================================================
// Student ID   : S10241624
// Student Name : Hendrik Yong
// Partner ID   : S10257971
// Partner Name : Caden Toh
//==========================================================
namespace PRG_Assignment
{
    class Flavour
    {
		private string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		private bool premium;

		public bool Premium
		{
			get { return premium; }
			set { premium = value; }
		}

		private int quantity;

		public int Quantity
		{
			get { return quantity; }
			set { quantity = value; }
		}

        public Flavour()
        {
            
        }

        public Flavour(string t, bool p, int q)
        {
            Type = t;
			Premium = p;
			Quantity = q;
        }

        public override string ToString()
        {
            return $"type: {Type}, Premium: {Premium}, Quantity: {Quantity}";
        }
	}
}