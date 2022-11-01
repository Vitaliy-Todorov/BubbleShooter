using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Services;
using Assets.Scripts.Logic;
using Infrastructure.Services;

namespace Assets.Scripts.Infrastructure.States
{
    public class GameStateMachine
    {
        private Dictionary<Type, IExitablState> _state;
        private IExitablState _activeState;

        DependencyInjection _container = DependencyInjection.Container;

        public GameStateMachine(MainStartup mainStartup)
        {
            SceneLoad sceneLoad = new SceneLoad(mainStartup);
            
            _state = new Dictionary<Type, IExitablState>()
            {
                [typeof(MainStartupState)] = new MainStartupState(this, mainStartup),
                [typeof(LoadLeveState)] = new LoadLeveState(this,
                    _container.GetDependency<GeneralDataService>(),
                    _container.GetDependency<StaticDataService>(),
                    _container.GetDependency<GameFactoryService>(),
                    _container.GetDependency<UIFactoryService>(),
                    sceneLoad),
                [typeof(GameLoopState)] = new GameLoopState(),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPlayLoad>(TPlayLoad playLoad) where TState : class, IPlayLoadState<TPlayLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(playLoad);
        }

        private TState ChangeState<TState>() where TState : class, IExitablState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitablState
        {
            return _state[typeof(TState)] as TState;
        }
    }
}