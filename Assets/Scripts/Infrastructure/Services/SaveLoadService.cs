using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using System.IO;
using System.Linq;
using Assets.Scripts.Infrastructure.States;
using Data;

namespace Assets.Scripts.Infrastructure.Services
{
    public class SaveLoadService : IService
    {
        private GeneralDataService _generalDataService;
        private string _filePath;
        private GameStateMachine _gameStateMachine;

        public SaveLoadService(GameStateMachine gameStateMachine, GeneralDataService generalDataService, string filePath)
        {
            _gameStateMachine = gameStateMachine;
            _generalDataService = generalDataService;
            _filePath = filePath + "save";
        }

        public void SaveGeneralPlayerDataForFile()
        {
            GeneralData generalData = _generalDataService.GeneralData;
            File.WriteAllText(_filePath, generalData.ToJson());
        }

        public void SavePlayerProgress(int saveID)
        {
            List<PlayerProgressData> saves = _generalDataService.GeneralData.Saves;

            // if (saves.Any(save => save.ID == saveID))
            if (saves.Count > saveID)
                saves[saveID] = _generalDataService.PlayerProgressData;
            else
            {
                _generalDataService.PlayerProgressData.ID = saves.Count;
                saves.Add(_generalDataService.PlayerProgressData);
            }
            
            SaveGeneralPlayerDataForFile();
        }

        public void LoadGeneralPlayerDataToFile()
        {
            if (File.Exists(_filePath))
            {
                string strJsonData = File.ReadAllText(_filePath);
                _generalDataService.GeneralData = (GeneralData) strJsonData?.ToDeserialized<GeneralData>();
            }
        }

        public void LoadPlayerProgress(int saveID)
        {
            _generalDataService.PlayerProgressData = 
                _generalDataService.GeneralData.Saves[saveID];
            
            _gameStateMachine.Enter<LoadLeveState, string>(_generalDataService.PlayerProgressData.SceneName);
        }
    }
}