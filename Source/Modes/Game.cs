using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Yogi
{
    class Game : Mode
    {
        private Game() { }

        private Game(PictureBox pictureBoxOnBitmap)
        {
            // Set Timer
            myTimer = new Timer();
            myTimer.Tick += new EventHandler(Refresh);

            // gameWindowSize
            this.gameWindowSize = pictureBoxOnBitmap.Size;

            // mainBitmap
            mainBitmap = new Bitmap(gameWindowSize.Width, gameWindowSize.Height);
            mainBitmap.MakeTransparent(Color.Pink);

            // pictureBoxOnBitmap
            this.pictureBoxOnBitmap = pictureBoxOnBitmap;
            pictureBoxOnBitmap.Image = mainBitmap;

            // falling elements
            fallingElements = new List<Element>();

            GameOver.getInstance(pictureBoxOnBitmap);
        }

        private const int moveYogi = 20;
        private const int gameSpeed = 17;

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
        private int moveDown; // Must : Rock.sizeOfImage.Height % moveDown == 0
        private int pixelsFromLastAddNewElemenet;
        private static Game instance;

        public static Game getInstance(PictureBox pictureBoxOnBitmap)
        {
            if (instance == null)
                instance = new Game(pictureBoxOnBitmap);
            return instance;
        }

        public static Game getInstance()
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }

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

            Graphics g = Graphics.FromImage(mainBitmap);

            // Clear bitmap
            g.Clear(Color.Transparent);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Draw falling elements
            foreach (var i in fallingElements)
                g.DrawImage(i.getImage(), new Rectangle(i.position, Rock.sizeOfPicture));

            // Draw Yogi
            g.DrawImage(yogi.getImage(), new Rectangle(yogi.position, YogiBear.sizeOfPicture));

            // Draw Points
            SolidBrush b = new SolidBrush(SystemColors.HotTrack);
            g.DrawString(String.Format("Score: {0}", score.points),
                         new Font("Showcard Gothic", 18F,
                         FontStyle.Regular,
                         GraphicsUnit.Point, ((byte)(0))),
                         b,
                         gameWindowSize.Width - 160, 2);

            b.Dispose();
            g.Dispose();

            pictureBoxOnBitmap.Refresh();
        }

        private void addNewElements()
        {
            if (pixelsFromLastAddNewElemenet == Rock.sizeOfPicture.Height)
                pixelsFromLastAddNewElemenet = 0;
            else
                return;

            int x = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);
            int x2 = x;
            while (x2 == x)
                x2 = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);

            if (_random.Next(3) < 2)
                AddNewBasket(x * Basket.sizeOfPicture.Width);
            if (_random.Next(3) < 2)
                AddNewRock(x2 * Rock.sizeOfPicture.Width);
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
            pixelsFromLastAddNewElemenet += moveDown;
        }

        private void collisionDetection()
        {
            int distanceTop = 5;
            int distanceBottom = 15;
            int distanceSide = 5;

            for (int i = 0; i < fallingElements.Count; i++)
            {
                if (fallingElements[i].position.Y + fallingElements[i].getImage().Height > (gameWindowSize.Height - YogiBear.sizeOfPicture.Height - 24 + distanceTop) && // Z gory
                    fallingElements[i].position.Y < gameWindowSize.Height - 24 - distanceBottom) // Z dolu
                {
                    // Jezeli odleglosc srodkow figur < Od dlugosc sumy srednic
                    if (Math.Abs(fallingElements[i].position.X + Basket.sizeOfPicture.Width / 2 - yogi.position.X - YogiBear.sizeOfPicture.Width / 2) < (YogiBear.sizeOfPicture.Width + Basket.sizeOfPicture.Width) / 2 - distanceSide)
                    {
                        if (fallingElements[i] is Basket)
                        {
                            score.addPoints(100);
                            fallingElements.Remove(fallingElements[i--]);
                        }
                        else if (fallingElements[i] is Rock)
                        {
                            stop();
                            GameOver.getInstance().display();
                        }
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

        public void move(Keys keyPress)
        {
            if (keyPress == Settings.lKey && yogi.position.X >= moveYogi)
                yogi.move(-moveYogi, 0);
            else if (keyPress == Settings.rKey && yogi.position.X + moveYogi < gameWindowSize.Width - YogiBear.sizeOfPicture.Width)
                yogi.move(moveYogi, 0);
            else
                return;

            collisionDetection();
            refreshBitmap();
        }

        public void display()
        {
            startGame = true;
            pausedGame = false;

            score = new Score(0);

            setLevel();

            myTimer.Interval = gameSpeed;
            myTimer.Start();

            fallingElements.Clear();

            yogi = new YogiBear(new Point(gameWindowSize.Width / 2 - ((gameWindowSize.Width / 2) % YogiBear.sizeOfPicture.Width), gameWindowSize.Height - YogiBear.sizeOfPicture.Height - 24));
        }

        private void setLevel()
        {
            switch (Settings.level)
            {
                case Level.EASY:
                    moveDown = 2;
                    break;
                case Level.MEDIUM:
                    moveDown = 4;
                    break;
                case Level.HARD:
                    moveDown = 8;
                    break;
                default:
                    moveDown = 4;
                    break;
            }
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
                pausedGame = false;
                myTimer.Start();
            }
        }

        public void pause()
        {
            if (startGame == true)
            {
                startGame = false;
                pausedGame = true;
                myTimer.Stop();
            }
        }
    }
}
