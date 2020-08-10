using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AstroInfo
{
    [System.Serializable]
    public struct Planet
    {
        public string name;
        public float radius, distanceFromTheSun, avgTemp;
    }
}
