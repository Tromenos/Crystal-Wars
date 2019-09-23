using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Unit : MonoBehaviour, IControllable
    {
        private static UnitData data;
        private byte _hp;
        private byte _bp;

        public void Attack(Unit target)
        {
            throw new System.NotImplementedException();
        }

        public void TakeDamage(byte value)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable InteractWith(IInteractable target)
        {
            throw new System.NotImplementedException();
        }

        public void Move(Vector3 direction)
        {
            throw new System.NotImplementedException();
        }

        public void MoveTo(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        private void Die()
        {

        }
    }
}
