namespace Yogi
{
    class Score
    {
        public Score(int startingPoints)
        {
            points = startingPoints;
        }

        public int points { get; private set; }
        
        public void addPoints(int value)
        {
            points += value;
        }
    }
}
