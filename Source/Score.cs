using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
