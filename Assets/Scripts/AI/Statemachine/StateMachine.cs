using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Prototype
{
    public enum UnitCommand { none = 0, build = 1, attack = 2 }
    public class StateMachine : MonoBehaviour
    {
        #region Variables / Properties

        //private enum States { Idle = 0, Build = 1, Attack = 2 }

        private State _currentState;
        private State _nextState;
        public State SetNextState { set { _nextState = value; } }

        #endregion

        #region Methods


        private void Update()
        {
            _currentState.StateUpdate();

        }

        public void InitStateMachine()
        {
            //Debug.Log($"Ma component {GetComponent<Agent>()}");
            if (this.GetComponent<Agent>() != null)
            {
                _currentState = new IdleState(this.GetComponent<Agent>(), UnitCommand.none);
                _nextState = _currentState;
            }
            //else
            //{
            //    this.enabled = false;
            //}
        }
        public void ChangeState(UnitCommand newCommand)
        {
            if (_currentState != null)
            {
                _currentState.UnitCommand = newCommand;
                if (_nextState != _currentState)
                {
                    _currentState = _nextState;
                }
            }
        }
    }
    #endregion
}