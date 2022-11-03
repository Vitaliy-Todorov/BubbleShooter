using System;
using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Menu
{
    public class CallingMainMenu : Button
    {
        private IInputSystem _inputSystem;
        private GameMenuGeneral _gameMenuGeneral;

        public void Construct(IInputSystem inputSystem, GameMenuGeneral gameMenuGeneral)
        {
            _inputSystem = inputSystem;
            _gameMenuGeneral = gameMenuGeneral;
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _gameMenuGeneral.EnableAndDisableMenu();
        }
    }
}