using System.Drawing;

namespace Yogi
{
    class Basket : Element
    {
        public Basket(Point startPosition)
        {
            position = startPosition;
        }

        public static Size sizeOfPicture = new Size(Yogi.Properties.Resources.Basket.Size.Width, Yogi.Properties.Resources.Basket.Height);
        public Point position { get; private set; }

        public Bitmap getImage()
        {
            return Yogi.Properties.Resources.Basket;
        }

        public void move(Point coordinates)
        {
            position = new Point(coordinates.X, coordinates.Y);
        }

        public void move(int relativeX, int relativeY)
        {
            position = new Point(position.X + relativeX, position.Y + relativeY);
        }
    }
}
