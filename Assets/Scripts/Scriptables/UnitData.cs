using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    [CreateAssetMenu(fileName = "UnitData")]
    public class UnitData : ScriptableObject
    {
        [Header("Points")]
        [SerializeField]
        private byte _healthPoints = 0;
        [SerializeField]
        private byte _buildPoints = 0;
        [SerializeField]
        private byte _attackPoints = 0;

        [SerializeField, Header("Speeds")]
        private float buildSpeed = 1f;
        [SerializeField]
        private float moveSpeed = 1f;

        [SerializeField, Header("Team")]
        private byte _teamId = 0;

        public byte HP => _healthPoints;
        public byte BP => _buildPoints;
        public byte AP => _attackPoints;
    }
}
