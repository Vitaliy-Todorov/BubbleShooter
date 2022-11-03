using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Data;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Menu
{
    public class LevelMenu : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _buttonsGO;
        
        [SerializeField] 
        private GameObject _levelButtonGO;
        
        [SerializeField] 
        private Button _mainMenuButton;
        [SerializeField] 
        private GameObject _mainMenuGO;

        private GameStateMachine _gameStateMachine;
        private StaticDataService _staticDataService;

        public void Construct(GameStateMachine gameStateMachine, StaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        private void Start()
        {
            _mainMenuButton.onClick.AddListener(MainMenu);

            CreateLevelButtons();
        }

        private void CreateLevelButtons()
        {
            foreach (KeyValuePair<string, LevelData> levelData in _staticDataService.LevelDatas)
            {
                Button loadButton = CreatButton(levelData.Value);
                loadButton.onClick.AddListener(delegate { LoadButton(levelData.Value); });
            }
            
            _mainMenuButton.transform.SetAsLastSibling();
        }

        private Button CreatButton(LevelData levelData)
        {
            GameObject loadButtonGO = Instantiate(_levelButtonGO, _buttonsGO.transform);
            loadButtonGO.SetActive(true);
            loadButtonGO.GetComponentInChildren<TextMeshProUGUI>().text = levelData.NameLevel;

            return loadButtonGO.GetComponent<Button>();
        }

        private void LoadButton(LevelData levelData) => 
            _gameStateMachine.Enter<LoadLeveState, string>(levelData.NameScene);

        private void MainMenu()
        {
            _mainMenuGO.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}