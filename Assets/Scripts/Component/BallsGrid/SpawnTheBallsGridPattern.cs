using System;
using Infrastructure.Services;
using UnityEngine;

namespace Component.BallsGrid
{
    public class SpawnTheBallsGridPattern : MonoBehaviour
    {
        private GameFactoryService _gameFactoryService;
        private Vector2 _upperWall;

        public void Construct(GameFactoryService gameFactoryService)
        {
            _gameFactoryService = gameFactoryService;
            
            Camera camera = Camera.main;
            _upperWall = camera.ViewportToWorldPoint(new Vector2(.5f, 1));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            BallsGridPattern ballsGridPattern = other.GetComponent<BallsGridPattern>();

            if (ballsGridPattern != null)
            {
                GameObject ballsGridPatternGO = _gameFactoryService.CreateBallsGridPattern(ballsGridPattern);
                ballsGridPatternGO.transform.position = _upperWall + new Vector2(0, ballsGridPattern.GridHeight/2 - .5f);
            }
        }
    }
}