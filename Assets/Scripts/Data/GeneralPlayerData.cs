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

        public GeneralData(StaticDataService staticDataService)
        {
            Saves = new List<PlayerProgressData>();
            ColorsOfBalls = staticDataService.StaticGeneralData.ColorsOfBalls;
        }
    }
}