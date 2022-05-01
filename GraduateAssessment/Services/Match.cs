using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduateAssessment.Services
{
    public class Match
    {
        public string playerName1 { get; set; }
        public string playerName2 { get; set; } 
        public string sentence { get; set; } 
        public string sequence { get; set; }
        public int percent { get; set; }

        public Match(string playerName1, string playerName2)
        {
            this.playerName1 = playerName1;
            this.playerName2 = playerName2;

          
            //Get process start time
            var startTime = DateTime.Now;

            //Log process start time
            Logger.Log("Starting matching process at " + startTime);

            //Run good match process
            sentence = (this.playerName1 + " matches " + this.playerName2).ToUpper();
            sequence = GoodMatch.calcSequence(sentence);
            percent = calcPercent(sequence);
            sentence = GoodMatch.appendSentence(sentence, percent.ToString());

            //Log execution time
            Logger.Log("Completed matching process");
            Logger.Log("Execution Time: " + (DateTime.Now - startTime));

        }
        //Store score in int 
        private int calcPercent(string sequence)
        {
            this.percent = int.Parse(GoodMatch.findPercentage(sequence));

            return this.percent;
        }

    }
}
