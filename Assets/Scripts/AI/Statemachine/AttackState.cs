using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class AttackState : State
    {
        #region Variables / Properties

        #endregion

        #region Methods
        public AttackState(Agent agent, UnitCommand startCommand)

           : base(agent, startCommand)
        {

        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        protected override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        protected override void OnStateStay()
        {
            //Debug.Log($"I Attack");
            base.OnStateStay();
        }

        protected override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion
    }
}
