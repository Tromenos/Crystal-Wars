using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Crystal : MonoBehaviour
    {
        private static Color team0c = Color.red;
        private static Color team1c = Color.blue;
        private static Color noTeamc = Color.gray;

        #region Variables / Properties
        public bool _spawn;
        [SerializeField]
        private byte _teamID;
        private readonly int _maxHealth = 1;
        private int _currentHealth;
        private float _spawnrate;

        [SerializeField]
        private Unit unitPrefab;

        private bool _isBase = false;
        private bool _isSpawning;
        private Dictionary<UnitID, Unit> _attackingEnemiesDic;

        public byte _GetTeamID { get { return _teamID; } }

        private Material _mat;

        [SerializeField]
        UnitData _myData;

        #endregion

        #region Methods

        private void Start()
        {
            Unit.Data = _myData;
            _attackingEnemiesDic = new Dictionary<UnitID, Unit>();
            _currentHealth = _maxHealth;
            _isSpawning = true;
            //StartCoroutine(Spawning());
            if(_spawn)
                SpawnUnit();
            _mat = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                StopAllCoroutines();
            }
        }

        private IEnumerator Spawning()
        {
            while(_isSpawning == true)
            {
                SpawnUnit();
                yield return new WaitForSeconds(2);
            }
        }

        private void SpawnUnit()
        {
            Instantiate(unitPrefab.gameObject);
        }


        public void ConquerCrystal(byte conquerPoints, byte teamID)
        {
            if(_currentHealth < _maxHealth)
            {
                _currentHealth += conquerPoints;
                if(_currentHealth == _maxHealth)
                {
                    _teamID = teamID;
                    if(_mat != null)
                        _mat.color = Color.red;
                }
            }
        }

        public void TakeDamage(byte damage)
        {
            if(_currentHealth > 0)
                _currentHealth -= damage;

            if(_currentHealth == 0)
            {
                Debug.Log("Deathed");
                if(_isBase)
                {
                    //Insert wincondition here
                }

                else
                {
                    _teamID = 0;
                    _isSpawning = false;
                    foreach(var agent in _attackingEnemiesDic)
                    {
                        agent.Value?.GetComponent<Agent>()?.GetControllingMachine?.ChangeState(UnitCommand.conquer);
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("TRIGGERED!!!");
            Unit detectedUnit = other.GetComponent<Unit>();
            if(detectedUnit != null && detectedUnit.TeamID != _teamID && !_attackingEnemiesDic.ContainsKey(detectedUnit.UnitID))
            {
                _attackingEnemiesDic.Add(detectedUnit.UnitID, detectedUnit);
                Agent unitAgent = detectedUnit.GetComponent<Agent>();
                if(unitAgent != null)
                {
                    unitAgent.Crystal = this;
                    unitAgent.GetControllingMachine.ChangeState(UnitCommand.attack);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Debug.Log("detriggered...");
            Unit detectedUnit = other.GetComponent<Unit>();
            if(detectedUnit != null && detectedUnit.TeamID != _teamID && _attackingEnemiesDic.ContainsKey(detectedUnit.UnitID))
            {
                _attackingEnemiesDic.Remove(detectedUnit.UnitID);
            }
        }
        #endregion
    }
}
