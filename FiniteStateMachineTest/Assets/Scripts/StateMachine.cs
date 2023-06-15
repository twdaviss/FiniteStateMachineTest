using System;
using System.Collections.Generic;
using UnityEngine;

namespace StateManager
{
    public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private List<State<T>> _states;
        private readonly Dictionary<Type, State<T>> _stateByType = new();
        private State<T> _activeState;

        protected virtual void Awake()
        {
            _states.ForEach(s => _stateByType.Add(s.GetType(), s));
            SetState(_states[0].GetType());
        }

        public void SetState(Type newStateType)
        {
            if(_activeState != null)
            {
                _activeState.Exit();
            }

            _activeState = _stateByType[newStateType];
            _activeState.Init(GetComponent<T>());
        }

        private void Update()
        {
            _activeState.InputHandler();
            _activeState.Update();
            _activeState.ChangeState();
        }

        private void FixedUpdate()
        {
            _activeState.FixedUpdate();
        }
    }
}

