using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniteStateMachine.Models
{
    public class NFA
    {
        //Nondeterministic Finite Automata
        public Dictionary<string,State> states { get; set; }
        public List<string> alphabet { get; set;}
        public State start { get; set; }
        public List<State> accept { get; set; }
        public State endingState { get; set; }
        public int distance { get; set; } //distance / acceptWord.Count

        public NFA(Dictionary<string, State> states, List<string> alphabet, State start, List<State> accept)
        {
            this.states = states;
            this.alphabet = alphabet;
            this.start = start;
            this.accept = accept;
            this.distance = 0;
        }
        public NFA()
        {
            
        }

        public bool CheckInput(string input)
        {
            var currentState = start;
            bool calcDistance = true;
            for (int i = 0; i <= input.Length-1; i++)
            {
                currentState = states[currentState.Label].transitions[input[i].ToString()];
                if(currentState == null && !accept.Contains(currentState))
                {
                    calcDistance = false;
                    currentState = start;
                }
                if (calcDistance)
                {
                    distance++;
                }
            }
            endingState = currentState;
            if (accept.Contains(currentState)) { return true; }
            return false;
        }

    }
}