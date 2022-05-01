using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraduateAssessment.Services
{
    public class GoodMatch
    {
        //Calculate sequence based on sentence
        public static string calcSequence(string sentence)
        {
            string sequence = "";

            while (sentence.Length > 0)
            {
                int occur = 0;
                for (int i = 0; i < sentence.Length; i++) //Iterate through sentence checking each letter for number of occurences
                {
                    if (Char.IsWhiteSpace(sentence[0]))
                    {
                        sentence = sentence.Replace(sentence[0].ToString(), string.Empty);
                    }

                    if (sentence[0] == sentence[i])
                    {
                        occur++;
                    }
                }

                if (occur != 0)
                {
                    sequence += occur; //add occurences of letter to end of sequence
                    sentence = sentence.Replace(sentence[0].ToString(), string.Empty);
                }
            }
            return sequence;
        }

        //Add first and last digit of sequence and put its sum at end of result creating new reduced sequence
        public static string sumSequence(string sequence)
        {
            string sum = "";

            while (sequence.Length > 0)
            {
                if (sequence.Length == 1)
                {
                    sum += sequence;
                    sequence = string.Empty;
                }
                else
                {
                    sum += int.Parse(sequence[0].ToString()) + int.Parse(sequence[sequence.Length - 1].ToString());
                    sequence = sequence.Substring(1, sequence.Length - 2);
                }

            }

            return sum;
        }

        //Repeat process until sequence reduced to last 2 digits 
        public static string findPercentage(string sequence)
        {
            string percent = sumSequence(sequence);

            while (percent.Length > 2)
            {
                percent = sumSequence(percent);
            }

            return percent;
        }

        //Indicate if match is good
        public static string appendSentence(string sentence, string percent)
        {
            if (int.Parse(percent) > 80)
            {
                sentence += " " + percent + "%, GOOD MATCH.";
            }
            else
            {
                sentence += " " + percent + "%";
            }

            return sentence;
        }

        //Check player name only contains alphabetic characters
        public static bool validateInput(string name)
        {
            foreach (char z in name)
            {
                if (!Char.IsLetter(z))
                {
                    return false;
                }
            }

            return true;
        }

        //Get and validate input from user
        public static string getInput(string name)
        {
            while (!validateInput(name))
            {
                Console.Write("Invalid input, please enter a valid name: ");
                name = Console.ReadLine();
            }

            return name;
        }

        //Get input from CSV file
        public static List<string> readFromCSV(string filepath, string category)
        {
            //List to contain names read from CSV file
            List<string> list = new List<string>();

            //Regular expression to validate correct input
            Regex alphabetic = new Regex("^[A-Z]+$");
            Regex catCheck = new Regex("^[MF]$");

            if (File.Exists(filepath))
            {
                if (!filepath.ToLower().EndsWith(".csv"))
                {
                    Console.WriteLine("INVALID FILE TYPE");
                    Logger.Log("Invalid File Type: CSV file required"); //Logging file type error
                }
                else
                {
                    var readlines = File.ReadAllLines(filepath);
                    foreach (var line in readlines)
                    {
                        string[] data = line.Split(',');

                        if (data.Length == 2) //Check correctness of format
                        {
                            data[0] = data[0].Trim().ToUpper();
                            data[1] = data[1].Trim().ToUpper();

                            if (alphabetic.IsMatch(data[0])) //Check name validity
                            {
                                if (catCheck.IsMatch(data[1]))
                                {
                                    if (data[1].Equals(category.ToUpper())) //Check gender validity
                                    {
                                        if (!list.Contains(data[0])) //Handle duplicates
                                        {
                                            list.Add(data[0]); //Add name to list
                                        }
                                    }
                                }
                                else
                                {
                                    Logger.Log("Invalid Gender Category: " + line + " | Valid Genders => m/f");
                                }
                            }
                            else
                            {
                                Logger.Log("Invalid Name: " + line + " | Only Alphabetic Characters Allowed");
                            }
                        }
                        else
                        {
                            Logger.Log("Invalid Input: " + line + " | Correct Format => [name], [m/f]");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("FILE NOT FOUND");
                Logger.Log(filepath + " Does Not Exist"); //Logging error if file not found
            }

            return list;
        }

        //Print list of players
        public static void printList(List<string> list)
        {
            foreach (string s in list)
            {
                Console.WriteLine(s);
            }
        }
    }
}
