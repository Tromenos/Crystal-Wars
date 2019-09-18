using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;

namespace Prototype
{

    public class StateMachine : MonoBehaviour
    {
        #region Variables / Properties

        private enum States { Idle = 0, Build = 1, Attack = 2 }
        public enum UnitCommand { none = 0, build = 1, attack = 2 }
        private State _currentState;
        private UnitCommand _receivedCommand;
        public UnitCommand _SetUnitCommand { set { _receivedCommand = value; } }

        #endregion

        #region Methods

        private void Update()
        {
            _currentState.StateUpdate();
        }

        private void ChangeState()
        {
            //Command inc
            //_currentstate.exit aufrufen



            //switch ((int)newState)
            //{
            //    case 0: return new IdleState();
            //    case 1: return new BuildState();
            //    case 2: return new AttackState();
            //    default: return new IdleState();
            //}

        }
    }
    #endregion
}