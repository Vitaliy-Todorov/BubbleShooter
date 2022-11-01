using UnityEngine;

namespace Assets.Scripts.Infrastructure.System.InputSystem
{
    public class KeyboardAndMouseInputSystem : IInputSystem
    {
        private readonly string _vertical = "Vertical";
        private readonly string _horizontal = "Horizontal";

        private Click _click = new Click();
        public Click Click { get => _click; }

        public Vector3 Axis
        {
            get
            {
                Vector3 axis = new Vector3();
                axis.x = Input.GetAxis(_horizontal);
                axis.z = Input.GetAxis(_vertical);

                return axis;
            }
        }

        public bool Shift =>
            Input.GetKey(KeyCode.LeftShift);

        public bool ESC => 
            Input.GetKeyDown(KeyCode.Escape);

        public void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Click.Up = true;
                Click.StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Click.Up = false;
                Click.Active = true;
                Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Click.Up = false;
                Click.Active = false;
            }


            if (Input.GetMouseButtonUp(0))
            {
                Click.Up = true;
                Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}