using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private Camera _cam;
        [SerializeField]
        private float _selectionRadius = 15f;
        [SerializeField]
        private int _selectionLayer;
        [SerializeField]
        private float _maxUnitHeight = 15f;
        Coroutine _boxSelection;
        [SerializeField]
        private List<Unit> _selected = new List<Unit>();

        private Vector3 center;
        private Vector3 size;

        private void Start()
        {
            _selectionLayer = 1 << _selectionLayer;
        }

        private void FixedUpdate()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);

            if(Input.GetMouseButtonDown(0) && hasHit)
                if(_selected != null)
                    CastSphereSelection(hit);

            if(Input.GetMouseButton(0) && hasHit && _selected != null && _boxSelection == null)
                _boxSelection = StartCoroutine(BoxSelectionRoutine(new Vector3(hit.point.x, -_maxUnitHeight, hit.point.z)));

            if(Input.GetMouseButtonDown(1) && hasHit && _boxSelection == null)
                foreach(var unit in _selected)
                    unit.MoveTo(hit.point);

        }

        private void CastSphereSelection(RaycastHit hit)
        {
            var selected = Physics.OverlapSphere(hit.point, _selectionRadius, _selectionLayer);

            AddSelection(selected);
        }

        private void AddSelection(Collider[] selection)
        {
            if(selection != null && selection.Length >= 1)
            {
                foreach(var collider in selection)
                {
                    var unit = collider.GetComponent<Unit>();

                    if(unit)
                        _selected.Add(unit);
                }
            }
            else if(selection.Length <= 0)
                _selected.Clear();
        }

        private void CastBoxSelection(Vector3 pos1, Vector3 pos2)
        {
            var parameter = CalculateBoxParameter(pos1, pos2);
            var selected = Physics.OverlapBox(parameter.Item1, parameter.Item2, Quaternion.identity, _selectionLayer);

            AddSelection(selected);
        }
        private IEnumerator BoxSelectionRoutine(Vector3 pos1)
        {
            RaycastHit hit;
            while(Input.GetMouseButton(0))
            {
                if(Input.GetMouseButtonDown(1))
                {
                    yield break;
                }
                if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    var param = CalculateBoxParameter(pos1, hit.point + Vector3.up * _maxUnitHeight);
                    center = param.Item1;
                    size = param.Item2;
                    //DrawCube(param.Item1, param.Item2);
                }
                yield return new WaitForEndOfFrame();
            }

            Vector3 pos2;
            if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.point != pos1)
                {
                    pos2 = hit.point;
                    pos2 = new Vector3(pos2.x, _maxUnitHeight, pos2.z);

                    pos1 = new Vector3(pos1.x, -_maxUnitHeight, pos1.z);

                    CastBoxSelection(pos1, pos2);
                }
            }
            _boxSelection = null;
        }

        private (Vector3, Vector3) CalculateBoxParameter(Vector3 pos1, Vector3 pos2)
        {
            if(pos2.z < pos1.z)
            {
                var z = pos2.z;
                pos2.z = pos1.z;
                pos1.z = z;
            }
            if(pos2.x < pos1.x)
            {
                var x = pos2.x;
                pos2.x = pos1.x;
                pos1.x = x;
            }

            var vec = pos2 - pos1;

            Vector3 halfExtents = vec * 0.5f;

            return (pos2 - halfExtents
                    , halfExtents);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(center, size);
        }

        private void DrawCube(Vector3 center, Vector3 size)
        {
            var bLR = center - size;
            var bRR = new Vector3(bLR.x + size.x, bLR.y, bLR.z);
            var bLL = new Vector3(bLR.x, bLR.y, bLR.z + size.z);
            var bRL = new Vector3(bRR.x, bRR.y, bRR.z + size.z);
            var tRL = center + size;
            var tLR = new Vector3(bLR.x, tRL.y, bLR.z);
            var tLL = new Vector3(bLL.x, tRL.y, bLL.z);
            var tRR = new Vector3(bRR.x, tRL.y, bRR.z);

            Color c = Color.green;

            Debug.DrawRay(bLR, bRR, c, 100f);
            Debug.DrawRay(tLR, tRR, c, 100f);
            Debug.DrawRay(bLL, bRL, c, 100f);
            Debug.DrawRay(tLL, tRL, c, 100f);
            Debug.DrawRay(bLR, bLL, c, 100f);
            Debug.DrawRay(tLR, tLL, c, 100f);
            Debug.DrawRay(bRR, bRL, c, 100f);
            Debug.DrawRay(tRR, tRL, c, 100f);
            Debug.DrawRay(tLR, bLR, c, 100f);
            Debug.DrawRay(tLL, bLL, c, 100f);
            Debug.DrawRay(tRR, bRR, c, 100f);
            Debug.DrawRay(tRL, bRL, c, 100f);
        }
    }
}
