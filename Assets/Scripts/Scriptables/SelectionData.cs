using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(fileName = "SelectionData")]
    public class SelectionData : ScriptableObject
    {
        public float SelectionRadius = 15f;
        public int SelectionLayerID = 0;
        public int SelectionLayer = 0;
        public int PlaneLayerID = 0;
        public int PlaneLayer = 0;

        private void OnValidate()
        {
            SelectionLayer = 1 << SelectionLayerID;
            PlaneLayer = 1 << PlaneLayerID;
        }
    }
}
