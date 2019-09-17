using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class State
    {
        protected Agent agent;
        protected enum Substates : byte { StateEnter, StateStay, StateExit };
        protected Substates currentSubstate;

        public State()
        {
            currentSubstate = Substates.StateEnter;
        }

        public State(Agent agent)
        {
            this.agent = agent;
            currentSubstate = Substates.StateEnter;
        }

        public virtual void StateUpdate()
        {
            switch (currentSubstate)
            {
                case Substates.StateEnter: OnStateEnter(); break;
                case Substates.StateStay: OnStateStay(); break;
                case Substates.StateExit: OnStateExit(); break;
                default: Debug.Log($"broken AF"); break;
            }
        }

        protected virtual void OnStateEnter()
        {
            currentSubstate = Substates.StateStay;
        }

        protected virtual void OnStateStay()
        {
            currentSubstate = Substates.StateExit;
        }

        protected virtual void OnStateExit()
        {
            currentSubstate = Substates.StateEnter;
        }
    }
}
