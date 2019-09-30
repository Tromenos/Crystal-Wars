using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class ConquerState : State
    {
        #region Variables / Properties
        Unit _targetUnit;
        Crystal _targetCrystal;
        Unit _connectedUnit;
        #endregion

        #region Methods

        public ConquerState(Agent agent, UnitCommand startCommand, Crystal targetCrystal)

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
            Debug.Log($"I C");
            if(_targetCrystal != null && _connectedUnit != null)
            {
                _targetCrystal.ConquerCrystal(_connectedUnit.AP, _connectedUnit.TeamID);
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
