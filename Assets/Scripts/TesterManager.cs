using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class TesterManager : MonoBehaviour
    {
        public UnitData Data;

        // Start is called before the first frame update
        void Awake()
        {
            Unit.Data = Data;
        }
    }
}
