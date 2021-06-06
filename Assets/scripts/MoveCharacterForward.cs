using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Command;

namespace Character.Command
{
    public class MoveCharacterForward : ScriptableObject, ICharacterMoveCommand
    {
        public void Execute(GameObject gameObject, float speed)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody>();
            if (rigidBody != null)
            {
                rigidBody.velocity = rigidBody.transform.forward * speed;
            }
        }
    }
}