using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public class Boidcontroller : MonoBehaviour
    {
        #region Variables / Properties
        private Dictionary<int, Boidling> _boidlingsDic;
        #endregion

        #region Methods

        private void Start()
        {
            _boidlingsDic = new Dictionary<int, Boidling>();
        }

        public void ConnectToController(Boidling connectingBoid)
        {
            _boidlingsDic.Add(connectingBoid.GetInstanceID(), connectingBoid);
        }

        public void DisconnectFromController(Boidling disconnectingBoid)
        {
            _boidlingsDic.Remove(disconnectingBoid.GetInstanceID());
        }

        #endregion
    }
}
