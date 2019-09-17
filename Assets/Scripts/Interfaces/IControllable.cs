using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public interface IControllable
    {
        IEnumerable InteractWith(IInteractable target);
        void Move(Vector3 direction);
        void MoveTo(Vector3 position);
    }
}
