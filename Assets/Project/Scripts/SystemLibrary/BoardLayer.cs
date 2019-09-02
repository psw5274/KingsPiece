using System;
using System.Linq;
using UnityEngine;

namespace BoardSystem
{
    [Serializable]
    public abstract class BoardLayer : ScriptableObject
    {
        public abstract BoardCoord[] GetCoordinations();
    }
}