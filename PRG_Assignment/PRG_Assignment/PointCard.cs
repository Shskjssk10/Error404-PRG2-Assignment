﻿//==========================================================
// Student ID   : S10241624
// Student Name : Hendrik Yong
// Partner ID   : S10257971
// Partner Name : Caden Toh
//==========================================================
using System;

namespace PRG_Assignment
{
    class PointCard
    {
		private int points;

		public int Points
		{
			get { return points; }
			set { points = value; }
		}

		private int punchCard;

		public int PunchCard
		{
			get { return punchCard; }
			set { punchCard = value; }
		}

		private string tier = "Ordinary";

        public string Tier
		{
			get { return tier; }
			set { tier = value; } // check if can implement calculation here 
		}

        public PointCard()
        {
            
        }

        public PointCard(int p, int pc)
        {
            Points = p;
			PunchCard = pc;
        }

        public void AddPoints(int point)
        {
            points += point;

            if (points >= 100 && tier != "Gold")
            {
                tier = "Gold";
            }
            else if (points >= 50 && tier != "Silver")
            {
                tier = "Silver";
            }
        }

        public void RedeemPoints(int point)
        {
            points = Math.Max(points - point, 0);
        }

        public void Punch()
        {
            PunchCard += 1;
            if (PunchCard == 11)
            {
                PunchCard = 0;
            }
        }

        public override string ToString()
        {
            return $"{Points}, {PunchCard}, {Tier}";
        }
    }
}