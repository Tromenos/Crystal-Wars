using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class MoveState : State
    {
        #region Variables / Properties
        Vector3 actualTarget;
        #endregion

        #region Methods
        public MoveState(Agent agent, UnitCommand startCommand)

           : base(agent, startCommand)
        {

        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        protected override void OnStateEnter()

        {
            actualTarget = _agent.Target;
            _agent.MoveTo();
            base.OnStateEnter();
        }

        protected override void OnStateStay()
        {
            if (_agent.Target != actualTarget)
            {
                _agent.MoveTo();
                actualTarget = _agent.Target;
            }
            //Debug.Log($"I Move");
            base.OnStateStay();
        }

        protected override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion
    }
}
