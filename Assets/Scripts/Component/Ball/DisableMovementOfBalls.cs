using UnityEngine;

namespace Component
{
    public class DisableMovementOfBalls : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            BallMove ballMove = col.gameObject.GetComponent<BallMove>();

            ballMove?.DisableMovement();
        }
    }
}