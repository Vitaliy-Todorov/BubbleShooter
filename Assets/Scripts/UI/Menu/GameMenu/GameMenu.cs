using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Data;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.System.InputSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Menu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textGold;

        [SerializeField] private Button _save;
        [SerializeField] private Button _load;

        [SerializeField] private Button _plusGold;

        [SerializeField] private Button _mainMenu;
        [SerializeField] private Button _exet;
        
        [SerializeField] private GameObject _saveMenu;
        [SerializeField] private GameObject _loadMenu;

        private GameStateMachine _gameStateMachine;
        private GeneralDataService _generalDataService;
        private SaveLoadService _saveLoadService;

        public void Construct(GameStateMachine gameStateMachine, 
            GeneralDataService generalDataService,
            SaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _generalDataService = generalDataService;
            _saveLoadService = saveLoadService;

            SetTextGold();
        }

        void Start()
        {
            _save.onClick.AddListener(Save);
            _load.onClick.AddListener(Load);
            _plusGold.onClick.AddListener(PlusGold);
            _mainMenu.onClick.AddListener(MainMenu);
            _exet.onClick.AddListener(Exet);
        }

        private void MainMenu()
        {
            _gameStateMachine.Enter<LoadLeveState, string>(AssetAddressAndNames.MainMenuScene);
        }

        private void Save()
        {
            _saveMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Load()
        {
            _loadMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Exet()
        {
            Application.Quit();
        }

        private void PlusGold()
        {
            _generalDataService.PlayerProgressData.GoldenCrystal += 1;
            SetTextGold();
        }

        public void SetTextGold()
        {
            _textGold.text = _generalDataService.PlayerProgressData.GoldenCrystal.ToString();
        }
    }
}
