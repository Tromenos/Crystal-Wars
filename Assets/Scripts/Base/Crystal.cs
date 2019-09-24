using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Crystal : MonoBehaviour
    {
        #region Variables / Properties

        private byte _teamID;
        private readonly int _maxHealth;
        private int _currentHealth;
        private float _spawnrate;

        private Unit unitPrefab;

        private bool _isBase;
        private bool _isSpawning;

        public byte _GetTeamID { get { return _teamID; } }


        #endregion

        #region Methods

        private void Start()
        {
            _currentHealth = _maxHealth;
            _isSpawning = true;
            StartCoroutine(Spawning());
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
            Instantiate(unitPrefab.gameobject);
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
                else { _teamID = 0; }
            }
        }

        #endregion
    }
}
