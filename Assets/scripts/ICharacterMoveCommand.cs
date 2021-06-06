using UnityEngine;

namespace Character.Command
{
    public interface ICharacterMoveCommand
    {
        void Execute(GameObject gameObject, float speed);
    }
}
