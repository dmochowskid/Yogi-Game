using System.Drawing;

namespace Yogi
{
    class Rock : Element
    {
        public Rock(Point startPosition)
        {
            position = startPosition;
        }

        public static Size sizeOfPicture = new Size(Yogi.Properties.Resources.Rock.Size.Width, Yogi.Properties.Resources.Rock.Height);

        public Point position { get; private set; }
        
        public Bitmap getImage()
        {
            return Yogi.Properties.Resources.Rock;
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
