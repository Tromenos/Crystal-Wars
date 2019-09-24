using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Unit : MonoBehaviour, IControllable
    {
        public static UnitData Data;
        private byte _hp;
        private byte _bp;

        private Coroutine _routine;

        public void Attack(Unit target)
        {
            target.TakeDamage(Data.AP);
            TakeDamage(Data.AP);
        }

        public void TakeDamage(byte value)
        {
            if(value >= _hp)
                Die();
            else
                _hp -= value;
        }

        public void InteractWith(IInteractable target)
        {
            target.Interact();
        }

        public void Move(Vector3 direction)
        {
            transform.position += direction * Data.MoveSpeed * Time.deltaTime;
        }

        public void MoveTo(Vector3 position)
        {
            if(_routine != null)
                StopCoroutine(_routine);
            _routine = StartCoroutine(CoroutineMoveTo(position));
        }

        private IEnumerator CoroutineMoveTo(Vector3 position)
        {
            while((transform.position - position).magnitude >= 0.1f)
            {
                yield return new WaitForEndOfFrame();
                Move((position - transform.position).normalized);
            }
            _routine = null;
        }

        private void Die()
        {
            //TODO: Add beautiful death effects
            Destroy(gameObject);
        }
    }
}
