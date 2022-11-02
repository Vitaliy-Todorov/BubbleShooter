using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Infrastructure.Services;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct GeneralData
    {
        public List<PlayerProgressData> Saves;

        public List<Color> ColorsOfBalls;
        public Vector2 lowerLeftCorner;
        public Vector2 upperRightCorner;

        public GeneralData(StaticDataService staticDataService)
        {
            Saves = new List<PlayerProgressData>();
            ColorsOfBalls = staticDataService.StaticGeneralData.ColorsOfBalls;
            lowerLeftCorner = new Vector2();
            upperRightCorner = new Vector2();
        }
    }
}