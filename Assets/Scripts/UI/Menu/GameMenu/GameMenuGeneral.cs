using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.UI.Menu
{
    public class GameMenuGeneral : MonoBehaviour
    {
        [SerializeField] 
        private GameMenu _gameMenu;
        
        [FormerlySerializedAs("_saveMenu")] [SerializeField] 
        private SaveLoadMenu _saveLoadMenu;


        public void Construct(GameStateMachine gameStateMachine, GeneralDataService generalDataService, SaveLoadService saveLoadService)
        {
            _gameMenu.Construct(gameStateMachine, generalDataService, saveLoadService);
            
            _saveLoadMenu.Construct(generalDataService, saveLoadService);
        }
    }
}