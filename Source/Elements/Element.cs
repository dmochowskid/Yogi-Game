using System.Drawing;

namespace Yogi
{
    interface Element
    {
        void move(Point coordinates);

        void move(int relativeX, int relativeY);
        
        Bitmap getImage();

        Point position { get; }
    }
}
