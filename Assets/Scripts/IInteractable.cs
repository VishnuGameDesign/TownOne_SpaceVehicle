using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public interface IInteractable
    {
        void CheckForToolMatch(InputValue value);
    }
}