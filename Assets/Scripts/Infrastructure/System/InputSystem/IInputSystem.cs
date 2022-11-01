using UnityEngine;

namespace Assets.Scripts.Infrastructure.System.InputSystem
{
    public interface IInputSystem
    {
        public Click Click { get; }

        public Vector3 Axis { get; }

        public bool Shift { get; }
        
        public  bool ESC { get; }

        void Update();
    }
}