using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace InSession
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private bool startControllingOnAwake;

        private void Awake()
        {
            PlayerController.MainPlayer.ControlActor(this);
        }

        public void BindInputActions(PlayerInput inputTarget)
        {
            foreach(InputListener inputListener in GetComponents<InputListener>())
            {
                foreach(Action<CallbackContext> function in inputListener.ListenerFunction)
                {
                    inputTarget.onActionTriggered += function;
                }
            }
        }
    }
}