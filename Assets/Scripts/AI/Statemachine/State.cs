using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class State
    {
        protected Agent _agent;
        protected enum Substates : byte { StateEnter, StateStay, StateExit };
        protected Substates currentSubstate;
        protected UnitCommand _actualUnitCommand;
        protected UnitCommand _newUnitCommand;
        public UnitCommand UnitCommand { set { _newUnitCommand = value; } }

        public State()
        {
            currentSubstate = Substates.StateEnter;
        }

        public State(Agent agent, UnitCommand startCommand)
        {
            _actualUnitCommand = startCommand;
            _agent = agent;
            currentSubstate = Substates.StateEnter;
        }

        public virtual void StateUpdate()
        {
            switch (currentSubstate)
            {
                case Substates.StateEnter: OnStateEnter(); break;
                case Substates.StateStay: OnStateStay(); break;
                case Substates.StateExit: OnStateExit(); break;
                default: Debug.Log($"That didn't work as expected!"); break;
            }
        }

        protected virtual void OnStateEnter()
        {

            currentSubstate = Substates.StateStay;
        }

        protected virtual void OnStateStay()
        {
            //Debug.Log($"StateStayBool {_newUnitCommand != _actualUnitCommand}");
            if (_newUnitCommand != _actualUnitCommand)
            {
                //Debug.Log($"Substate before Change {currentSubstate}");
                currentSubstate = Substates.StateExit;
                //Debug.Log($"Substate after Change {currentSubstate}");
            }
        }

        protected virtual void OnStateExit()
        {
            //Debug.Log($"Ne Command int {(int)_newUnitCommand}");
            switch ((int)_newUnitCommand)
            {
                case 0: _agent.GetControllingMachine.SetNextState = new IdleState(_agent, _newUnitCommand); break;
                case 1: _agent.GetControllingMachine.SetNextState = new BuildState(_agent, _newUnitCommand); break;
                case 2: _agent.GetControllingMachine.SetNextState = new AttackState(_agent, _newUnitCommand); break;
                default: Debug.Log("new idle"); _agent.GetControllingMachine.SetNextState = new IdleState(_agent, _newUnitCommand); break;
            }
        }
    }
}
