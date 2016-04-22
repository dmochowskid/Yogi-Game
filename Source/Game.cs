﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yogi
{
    class Game
    {
        public Game(Size gameWindowSize, PictureBox pictureBoxOnBitmap, BestScores bestScores)
        {
            // bestScores
            this.bestScores = bestScores;

            // Set Flags
            startGame = false;
            pausedGame = false;

            // Set Timer
            myTimer = new Timer();
            myTimer.Interval = gameSpeed;
            myTimer.Tick += new EventHandler(Refresh);

            // gameWindowSize
            this.gameWindowSize = gameWindowSize;

            // mainBitmap
            mainBitmap = new Bitmap(gameWindowSize.Width, gameWindowSize.Height);
            mainBitmap.MakeTransparent(Color.Pink);

            // pictureBoxOnBitmap
            this.pictureBoxOnBitmap = pictureBoxOnBitmap;
            pictureBoxOnBitmap.Image = mainBitmap;

            // gameOver
            gOver = new GameOver(gameWindowSize, pictureBoxOnBitmap, bestScores);

            fallingElements = new List<Element>();

            yogi = new YogiBear(new Point(gameWindowSize.Width / 2 - ((gameWindowSize.Width / 2) % YogiBear.sizeOfPicture.Width), gameWindowSize.Height - YogiBear.sizeOfPicture.Height));
        }

        private const int moveYogi = 40;
        private const int moveDown = 4; // Must : Rock.sizeOfImage.Height % moveDown == 0
        private const int gameSpeed = 5;

        public bool pausedGame { get; private set; }
        public bool startGame { get; private set; }
        public Score score { get; private set; }
        private Timer myTimer;
        private List<Element> fallingElements;
        private YogiBear yogi;
        private Random _random = new Random();
        private Size gameWindowSize;
        private Bitmap mainBitmap;
        private PictureBox pictureBoxOnBitmap;
        private GameOver gOver;
        private BestScores bestScores;
        private int pixelFromLastAddNewElemenet;
        
        private void Refresh(Object myObject, EventArgs myEventArgs)
        {
            if (startGame == false)
                return;

            // Przesuniecie wszystkich w dul
            moveToDown();

            // Usuniecie niepotrzebnych
            deleteUseless();

            // Dodanie nowych
            addNewElements();

            // Wykrycie kolizi
            collisionDetection();

            // Wyswietlenie aktualnego stanu
            refreshBitmap();
        }

        private void refreshBitmap()
        {
            if (startGame == false)
                return;

            // Clear bitmap
            Graphics g = Graphics.FromImage(mainBitmap);
            g.Clear(Color.Transparent);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Draw falling elements
            foreach (var i in fallingElements)
                g.DrawImage(i.getImage(), new Rectangle(i.position, Rock.sizeOfPicture));

            // Draw Yogi
            g.DrawImage(yogi.getImage(), new Rectangle(yogi.position.X, yogi.position.Y - 24, YogiBear.sizeOfPicture.Width, YogiBear.sizeOfPicture.Height));

            // Draw Points
            SolidBrush b = new SolidBrush(SystemColors.HotTrack);
            g.DrawString(String.Format("Score: {0}", score.points),
                         new Font("Showcard Gothic", 18F,
                         FontStyle.Regular,
                         GraphicsUnit.Point, ((byte)(0))),
                         b,
                         gameWindowSize.Width - 160, 20);

            b.Dispose();
            g.Dispose();

            pictureBoxOnBitmap.Refresh();
        }

        private void addNewElements()
        {
            if (pixelFromLastAddNewElemenet == Rock.sizeOfPicture.Height)
                pixelFromLastAddNewElemenet = 0;
            else
                return;

            int x = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);
            int x2 = x;
            while (x2 == x)
                x2 = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);
            x *= Rock.sizeOfPicture.Width;
            x2 *= Rock.sizeOfPicture.Width;

            if (_random.Next(3) < 2)
                AddNewBasket(x);
            if (_random.Next(3) < 2)
                AddNewRock(x2);
        }

        private void AddNewRock(int x)
        {
            Element tym = new Rock(new Point(x, -Rock.sizeOfPicture.Height));
            fallingElements.Add(tym);
        }

        private void AddNewBasket(int x)
        {
            Element tym = new Basket(new Point(x, -Basket.sizeOfPicture.Height));
            fallingElements.Add(tym);
        }

        private void deleteUseless()
        {
            for (int i = 0; i < fallingElements.Count; i++)
                if (fallingElements[i].position.Y >= gameWindowSize.Height)
                    fallingElements.Remove(fallingElements[i--]);
        }

        private void moveToDown()
        {
            foreach (var i in fallingElements)
                i.move(0, moveDown);
            pixelFromLastAddNewElemenet += moveDown;
        }

        private void collisionDetection()
        {
            for (int i = 0; i < fallingElements.Count; i++)
            {
                if (fallingElements[i].position.Y > (gameWindowSize.Height - YogiBear.sizeOfPicture.Height - Basket.sizeOfPicture.Height))
                    if (Math.Abs(fallingElements[i].position.X - yogi.position.X + ((Basket.sizeOfPicture.Width - YogiBear.sizeOfPicture.Width) / 2)) < YogiBear.sizeOfPicture.Width / 2)
                    {
                        if (fallingElements[i] is Basket)
                        {
                            score.addPoints(100);
                            fallingElements.Remove(fallingElements[i--]);
                        }
                        else if (fallingElements[i] is Rock)
                        {
                            stop();
                            gOver.disply(score);
                        }
                    }
            }
        }

        private void clearBitmap()
        {
            Graphics g = Graphics.FromImage(mainBitmap);
            g.Clear(Color.Transparent);
            g.Dispose();
            pictureBoxOnBitmap.Refresh();
        }

        public void move(Settings set, Keys keyPress)
        {
            if (keyPress == set.lKey)
                if (yogi.position.X >= moveYogi)
                {
                    yogi.move(-moveYogi, 0);
                    collisionDetection();
                }

            if (keyPress == set.rKey)
                if (yogi.position.X <= gameWindowSize.Width - YogiBear.sizeOfPicture.Width - moveYogi)
                {
                    yogi.move(moveYogi, 0);
                    collisionDetection();
                }
            refreshBitmap();
        }

        public void display()
        {
            myTimer.Start();

            startGame = true;
            pausedGame = false;
            score = new Score(0);

            fallingElements.Clear();

            yogi.move(new Point(gameWindowSize.Width / 2 - ((gameWindowSize.Width / 2) % Rock.sizeOfPicture.Width), gameWindowSize.Height - YogiBear.sizeOfPicture.Height));
        }

        public void stop()
        {
            if (startGame == true)
            {
                startGame = false;
                pausedGame = false;
                myTimer.Stop();
                clearBitmap();
            }
        }

        public void resume()
        {
            if (pausedGame == true)
            {
                startGame = true;
                myTimer.Start();
                pausedGame = false;
            }
        }

        public void pause()
        {
            if (startGame == true)
            {
                startGame = false;
                myTimer.Stop();
                pausedGame = true;
            }
        }
    }
}
