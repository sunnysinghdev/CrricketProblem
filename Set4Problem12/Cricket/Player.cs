using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cricket
{
    class Player
    {
        private int[] scoreBag;
        int _runsScored = 0;
        int _ballPlayed = 0;
        bool _batted = false;
        Random rnd = new Random();

        public string name = "";
        public bool isOut = false;
        public Player(string name, int[] probablityArray)
        {
            this.name = name;
            generateProbablity(probablityArray);
        }

        public int strike()
        {
            ++_ballPlayed;
            var run = scoreBag[rnd.Next(100)];
            if (run == 7)
            {
                isOut = true;
            }
            else
            {
                _runsScored += run;
            }
            return run;
        }
        public int getRunsScored()
        {
            return _runsScored;
        }
        public int getBallsPlayed()
        {
            return _ballPlayed;
        }
        public void setPlayerHasBatted()
        {
            _batted = true;
        }
        public bool hasBatted()
        {
            return _batted;
        }
        private void generateProbablity(int[] probablity)
        {
            var counter = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            var list = new int[100];
            for (var i = 0; i < 100; i++)
            {
                var num = 0;
                num = rnd.Next(8);// Math.Floor((Math.Random() * 8) + 0);
                if (num == 7)
                {
                    //console.log(num);
                }
                if (counter[num] < probablity[num])
                {
                    counter[num]++;
                    list[i] = num;

                }
                else
                {
                    for (var j = 0; j < 8; j++)
                    {
                        if (counter[j] < probablity[j])
                        {
                            counter[j]++;
                            list[i] = j;

                            break;
                        }
                    }
                }
            }
            //console.log(counter); //= [0, 0, 0, 0, 0, 0, 0, 0];
            scoreBag = list;
        }
    }
}
