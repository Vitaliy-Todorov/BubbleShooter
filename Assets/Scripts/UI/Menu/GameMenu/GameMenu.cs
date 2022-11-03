using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.System.InputSystem;
using Infrastructure.Services;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textGold;

        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _levelsButton;
        [SerializeField] private Button _exet;
        
        [SerializeField] private GameObject _levelsMenu;

        private GameStateMachine _gameStateMachine;
        private GeneralDataService _generalDataService;

        public void Construct(GameStateMachine gameStateMachine, GeneralDataService generalDataService)
        {
            _gameStateMachine = gameStateMachine;
            _generalDataService = generalDataService;

            SetTextGold();
        }

        void Start()
        {
            _mainMenuButton.onClick.AddListener(MainMenu);
            _restartButton.onClick.AddListener(Restart);
            _levelsButton.onClick.AddListener(LevelsMenu);
            _exet.onClick.AddListener(Exet);
        }

        private void MainMenu()
        {
            _gameStateMachine.Enter<LoadLeveState, string>(AssetAddressAndNames.MainMenuScene);
        }

        private void Restart()
        {
            _gameStateMachine.Enter<LoadLeveState, string>(SceneManager.GetActiveScene().name);
        }

        private void LevelsMenu()
        {
            _levelsMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Exet()
        {
            Application.Quit();
        }

        public void SetTextGold()
        {
            _textGold.text = _generalDataService.PlayerProgressData.GoldenCrystal.ToString();
        }
    }
}
