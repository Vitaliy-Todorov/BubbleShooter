using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Assets.Scripts.Infrastructure.System.InputSystem
{
    public class KeyboardAndMouseInputSystem : IInputSystem
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

            if (_upAfterBlocking)
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
            Input.GetMouseButtonUp(0) && !_upAfterBlocking;

        private void RefreshByClicking()
        {
            Click.Down = false;
            Click.Up = false;
            Click.Active = false;
            
            if (Input.GetMouseButtonDown(0))
            {
                Click.Down = true;
                Click.StartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Click.Down = false;
                Click.Active = true;
                Click.Up = false;
                Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Click.Down = false;
                Click.Active = false;
                Click.Up = true;
                Click.EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Click.Up = false;
                Click.Active = false;
                Click.Down = false;
            }
        }
    }
}