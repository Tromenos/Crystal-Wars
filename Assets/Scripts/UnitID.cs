using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype
{
    public struct UnitID
    {
        ulong _unitID;
        byte _teamID;

        public UnitID(ulong unitID, byte teamID)
        {
            _unitID = unitID;
            _teamID = teamID;
        }
    }
}
