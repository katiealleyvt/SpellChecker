using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniteStateMachine.Models
{
    public class State
    {
        public string Label { get; }
        public Dictionary<string, State> transitions { get; set; }

        public State(List<string> input, string label)
        {
            Label = label;
            transitions = new Dictionary<string, State>();
            input.ForEach(f => transitions.Add(f, null));
            
        }
    }
}