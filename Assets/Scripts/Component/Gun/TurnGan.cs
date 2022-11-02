using Assets.Scripts.Infrastructure.System.InputSystem;
using UnityEngine;

namespace Component
{
    public class TurnGan : MonoBehaviour
    {
        private IInputSystem _inputSystem;

        public void Construct(IInputSystem inputSystem)
        {
            _inputSystem = inputSystem;
        }
        
        private void FixedUpdate()
        {
            Vector2 touchPosition = _inputSystem.Click.EndPosition ;
            Vector2 direction = touchPosition - (Vector2) transform.position;

            Rotation(direction);
        }

        private void Rotation(Vector2 moveIn) => 
            transform.right = moveIn;
    }
}