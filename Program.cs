using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using starlink_greed.Casting;
using starlink_greed.Directing;
using starlink_greed.Services;


namespace starlink_greed
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int SCORE = 0;
        private static int FONT_SIZE = 20;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed";
        private static Color WHITE = new Color(255, 255, 255);
        private static Color GREEN = new Color(0, 128, 0);
        private static Color RED = new Color(255, 0, 0);
        private static int DEFAULT_ARTIFACTS = 40;


        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("0");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the robot
            Actor robot = new Actor();
            robot.SetText("#");
            robot.SetFontSize(FONT_SIZE + 5);
            robot.SetScore(SCORE);
            robot.SetColor(WHITE);
            robot.SetPosition(new Point(MAX_X / 2, 1150));
            cast.AddActor("robot", robot);

            string gem = "*";
            string rock = "O";
            string text = ""; 

            // create the artifacts
            Random random = new Random();
            for (int i = 0; i < DEFAULT_ARTIFACTS; i++)
            {
                if (i % 2 == 0)
                {
                    text = gem;

                }
                else
                {
                    text = rock;
                }

                int x = random.Next(1, COLS);
                // int y = random.Next(1, ROWS);
                int y = 0;
                Point position = new Point(x, y);
                position = position.Scale(CELL_SIZE);

                int yVelocity = random.Next(3, 7);
                Point velocity = new Point(0, yVelocity);

                Artifact artifact = new Artifact();
                artifact.SetText(text);
                artifact.SetFontSize(FONT_SIZE);
                if (i % 2 == 0)
                {
                    artifact.SetColor(GREEN);
                    artifact.SetScore(50);
                }
                else
                {
                    artifact.SetColor(RED);
                    artifact.SetScore(-10);
                }
                artifact.SetPosition(position);
                artifact.SetVelocity(velocity);
                cast.AddActor("artifacts", artifact);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}