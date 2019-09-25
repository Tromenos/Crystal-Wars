using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Prototype
{
    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private Camera _cam = null;
        [SerializeField]
        private SelectionData _data = null;
        [SerializeField]
        private float _maxUnitHeight = 15f;
        [SerializeField]
        private Agent[] _selected = null;

        Coroutine _boxSelection = null;

        private float3 center;
        private float3 size;

        private void Start()
        {
            Selection.Data = _data;
        }

        private void FixedUpdate()
        {
            var ray = _cam.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, _data.PlaneLayer);


            if(Input.GetMouseButtonDown(0) && hasHit)
                if(_selected != null)
                    Selection.CastSphereSelection(hit);

            if(Input.GetMouseButton(0) && hasHit && _selected != null && _boxSelection == null)
                _boxSelection = StartCoroutine(BoxSelectionRoutine(new float3(hit.point.x, -_maxUnitHeight, hit.point.z)));

            if(Input.GetMouseButtonDown(1) && hasHit && _boxSelection == null)
                foreach(var agent in Selection.Selected)
                {
                    agent.Target = hit.point;
                    agent.GetControllingMachine.ChangeState(UnitCommand.move);
                }

        }
        private IEnumerator BoxSelectionRoutine(float3 pos1)
        {
            RaycastHit hit;
            while(Input.GetMouseButton(0))
            {
                if(Input.GetMouseButtonDown(1))
                {
                    center = float3.zero;
                    size = float3.zero;
                    StopAllCoroutines();
                    yield break;
                }
                if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, _data.PlaneLayer))
                {
                    var param = Selection.CalculateBoxParameter(pos1, new float3(hit.point.x, _maxUnitHeight, hit.point.z));
                    center = param.center;
                    size = param.extents;
                }
                yield return new WaitForEndOfFrame();
            }

            if(Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, _data.PlaneLayer))
            {
                if(hit.point != (Vector3)pos1)
                {
                    float3 pos2 = hit.point;
                    pos2.y = _maxUnitHeight;

                    pos1.y = -_maxUnitHeight;

                    Selection.CastBoxSelection(pos1, pos2);
                    _selected = Selection.Selected;
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
