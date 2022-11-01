using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenuGeneral : MonoBehaviour
    {
        [SerializeField] 
        private MainMenu _mainMenu;
        
        [SerializeField] 
        private LoadMenu _loadMenu;


        public void Construct(GameStateMachine gameStateMachine, GeneralDataService generalDataService, SaveLoadService saveLoadService)
        {
            _mainMenu.Construct(gameStateMachine, saveLoadService);
            
            _loadMenu.Construct(generalDataService, saveLoadService);
        }
    }
}