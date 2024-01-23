//==========================================================
// Student ID   : S10241624
// Student Name : Hendrik Yong
// Partner ID   : S10257971
// Partner Name : Caden Toh
//==========================================================
namespace PRG_Assignment
{
    class Topping
    {
		private string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

        public Topping()
        {
            
        }

        public Topping(string t)
        {
            Type = t;
        }

        public override string ToString()
        {
            return $"type: {Type}";
        }
    }
}