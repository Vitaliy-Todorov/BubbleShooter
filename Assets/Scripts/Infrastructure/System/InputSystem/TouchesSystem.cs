using UnityEngine;

namespace Assets.Scripts.Infrastructure.System.InputSystem
{
    public class TouchesSystem : IInputSystem
    {
        private readonly string _vertical = "Vertical";
        private readonly string _horizontal = "Horizontal";

        private Click _click = new Click();

        private bool _blockieren;
        private bool _upAfterBlocking = true;
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
            if (_blockieren)
            {
                _upAfterBlocking = false;
                return;
            }
            if (UpAfterBlocking())
            {
                _upAfterBlocking = true;
                return;
            }
            
            Click.Up = false;
            Click.Active = false;
            Click.Down = false;
            
            if (Input.touchCount > 0)
                RefreshByClicking();
        }

        public void Block() => 
            _blockieren = true;

        public void UnlockAfterUp() => 
            _blockieren = false;

        public void InstantlyUnlock()
        {
            _blockieren = false;
            _upAfterBlocking = true;
        }

        private bool UpAfterBlocking() => 
            Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !_upAfterBlocking;

        private void RefreshByClicking()
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Click.Down = true;
                    Click.StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                case TouchPhase.Moved:
                    Click.Down = false;
                    Click.Active = true;
                    Click.Up = false;
                    Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                case TouchPhase.Ended:
                    Click.Down = false;
                    Click.Active = false;
                    Click.Up = true;
                    Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    break;

                default:
                    Click.Up = false;
                    Click.Active = false;
                    Click.Down = false;
                    break;
            }
        }
    }
}