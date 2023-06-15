using UnityEngine;
namespace StateManager
{
    public abstract class State<T> : ScriptableObject where T: MonoBehaviour
    {
        protected T _manager;

        public virtual void Init(T parent)
        {
            _manager = parent;
        }
        public abstract void InputHandler();
        public abstract void Update();
        public abstract void FixedUpdate();
        public abstract void ChangeState();
        public abstract void Exit();

    }
}

