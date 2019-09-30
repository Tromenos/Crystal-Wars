using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class AttackState : State
    {
        #region Variables / Properties
        Unit _targetUnit;
        Crystal _targetCrystal;
        Unit _connectedUnit;
        #endregion

        #region Methods
        public AttackState(Agent agent, UnitCommand startCommand, Unit targetUnit)

           : base(agent, startCommand)
        {
            _targetUnit = targetUnit;
        }

        public AttackState(Agent agent, UnitCommand startCommand, Crystal targetCrystal)

        : base(agent, startCommand)
        {
            _targetCrystal = targetCrystal;
        }

        public override void StateUpdate()
        {
            base.StateUpdate();
        }

        protected override void OnStateEnter()
        {
            _connectedUnit = _agent.GetComponent<Unit>();
            base.OnStateEnter();
        }

        protected override void OnStateStay()
        {
            //Debug.Log($"I Attack");
            if (_targetCrystal != null && _connectedUnit != null)
            {
                _connectedUnit.Attack(_targetCrystal);
            }

            if (_targetUnit != null && _connectedUnit != null)
            {
                _connectedUnit.Attack(_targetUnit);
            }
            base.OnStateStay();
        }

        protected override void OnStateExit()
        {
            base.OnStateExit();
        }

        #endregion
    }
}
