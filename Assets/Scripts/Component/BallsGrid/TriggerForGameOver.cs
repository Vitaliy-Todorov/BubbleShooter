using System;
using Assets.Scripts.Infrastructure.Services;
using Infrastructure.Services;
using UI.Menu;
using UnityEngine;

namespace Component.BallsGrid
{
    [RequireComponent( typeof(BoxCollider2D) )]
    public class TriggerForGameOver : MonoBehaviour
    {
        private GameOverMenuGeneral _gameOverMenuGeneral;
        private UIFactoryService _uiGameFactoryService;
        private SpriteRenderer _triggerSprite;

        public void Construct(UIFactoryService uiGameFactoryService)
        {
            _uiGameFactoryService = uiGameFactoryService;
            
            GetComponent<BoxCollider2D>().isTrigger = true;
            // GetComponent<Rigidbody2D>().constraints
            
            _triggerSprite = GetComponent<SpriteRenderer>();
            MakeTransparent();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Ball ball = col.GetComponent<Ball>();

            if (ball != null && ball.Stationary)
                _uiGameFactoryService.CreateGameOverMenu();
        }

        private void MakeTransparent()
        {
            Color color = _triggerSprite.color;
            color.a = .3f;
            _triggerSprite.color = color;
        }
    }
}