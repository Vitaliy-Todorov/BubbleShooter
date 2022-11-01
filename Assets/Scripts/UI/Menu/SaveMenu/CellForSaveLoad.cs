using System;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class CellForSaveLoad: MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputField;
        
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _delete;

        private GeneralDataService _generalDataService;
        private SaveLoadService _saveLoadService;
        private PlayerProgressData _playerProgress;

        public void Construct(GeneralDataService generalDataService, SaveLoadService saveLoadService, PlayerProgressData playerProgress)
        {
            _playerProgress = playerProgress;
            _saveLoadService = saveLoadService;
            _generalDataService = generalDataService;

            _inputField.text = playerProgress.Name;
        }

        private void Start()
        {
            _saveButton.onClick.AddListener(Save);
            _loadButton.onClick.AddListener(Load);
            _delete.onClick.AddListener(Delete);
        }

        private void Save()
        {
            _generalDataService.PlayerProgressData.Name = _inputField.text;
            
            _saveLoadService.SavePlayerProgress(_playerProgress.ID);
        }

        private void Load()
        {
            _saveLoadService.LoadPlayerProgress(_playerProgress.ID);
        }

        private void Delete()
        {
            _generalDataService.GeneralData.Saves.RemoveAt(_playerProgress.ID);
            
            Destroy(gameObject);
        }
    }
}