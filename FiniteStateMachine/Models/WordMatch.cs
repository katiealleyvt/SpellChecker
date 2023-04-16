using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniteStateMachine.Models
{
    public class WordMatch : NFA
    {
        public string acceptWord { get; set; }
        
        public WordMatch(string acceptWord) : base()
        {
            this.acceptWord = acceptWord;
            this.alphabet = new List<string> {
                "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

            this.states = CreateStates();
            
        }
        private Dictionary<string,State> CreateStates()
        {
            accept = new List<State>();
            states = new Dictionary<string, State>();
            
            for (int i = acceptWord.Length+1; i > 0; i--)
            {
                var label = i.ToString();
                states.Add(label, new State(alphabet, label));
                State nextState = null;
                if (i == acceptWord.Length+1)
                {
                    accept.Add(states[label]);
                }
                else
                {
                    nextState = states[(i+1).ToString()];
                    if(i-1 == 0)
                    {
                        start = states[label];
                    }
                    states[label].transitions[acceptWord[i - 1].ToString()] = nextState;
                }
                
            }
            if (states.Count <= 1)
            {
                throw new Exception("Not enough states");
            }
            return states;
            
        }
        

    }
}