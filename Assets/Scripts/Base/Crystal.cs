using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Crystal : MonoBehaviour
    {
        #region Variables / Properties

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
            StartCoroutine(Spawning());
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                TakeDamage(1);
            }
        }

        private IEnumerator Spawning()
        {
            while (_isSpawning == true)
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
            if (_currentHealth < _maxHealth)
            {
                _currentHealth += conquerPoints;
                if (_currentHealth == _maxHealth)
                {
                    _teamID = teamID;
                }
            }
        }

        public void TakeDamage(byte damage)
        {
            if (_currentHealth > 0)
                _currentHealth -= damage;

            if (_currentHealth == 0)
            {

                if (_isBase)
                {
                    //Insert wincondition here
                }

                else
                {
                    _teamID = 0;
                    _isSpawning = false;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Unit detectedUnit = other.GetComponent<Unit>();
            if (detectedUnit != null && detectedUnit.TeamID != _teamID && !_attackingEnemiesDic.ContainsKey(detectedUnit.UnitID))
            {
                _attackingEnemiesDic.Add(detectedUnit.UnitID, detectedUnit);
                Agent unitAgent = detectedUnit.GetComponent<Agent>();
                if (unitAgent != null)
                {
                    unitAgent.GetControllingMachine.ChangeState(UnitCommand.attack);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Unit detectedUnit = other.GetComponent<Unit>();
            if (detectedUnit != null && detectedUnit.TeamID != _teamID && _attackingEnemiesDic.ContainsKey(detectedUnit.UnitID))
            {
                _attackingEnemiesDic.Remove(detectedUnit.UnitID);
            }
        }

        #endregion
    }
}
