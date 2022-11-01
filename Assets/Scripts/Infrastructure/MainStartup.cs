using Assets.Scripts.Infrastructure.States;
using System;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public class MainStartup : MonoBehaviour, ICoroutineRanner
    {
        private GameStateMachine _stateMachine;

        public event Action UpdateEvent;
        public event Action FixedUpdateEvent;

        private void Awake()
        {
            _stateMachine = new GameStateMachine(this);
            _stateMachine.Enter<MainStartupState>();

            DontDestroyOnLoad(this);
        }

        private void Update() =>
            UpdateEvent?.Invoke();

        private void FixedUpdate() =>
            FixedUpdateEvent?.Invoke();
    }
}
