using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.UI.Menu;
using UnityEngine;

namespace UI.Menu
{
    public class GameMenuGeneral : MonoBehaviour
    {
        [SerializeField] 
        private GameMenu _gameMenu;
        
        [SerializeField] 
        private SaveLoadMenu _saveLoadMenu;
        
        public void Construct(GameStateMachine gameStateMachine, GeneralDataService generalDataService, SaveLoadService saveLoadService)
        {
            _gameMenu.Construct(gameStateMachine, generalDataService, saveLoadService);
            
            _saveLoadMenu.Construct(generalDataService, saveLoadService);
        }

        public void EnableMenu()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        public void DisableEnableMenu()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }
}