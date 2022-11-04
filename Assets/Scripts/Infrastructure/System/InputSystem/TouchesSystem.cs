using UnityEngine;

namespace Assets.Scripts.Infrastructure.System.InputSystem
{
    public class TouchesSystem : IInputSystem
    {
        private readonly string _vertical = "Vertical";
        private readonly string _horizontal = "Horizontal";

        private Click _click = new Click();
        
        public bool Blockieren { get; set; }
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
            if (Blockieren)
                return;
            
            if (!(Input.touchCount > 0))
                return;

            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Click.Up = true;
                    Click.StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                case TouchPhase.Moved:
                    Click.Up = false;
                    Click.Active = true;
                    Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                case TouchPhase.Ended:
                    Click.Up = true;
                    Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;
                
                default: 
                    Click.Up = false;
                    Click.Active = false;
                    break;
            }
        }
    }
}