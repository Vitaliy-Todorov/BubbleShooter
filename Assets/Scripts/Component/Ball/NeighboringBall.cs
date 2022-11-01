using System;
using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    [RequireComponent( typeof(Ball) )]
    public class NeighboringBall : MonoBehaviour
    {
        private Ball _thisBall;
        private List<GameObject> _neighboringBalls = new List<GameObject>();

        private void Awake()
        {
            _thisBall = GetComponent<Ball>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Ball ball = col.gameObject.GetComponent<Ball>();

            if (ball != null && ball.Stationary && _thisBall.Color == ball.Color)
                _neighboringBalls.Add(ball.gameObject);
        }

        private void OnDestroy()
        {
            foreach (GameObject neighboringBall in _neighboringBalls)
            {
                if(neighboringBall != null)
                    Destroy(neighboringBall);
            }
        }
    }
}