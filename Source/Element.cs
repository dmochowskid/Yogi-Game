using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
