namespace Assets.Scripts.Infrastructure.States
{
    public interface IState : IExitablState
    {
        public void Enter();
    }

    public interface IPlayLoadState<TPlayLoad> : IExitablState
    {
        public void Enter(TPlayLoad playLoad);
    }

    public interface IExitablState
    {
        public void Exit();
    }
}