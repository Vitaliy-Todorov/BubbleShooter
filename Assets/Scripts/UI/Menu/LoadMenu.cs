using System;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class LoadMenu : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _buttonsGO;
        
        [SerializeField] 
        private GameObject _loadButtonGO;
        
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

            CreateLoadButtons();
        }

        private void CreateLoadButtons()
        {
            foreach (PlayerProgressData playerProgress in _generalDataService.GeneralData.Saves)
            {
                Button loadButton = CreatButton(playerProgress);
                loadButton.onClick.AddListener(delegate { LoadButton(playerProgress); });
            }
            
            _mainMenuButton.transform.SetAsLastSibling();
        }

        private Button CreatButton(PlayerProgressData playerProgress)
        {
            GameObject loadButtonGO = Instantiate(_loadButtonGO, _buttonsGO.transform);
            loadButtonGO.SetActive(true);
            loadButtonGO.GetComponentInChildren<TextMeshProUGUI>().text = playerProgress.Name;

            return loadButtonGO.GetComponent<Button>();
        }

        private void MainMenu()
        {
            _mainMenuGO.SetActive(true);
            gameObject.SetActive(false);
        }

        private void LoadButton(PlayerProgressData playerProgress)
        {
            _saveLoadService.LoadPlayerProgress(playerProgress.ID);
        }
    }
}