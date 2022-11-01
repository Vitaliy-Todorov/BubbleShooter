using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI 
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _testLevel;
        [SerializeField]
        private Button _load;
        [SerializeField]
        private GameObject _loadMenu;

        [SerializeField]
        private Button _exet;

        private GameStateMachine _gameStateMachine;
        private SaveLoadService _saveLoadService;

        public void Construct(GameStateMachine gameStateMachine, SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        void Start()
        {
            _testLevel.onClick.AddListener(LoadTestLevel);
            _load.onClick.AddListener(Load);
            _exet.onClick.AddListener(Exet);
        }

        private void LoadTestLevel()
        {
            _gameStateMachine.Enter<LoadLeveState, string>(AssetAddressAndNames.TestLevel_1);
        }

        private void Load()
        {
            _loadMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Exet() => 
            Application.Quit();
    }
}

