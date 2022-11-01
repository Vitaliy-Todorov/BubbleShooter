using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public struct PlayerProgressData
    {
        public int ID;
        public string Name;
        public string SceneName;
        
        public int GoldenCrystal;
    }
}