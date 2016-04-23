namespace Yogi
{
    class Score
    {
        /// <summary>
        /// </summary>
        /// <param name="startingPoints">Poczatkowa wartosc punktow</param>
        public Score(int startingPoints)
        {
            points = startingPoints;
        }

        public int points { get; private set; }
        
        /// <summary>
        /// </summary>
        /// <param name="value">Wartosc punktow ktora ma zostac dodana</param>
        public void addPoints(int value)
        {
            points += value;
        }
    }
}
