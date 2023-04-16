using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniteStateMachine.Models
{
    public class WordsNFA : NFA
    {
        public WordsNFA(List<string> vocab)
        {

        }

        public void CreateStates(List<string> vocab)
        {
            accept = new List<State>();
            states = new Dictionary<string, State>();

            for (int i = 0; i < vocab.Count; i++)
            {

            }

        }
    }
}