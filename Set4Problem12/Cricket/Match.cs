using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cricket
{
    class Match
    {
        
        bool isFinished = false;
        string[] runs_name = new string[] { "0", "1", "2", "3", "4", "5", "6", "OUT" };

        public int requiredRuns = 40;
        public int remainingBall = 24;
        public int score = 0;
        public int playerCount = 2;
        public Player batsman;
        public Player NonStriker;
        public List<Player> players;

        public Match(List<Player> players)
        {
            this.players = players;
            batsman = this.players[0];
            NonStriker = this.players[1];
            batsman.setPlayerHasBatted();
            NonStriker.setPlayerHasBatted();
        }
        public void changeStrike()
        {
            var temp = batsman;
            batsman = NonStriker;
            NonStriker = temp;
        }
        public void addNextPlayer()
        {

            batsman = players[playerCount++];
            batsman.setPlayerHasBatted();
        }
        private void start(int startOver, int endOver)
        {
            for (int over = startOver; over < endOver; over++)
            {
                if (!isFinished)
                {
                    Console.WriteLine("\n" + (endOver - over) + " overs left.  " + (requiredRuns - score) + " runs remaining.");

                    for (int ball = 1; ball <= 6; ball++)
                    {
                        remainingBall--;
                        var over_ball = over + "." + ball + " ";
                        var run_scored = batsman.strike();

                        if (run_scored > 6)
                        {
                            Console.WriteLine(over_ball + batsman.name + " is out !!!.");
                            if (playerCount >= players.Count)
                            {
                                isFinished = true;
                                break;
                            }
                            addNextPlayer();
                        }
                        else
                        {

                            score += run_scored;

                            Console.WriteLine(over_ball + batsman.name + " scored " + runs_name[run_scored] + " runs.");
                            if (run_scored % 2 == 1)
                            {
                                changeStrike();
                            }
                            if (score >= requiredRuns)
                            {
                                isFinished = true;
                                break;
                            }


                        }
                    }
                    changeStrike();
                }
                else
                {
                    break;
                }
            }
        }
        public void Play(int startOver, int endOver){

            
            Console.WriteLine("===============  Match started  ==================\n\n");
            Console.WriteLine("Match Commentry\n\n");
            start(startOver, endOver);
            Console.WriteLine("\n\n=============  Scorecard  ===========================\n\n");

            var wicket = 0;
            for (int count = 0; count < players.Count; count++)
            {
                var player = players[count];
                if (player.hasBatted())
                {
                    if (!player.isOut)
                    {
                        wicket++;
                    }
                    Console.WriteLine(player.name + " - " + player.getRunsScored() + (player.isOut ? "" : "*") + " (" + player.getBallsPlayed() + " balls)");
                }
            }
            Console.WriteLine("=============  Result  ===========================\n\n");
            if (score == requiredRuns - 1)
            {
                
                Console.WriteLine("Match tied");
            }
            else if (score < requiredRuns)
            {
                Console.WriteLine("Team lost by " + (requiredRuns - score) + " runs.");

            }
            else
            {
                Console.WriteLine("Bengaluru won by " + wicket + " wicket" + (remainingBall > 0 ? " and " + remainingBall + " balls remaining." : "."));
            }
        }
    }
}
