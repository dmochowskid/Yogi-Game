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

        /// <summary>
        /// Poczatkowa inicjalizacja gry
        /// </summary>
        private Game(PictureBox pictureBoxOnBitmap)
        {
            myTimer = new Timer();
            myTimer.Tick += new EventHandler(Refresh);

            this.gameWindowSize = pictureBoxOnBitmap.Size;
            gameWindowSize.Height -= 24; // 24 - stala wysokosc menu

            mainBitmap = new Bitmap(gameWindowSize.Width, gameWindowSize.Height);
            mainBitmap.MakeTransparent(Color.Pink);

            this.pictureBoxOnBitmap = pictureBoxOnBitmap;
            pictureBoxOnBitmap.Image = mainBitmap;

            fallingElements = new List<Element>();

            GameOver.getInstance(pictureBoxOnBitmap);
        }

        private const int moveYogi = 40; // Na ile pikseli ma sie przesuwac Yogi
        private const int gameSpeed = 17; // Czas (w ms) odswierzania gry

        public bool pausedGame { get; private set; } // Czy gra jest zatrzymana
        public bool startGame { get; private set; } // Czy gra jest aktywana (jeżeli pausedGame=true => startGame=false)
        public Score score { get; private set; } // Aktualny stan punktow
        private List<Element> fallingElements; // Lista aktualnie spadajacych elementow (Rock i Basket)
        private Size gameWindowSize; // Rozmiar okna gry (wlacznie z menu)
        private Bitmap mainBitmap; // Glowna Bitmap'a gry na ktorej jest wszystko rysowane
        private PictureBox pictureBoxOnBitmap; // Kontrolka na ktorej jest umieszczony 'mainBitmap'
        private int moveDown; // Na ile pikseli maja sie przesuwac spadajace elementy
        private int pixelsFromLastAddNewElemenet; // Ile pixely przesunely sie juz spadajace elemnty od ostatniego dodania nowych elementow
                                                  // pixelsFromLastAddNewElemenet == fallingElements.Height => dodanie nowych elementow
        private YogiBear yogi;
        private static Game instance;
        private Random _random = new Random();
        private Timer myTimer;

        public static Game getInstance(PictureBox pictureBoxOnBitmap)
        {
            if (instance == null)
                instance = new Game(pictureBoxOnBitmap);
            return instance;
        }

        public static Game getInstance()
        {
            return instance;
        }

        /// <summary>
        /// Wykonanie wszystkich czynnosci zwiazanych z gra
        /// Odswierzenie okna
        /// </summary>
        private void Refresh(Object myObject, EventArgs myEventArgs)
        {
            if (startGame == false)
                return;

            moveToDown();

            deleteUseless();

            if (pixelsFromLastAddNewElemenet == Rock.sizeOfPicture.Height)
            {
                pixelsFromLastAddNewElemenet = 0;
                addNewElements();
            }

            collisionDetection();

            refreshBitmap();
        }

        /// <summary>
        /// Usuniecie starej i narysowanie nowej zawartosci na bitmape
        /// </summary>
        private void refreshBitmap()
        {
            if (startGame == false)
                return;

            Graphics g = Graphics.FromImage(mainBitmap);

            // Czyszczenie starej zawartosci
            g.Clear(Color.Transparent);
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            // Narysowanie spadajacych elementow
            foreach (var i in fallingElements)
                g.DrawImage(i.getImage(), new Rectangle(i.position, Rock.sizeOfPicture));

            // Narysowanie Yogi'ego
            g.DrawImage(yogi.getImage(), new Rectangle(yogi.position, YogiBear.sizeOfPicture));

            // Napisanie wyniku w gornych prawym rogu
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

        /// <summary>
        /// Dodanie elementow w wylosowanych punktach
        /// Prawdopodobienstwo dodania nowego elementu : 2/3 (67%)
        /// </summary>
        private void addNewElements()
        {
            int basketPoint = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);
            int rockPoint = basketPoint;

            // Losowanie aby punkty byly rozne
            while (rockPoint == basketPoint)
                rockPoint = _random.Next(gameWindowSize.Width / Rock.sizeOfPicture.Width);

            if (_random.Next(3) < 2)
                AddNewBasket(basketPoint * Basket.sizeOfPicture.Width);
            if (_random.Next(3) < 2)
                AddNewRock(rockPoint * Rock.sizeOfPicture.Width);
        }

        /// <summary>
        /// Dodanie nowego kamienia do listy na samej gorze ekranu
        /// </summary>
        /// <param name="x">Wspolrzedna wzgledem osi X nowego elementu</param>
        private void AddNewRock(int x)
        {
            Element tym = new Rock(new Point(x, -Rock.sizeOfPicture.Height));
            fallingElements.Add(tym);
        }

        /// <summary>
        /// Dodanie nowego koszyka do listy na samej gorze ekranu
        /// </summary>
        /// <param name="x">Wspolrzedna wzgledem osi X nowego elementu</param>
        private void AddNewBasket(int x)
        {
            Element tym = new Basket(new Point(x, -Basket.sizeOfPicture.Height));
            fallingElements.Add(tym);
        }

        /// <summary>
        /// Usuniecie z listy wszystkich elementow ktore wykraczaja poza ekran
        /// </summary>
        private void deleteUseless()
        {
            for (int i = 0; i < fallingElements.Count; i++)
                if (fallingElements[i].position.Y >= gameWindowSize.Height) // ?
                    fallingElements.Remove(fallingElements[i--]);
        }

        /// <summary>
        /// Przesuniecie wszystkich spadajacych elementow w dol
        /// </summary>
        private void moveToDown()
        {
            foreach (var i in fallingElements)
                i.move(0, moveDown);
            pixelsFromLastAddNewElemenet += moveDown;
        }

        /// <summary>
        /// Czy dwa elementy koliduja ze soba
        /// </summary>
        /// <param name="first">Pierwszy element</param>
        /// <param name="second">Brugi element</param>
        /// <returns>true - jeżeli elmenty koliduja ze soba
        ///          false - w.p.p</returns>
        private bool isCollision(Element first, Element second)
        {
            int distanceTop = 5;
            int distanceBottom = 15;
            int distanceSide = 5;

            bool isTopCollision = first.position.Y + first.getImage().Height > (gameWindowSize.Height - second.getImage().Height + distanceTop);
            bool isBottomCollision = first.position.Y < second.position.Y + second.getImage().Height - distanceBottom;
            bool isSideCollision = Math.Abs(first.position.X + first.getImage().Width / 2 - second.position.X - second.getImage().Width / 2) < (second.getImage().Width + first.getImage().Width) / 2 - distanceSide;
            return isTopCollision == true && isBottomCollision == true && isSideCollision == true;
        }

        /// <summary>
        /// Sprawdza czy Yogi nie koliduje z jakims elementem, jezeli tak podejmuje odpowiednie dzialanie
        /// </summary>
        private void collisionDetection()
        {
            for (int i = 0; i < fallingElements.Count; i++)
            {
                if (isCollision(fallingElements[i], yogi) == true)
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

        /// <summary>
        /// Czyszczenie obrazu gry
        /// </summary>
        private void clearBitmap()
        {
            Graphics g = Graphics.FromImage(mainBitmap);
            g.Clear(Color.Transparent);
            g.Dispose();
            pictureBoxOnBitmap.Refresh();
        }

        /// <summary>
        /// Przesunie Yogi'ego w odpowienim kierunku
        /// </summary>
        /// <param name="keyPress">Klawisz jaki zostal nacisniety</param>
        public void move(Keys keyPress)
        {
            if (keyPress == Settings.lKey && yogi.position.X >= moveYogi)
                yogi.move(-moveYogi, 0);
            else if (keyPress == Settings.rKey && yogi.position.X + moveYogi <= gameWindowSize.Width - YogiBear.sizeOfPicture.Width)
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

            // Ustawienie Yogi'ego na dole na srodku
            yogi = new YogiBear(new Point(gameWindowSize.Width / 2 - ((gameWindowSize.Width / 2) % YogiBear.sizeOfPicture.Width),
                                          gameWindowSize.Height - YogiBear.sizeOfPicture.Height));
        }

        /// <summary>
        /// Ustawienie poziomu trudnosci (moveDown)
        /// </summary>
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

        /// <summary>
        /// Zatrzymanie gry (Nie ma mozliwosci wznowienia)
        /// </summary>
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

        /// <summary>
        /// Wznowienie gry
        /// </summary>
        public void resume()
        {
            if (pausedGame == true)
            {
                startGame = true;
                pausedGame = false;
                myTimer.Start();
            }
        }

        /// <summary>
        /// Wstrzymanie gry
        /// </summary>
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
