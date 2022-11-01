using System;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class SaveLoadMenu : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _buttonsGO;
        
        [SerializeField] 
        private GameObject _cellForSaveGO;
        
        [SerializeField] 
        private Button _mainMenuButton;
        [SerializeField] 
        private GameObject _mainMenuGO;

        private GeneralDataService _generalDataService;
        private SaveLoadService _saveLoadService;
        
        public void Construct(GeneralDataService generalDataService, SaveLoadService saveLoadService)
        {
            _generalDataService = generalDataService;
            _saveLoadService = saveLoadService;
        }

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(MainMenu);

            CreateCellsForSaveLoad();
        }

        private void CreateCellsForSaveLoad()
        {
            foreach (PlayerProgressData playerProgress in _generalDataService.GeneralData.Saves)
                CreatCellForSaveLoad(playerProgress);
            
            _mainMenuButton.transform.SetAsLastSibling();
        }

        private CellForSaveLoad CreatCellForSaveLoad(PlayerProgressData playerProgress)
        {
            GameObject saveLoadButtonGO = Instantiate(_cellForSaveGO, _buttonsGO.transform);
            saveLoadButtonGO.SetActive(true);

            CellForSaveLoad cellForSaveLoad = saveLoadButtonGO.GetComponent<CellForSaveLoad>();
            cellForSaveLoad.Construct(_generalDataService, _saveLoadService, playerProgress);
            
            return cellForSaveLoad;
        }

        private void MainMenu()
        {
            _mainMenuGO.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}