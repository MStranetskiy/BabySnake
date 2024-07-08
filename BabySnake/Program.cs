using System;
using System.Collections.Generic;
using System.Threading;
using static BabySnake.Program;


namespace BabySnake
{
    internal class Program
    {
        private static List<string> snake = new List<string>();
        private static List<int> coorBodyX = new List<int>();
        private static List<int> coorBodyY = new List<int>();
        private static int coordHeadX = 1;
        private static int coordHeadY = 1;
        private static string direction="down";
        private static int gameSpeed = 200;
        private static bool play = true;
        private static int score = 0;
        private static int width = 35;
        private static int height = 25;
        private static int remuveFood;
        private static int liveTimeUI;

       static void Main(string[] args)
        {
            var worm = new Food
            {
                appearanceFood = "W",
                foodLive = false,
                addScore = 1,
                collorFood = ConsoleColor.Yellow,
                delaySpawn = 0,
                lifeTime = 0
            };

            var berry = new Food
            {
                appearanceFood = "%",
                foodLive = false,
                addScore = 3,
                collorFood = ConsoleColor.Red,
                delaySpawn = 30,
                lifeTime = 30
            };

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorVisible = false;
            snake.Add("O");
            DrawMap();
            
            while (play)
            {
                MoveSnake(snake);
                Control();
                Thread.Sleep(gameSpeed);
                SpawnFood(worm);
                SpawnFood(berry);
                if (liveTimeUI>0) { ClaerUI(); }
             }
        }

        static void Control()
        {
            if (Console.KeyAvailable)
                {
                    var rk = Console.ReadKey(true).Key;
                    switch (rk)
                    {
                        case ConsoleKey.W:
                            if (coordHeadY > 0 && direction != "down") {
                            direction = "up";
                            }
                            break;
                        case ConsoleKey.S:
                        if (direction != "up") {
                            direction = "down";
                        }
                            break;
                        case ConsoleKey.D:
                        if (direction != "left") {
                            direction = "right"; 
                        }   
                        break;
                        case ConsoleKey.A:
                            if (coordHeadX > 0 && direction != "right") {
                            direction = "left";
                            }
                        break;
                        
                        case ConsoleKey.P:
                            rk = Console.ReadKey(true).Key;
                        break;
                    }
                }
        }
        static void MoveSnake(List<string> snake)
        {          
            var snakeLight = snake.Count;
            
            if (coorBodyX.Count != snakeLight){
                coorBodyX.Add(coordHeadX);
                coorBodyY.Add(coordHeadY);
            }

            Console.SetCursorPosition(coorBodyX[0], coorBodyY[0]);
            Console.Write(" ");

            if (coorBodyX.Count == snakeLight)
            {
                coorBodyX.RemoveRange(0, 1);
                coorBodyY.RemoveRange(0, 1);
            }

            switch (direction)
            {
                case "left":
                    coordHeadX--;
                    break;
                case "right":
                    coordHeadX++;
                    break;
                case "up":
                    coordHeadY--;
                    break;
                case "down":
                    coordHeadY++;
                    break;
            }

            for (int i = 0; i < coorBodyX.Count; i++)
            {
                if (coorBodyX[i] == coordHeadX && coorBodyY[i] == coordHeadY)
                {
                    Dead();
                }
            }

            if(coordHeadX <=0 || coordHeadX >= width) { Dead(); }
            if (coordHeadY >= height || coordHeadY <= 0) { Dead(); }

            Console.SetCursorPosition(coordHeadX, coordHeadY);
            Console.Write(snake[0]);
        }
        static void SpawnFood(Food food) {

            if (food.foodLive) {
                Console.SetCursorPosition(food.foodCoordX, food.foodCoordY);
                Console.ForegroundColor = food.collorFood;
                Console.Write(food.appearanceFood);
                Console.ForegroundColor = ConsoleColor.Blue;

                if (food.lifeTime > 0) {
                    remuveFood++;
                    if (remuveFood == food.lifeTime)
                    {
                        food.foodLive = false;
                        remuveFood = 0;
                        Console.SetCursorPosition(food.foodCoordX, food.foodCoordY);
                        Console.Write(" ");
                    }
                }             
            }

            if (!food.foodLive) {
                var rnd = new Random().Next(food.delaySpawn);

                if (rnd==0)
                {
                    food.foodLive = true;
                    food.GenerateСoordinates(width, height);
                }

                Console.SetCursorPosition(width + 2, 0);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"Очки {score}");
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            if (food.foodCoordX == coordHeadX && food.foodCoordY == coordHeadY && food.foodLive) {
                remuveFood = 0;
                food.foodLive = false;
                snake.Add("@");
                score += food.addScore;
                Console.SetCursorPosition(width + 10, 0);
                Console.ForegroundColor = food.collorFood;
                Console.Write($"+{food.addScore}");
                Console.ForegroundColor = ConsoleColor.Blue;
                liveTimeUI = 5;
            }
        }

        static void DrawMap() {

            for (int cont = 0; cont < 2; cont++){

                switch (cont)
                {
                    case 0:
                        for (int i = 0; i < width+1; i++)
                        {
                            Console.SetCursorPosition(i, 0);
                            Console.Write("-");
                        }
                        for (int i = 0; i < width+1; i++)
                        {
                            Console.SetCursorPosition(i, height);
                            Console.Write("-");
                        }
                        break;

                    case 1:
                        for (int i = 1; i < height; i++)
                        {
                            Console.SetCursorPosition(0, i);
                            Console.Write("|");
                        }
                        for (int i = 1; i < height; i++)
                        {
                            Console.SetCursorPosition(width, i);
                            Console.Write("|");
                        }
                        break;
                }
            }
        }
        static void Dead() {

            Console.SetCursorPosition(53, 1);
            Console.WriteLine("Смерть!");
            play = false;
        }

        static void ClaerUI() {
            liveTimeUI--;      
            if (liveTimeUI==0)
            {
                 Console.SetCursorPosition(width + 10, 0);
                 Console.Write($"   ");
            }
        }
    }
}
