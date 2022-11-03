using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure.Services;
using Data;
using UnityEngine;

namespace Infrastructure.Services
{
    public class StaticDataService : IService
    {
        private const string LevelDataPath = "Data";
        private const string StaticGeneralDataPath = "Data/StaticGeneralData";
        
        public StaticGeneralData StaticGeneralData;
        private Dictionary<string, LevelData> _levelDatas;

        public StaticDataService()
        {
            _levelDatas = 
                Resources
                .LoadAll<LevelData>(LevelDataPath)
                .ToDictionary(levelData => levelData.NameScene, levelData => levelData);

            StaticGeneralData = Resources.Load<StaticGeneralData>(StaticGeneralDataPath);
        }

        public LevelData GetLevelData(string nameScene) => 
            _levelDatas.TryGetValue(nameScene, out LevelData levelData)
            ? levelData
            : null;
    }
}