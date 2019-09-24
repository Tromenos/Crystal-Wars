using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        private int _selectionLayer, _planeLayer;
        [SerializeField]
        private float _maxUnitHeight = 15f;
        Coroutine _boxSelection = null;
        [SerializeField]
        private List<Unit> _selected = new List<Unit>();

        private Vector3 center;
        private Vector3 size;

        private void Start()
        {
            _selectionLayer = 1 << _selectionLayer;
            _planeLayer = 1 << _planeLayer;

            Selection.Cam = _cam;
            Selection.SelectionLayer = _selectionLayer;
            Selection.SelectionRadius = _selectionRadius;
        }

        private void FixedUpdate()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, _planeLayer);


            if(Input.GetMouseButtonDown(0) && hasHit)
                if(_selected != null)
                    Selection.CastSphereSelection(hit);

            if(Input.GetMouseButton(0) && hasHit && _selected != null && _boxSelection == null)
                _boxSelection = StartCoroutine(BoxSelectionRoutine(new Vector3(hit.point.x, -_maxUnitHeight, hit.point.z)));

            if(Input.GetMouseButtonDown(1) && hasHit && _boxSelection == null)
                foreach(var unit in _selected)
                    unit.MoveTo(hit.point);

        }
        private IEnumerator BoxSelectionRoutine(Vector3 pos1)
        {
            RaycastHit hit;
            while(Input.GetMouseButton(0))
            {
                if(Input.GetMouseButtonDown(1))
                {
                    center = Vector3.zero;
                    size = Vector3.zero;
                    yield break;
                }
                if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, _planeLayer))
                {
                    var param = Selection.CalculateBoxParameter(pos1, hit.point + Vector3.up * _maxUnitHeight);
                    center = param.center;
                    size = param.extents;
                }
                yield return new WaitForEndOfFrame();
            }

            Vector3 pos2;
            if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, _planeLayer))
            {
                if(hit.point != pos1)
                {
                    pos2 = hit.point;
                    pos2 = new Vector3(pos2.x, _maxUnitHeight, pos2.z);

                    pos1 = new Vector3(pos1.x, -_maxUnitHeight, pos1.z);

                    Selection.CastBoxSelection(pos1, pos2);
                }
            }
            _boxSelection = null;
        }

        private void OnDrawGizmos()
        {
            var c = Gizmos.color;
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(center, size * 2);
            Gizmos.color = c;
        }
    }
}
