using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FiniteStateMachine.Models;
using Newtonsoft.Json;

namespace FiniteStateMachine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            return View();
        }
        public ActionResult FindKeywords(string input)
        {
            List<string> words = FindClosestWord(input);
            string jsonWords = Newtonsoft.Json.JsonConvert.SerializeObject(words);
            return Content(jsonWords, "application/json");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public List<string> FindClosestWord(string userWord)
        {
            
            var path = "words.json";
            var text = System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path));
            var words = JsonConvert.DeserializeObject<List<string>>(text);

            //Get words with first char match
            List<string> startingSet = words;
            bool firstChar = false;
            /*for (int i = 0; i < words.Count; i++)
            {
                while (words[i][0].Equals(userWord[0]))
                {
                    startingSet.Add(words[i]);
                    i++;
                    firstChar = true;
                }
                if (firstChar)
                {
                    break;
                }
            }*/

            //Create NFA for each word in starting set
            WordMatch match = null;
            int closestDistance = userWord.Length;
            List<WordMatch> closestWords = new List<WordMatch>();
            foreach (var word in startingSet)
            {
                WordMatch newNFA = new WordMatch(word);
                match = newNFA.CheckInput(userWord) ? newNFA : null;
                if (match!=null)
                {
                    
                    if (match.acceptWord.Length != userWord.Length)
                    {
                        match = null;
                    }
                    else
                    {
                        break;
                    }
                    
                }
                int newDistance = CalcDistance(word, userWord);
                if (closestDistance>newDistance )
                {
                    closestDistance = newDistance;
                    closestWords = new List<WordMatch>();
                    closestWords.Add(newNFA);

                }
                else if (closestDistance == newDistance)
                {
                    closestWords.Add(newNFA);
                }
            }
            if (match != null)
            {
                return new List<string>() { match.acceptWord };
            }
            else
            {
                var wordsString = new List<string>();
                closestWords.ForEach(f => wordsString.Add(f.acceptWord));
                return wordsString;
            }

        }

        public int CalcDistance(string word, string word2)
        {
            int m = word.Length;
            int n = word2.Length;
            int[,] matrix = new int[m + 1, n + 1];


            for (int i = 0; i <= m; i++)
            {
                //col
                matrix[i, 0] = i;
            }
            for (int j = 0; j <= n; j++)
            {
                //row
                matrix[0,j] = j;
            }
            int insertion, substitution, deletion;
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n;j++)
                {
                    if(word[i-1] == word2[j - 1])
                    {
                        matrix[i, j] = matrix[i - 1, j - 1];
                    }
                    else
                    {
                        insertion = matrix[i, j - 1];
                        substitution = matrix[i - 1, j - 1];
                        deletion = matrix[i - 1, j];

                        matrix[i, j] = Math.Min(insertion, substitution);
                        matrix[i, j] = Math.Min(matrix[i, j], deletion) + 1;

                    }
                }
            }
            return matrix[m, n];

        }

    }
}