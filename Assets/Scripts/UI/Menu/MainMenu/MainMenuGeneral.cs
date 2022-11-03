using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.States;
using Infrastructure.Services;
using UI.Menu;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.UI.Menu
{
    public class MainMenuGeneral : MonoBehaviour
    {
        [SerializeField] 
        private LevelMenu _LevelMenu;


        public void Construct(GameStateMachine gameStateMachine, StaticDataService staticDataService)
        {
            _LevelMenu.Construct(gameStateMachine, staticDataService);
        }
    }
}