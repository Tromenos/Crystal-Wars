using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Agent : MonoBehaviour
    {
        #region Variables / Properties

        private IControllable _myUnit;
        private Vector3 _target;
        private float _distanceToTarget;

        StateMachine _controllingMachine;
        public StateMachine GetControllingMachine { get { return _controllingMachine; } }
        public Vector3 Target { get { return _target; } set { _target = value; } }

        #endregion

        #region Methods

        private void Awake()
        {
            //Debug.Log($"Good Morning Sir, I am {this.gameObject.name}");
            _myUnit = GetComponent<IControllable>();
            Debug.Log($"MyUnit {_myUnit}");
            _controllingMachine = gameObject.AddComponent<StateMachine>();
            _controllingMachine.InitStateMachine();
        }

        private void Update()
        {
            //For Testing Purpose only
            //if (Input.GetKey(KeyCode.I))
            //{
            //    _controllingMachine.ChangeState(UnitCommand.none);
            //}
            //if (Input.GetKey(KeyCode.B))
            //{
            //    _controllingMachine.ChangeState(UnitCommand.build);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    _controllingMachine.ChangeState(UnitCommand.attack);
            //}
        }

        public void MoveTo()
        {
            _myUnit.MoveTo(_target);
        }

        #endregion
    }
}
