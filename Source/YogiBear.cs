using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogi
{
    class YogiBear : Element
    {
        public YogiBear(Point startPosition)
        {
            position = startPosition;
        }

        public static Size sizeOfPicture = new Size(Yogi.Properties.Resources.Yogi_Bear.Size.Width, Yogi.Properties.Resources.Yogi_Bear.Height);
        public Point position { get; private set; }

        public Bitmap getImage()
        {
            return Yogi.Properties.Resources.Yogi_Bear;
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
