using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype
{
    public static class Selection
    {
        private static List<Agent> _selected = new List<Agent>();

        public static SelectionData Data;

        public static Agent[] Selected => _selected?.ToArray();

        public static void CastSphereSelection(RaycastHit hit)
        {
            var selected = Physics.OverlapSphere(hit.point, Data.SelectionRadius, Data.SelectionLayer);

            AddSelection(selected);
        }

        private static void AddSelection(Collider[] selection)
        {
            if(selection != null && selection.Length >= 1)
            {
                foreach(var collider in selection)
                {
                    var unit = collider.GetComponent<Agent>();

                    if(unit)
                        _selected.Add(unit);
                }
            }
            else if(selection.Length <= 0)
                _selected.Clear();
        }

        public static void CastBoxSelection(Vector3 pos1, Vector3 pos2)
        {
            var (center, extents) = CalculateBoxParameter(pos1, pos2);
            var selected = Physics.OverlapBox(center, extents, Quaternion.identity, Data.SelectionLayer);

            AddSelection(selected);
        }


        public static (Vector3 center, Vector3 extents) CalculateBoxParameter(Vector3 pos1, Vector3 pos2)
        {
            var vec = (pos2 - pos1) * 0.5f;

            return (pos1 + vec, math.abs(vec));
        }
    }
}
