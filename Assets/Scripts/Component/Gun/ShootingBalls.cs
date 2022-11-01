using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;

namespace Component
{
    public class ShootingBalls : MonoBehaviour
    {
        [SerializeField]
        private Transform _gan;
        [SerializeField]
        private float _speedBall;
        [SerializeField]
        private GameObject _ball;

        private Color _gunColor;
        private SpriteRenderer _spriteGun;

        private GeneralDataService _generalDataService;
        private IInputSystem _inputSystem;
        private List<Color> _colorsOfBalls;

        public void Construct(GeneralDataService generalDataService, IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
            _generalDataService = generalDataService;

            _colorsOfBalls = _generalDataService.GeneralData.ColorsOfBalls;
            
            _spriteGun = GetComponent<SpriteRenderer>();
            PaintGun();
        }

        private void Update()
        {
            if (_inputSystem.Click.Up)
                Shoot(_gan, _speedBall, _ball);
        }

        private void Shoot(Transform gun, float speedBall, GameObject ballGOBasic)
        {
            GameObject ballGO = CreateBall(gun, ballGOBasic);
            
            PaintBallAndGun(ballGO);
            BallMove(speedBall, ballGO);
        }

        private static GameObject CreateBall(Transform gun, GameObject ballGOBasic)
        {
            Quaternion ganRotation = gun.transform.rotation;
            Vector3 ganPosition = gun.transform.position;
            
            return Instantiate(ballGOBasic, ganPosition, ganRotation);
        }

        private void PaintBallAndGun(GameObject ballGO)
        {
            Ball ball = ballGO.GetComponent<Ball>();
            ball.Construct(_gunColor);
            
            PaintGun();
        }

        private static void BallMove(float speedBall, GameObject ballGO)
        {
            BallMove ballMove = ballGO.GetComponent<BallMove>();
            ballMove.Construct(speedBall);
        }

        private void PaintGun()
        {
            _gunColor = RandomColor();
            _spriteGun.color = _gunColor;
        }

        private Color RandomColor()
        {
            int randomIndex = Random.Range(0, _colorsOfBalls.Count);
            return _colorsOfBalls[randomIndex];
        }
    }
}
