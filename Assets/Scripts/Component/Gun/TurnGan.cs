using UnityEngine;

namespace Component
{
    public class TurnGan : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            // Vector2 direction = touchPosition - (Vector2) transform.position;

            if (touchPosition != new Vector2())
                Rotation(touchPosition);
        }

        private void Rotation(Vector2 moveIn) => 
            transform.right = moveIn;
    }
}