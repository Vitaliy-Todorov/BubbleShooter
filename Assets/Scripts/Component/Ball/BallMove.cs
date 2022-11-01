using System;
using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;

namespace Component
{
    [RequireComponent( typeof(Ball) )]
    public class BallMove : MonoBehaviour, IMove
    {
        public Ball _thisBall;
        private Rigidbody2D _rigidbody;
    
        private float _speed;

        public void Construct(float speeed)
        {
            _speed = speeed;
            
            _thisBall = GetComponent<Ball>();
                        // ?? throw new MissingComponentException($"{nameof(_thisBall)}. The Ball component is missing");
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = _speed * transform.right;
        }

        public void DisableMovement()
        {
            _thisBall.Stationary = true;
            
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            Destroy(rigidbody);
            
            gameObject.AddComponent<DisableMovementOfBalls>();
            
            Destroy(this);
        }
    }
}
