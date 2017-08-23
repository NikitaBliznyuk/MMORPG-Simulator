using UnityEngine;

namespace GameScreen.Character.StateMachine
{
    public abstract class Action : ScriptableObject 
    {
        public abstract void Act (StateController controller);
    }
}