using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "StaticGeneralData", menuName = "Data/StaticGeneralData")]
    public class StaticGeneralData : ScriptableObject
    {
        public List<Color> ColorsOfBalls;
    }
}