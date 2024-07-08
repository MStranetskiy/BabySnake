using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BabySnake
{
    
    internal class Food
    {
        public int foodCoordX;
        public int foodCoordY;
        public string appearanceFood;
        public bool foodLive;
        public int addScore;
        public ConsoleColor collorFood;
        public int delaySpawn;
        public int lifeTime;

        public void GenerateСoordinates(int width, int height) {
            Random rndSeeds = new Random();
            foodCoordX = new Random(rndSeeds.Next()).Next(2, width-1);
            foodCoordY = new Random(rndSeeds.Next()).Next(2, height-1);
        }
    }
}
