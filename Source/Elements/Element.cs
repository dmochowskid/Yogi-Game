using System.Drawing;

namespace Yogi
{
    interface Element
    {
        /// <summary>
        /// Ustawia elementu w podanym punkcie
        /// </summary>
        /// <param name="coordinates">Punkt ustawienia wzgledem lewego gornego rogu</param>
        void move(Point coordinates);

        /// <summary>
        /// Przesuwa element
        /// </summary>
        /// <param name="relativeX">Przesuniecie wzgledem osi x</param>
        /// <param name="relativeY">Przesuniecie wzgledem osi y</param>
        void move(int relativeX, int relativeY);

        /// <returns>Obraz elementu</returns>
        Bitmap getImage();

        /// <summary>
        /// Aktualna pozycja elementu
        /// </summary>
        Point position { get; }
    }
}
