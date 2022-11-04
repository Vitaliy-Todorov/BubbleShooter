using System;
using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Menu
{
    public class CallingMainMenu : Button
    {
        private GameMenuGeneral _gameMenuGeneral;

        public void Construct(GameMenuGeneral gameMenuGeneral) => 
            _gameMenuGeneral = gameMenuGeneral;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _gameMenuGeneral.EnableAndDisableMenu();
        }
    }
}