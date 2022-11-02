using System;
using System.Collections.Generic;
using Component.BallsGrid;
using UnityEngine;

namespace Component
{
    [RequireComponent( typeof(Ball) )]
    public class DestroyBalls : MonoBehaviour
    {
        private Ball _thisBall;

        private void Awake()
        {
            _thisBall = GetComponent<Ball>();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Ball ball = col.gameObject.GetComponent<Ball>();

            if (ball != null)
            {
                if (ball.Stationary && _thisBall.Color == ball.Color)
                {
                    Destroy(col.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}