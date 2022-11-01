using Assets.Scripts.Infrastructure.System.InputSystem;

namespace Component
{
    public interface IMove
    {
        void Construct(float speeed);
        void DisableMovement();
    }
}