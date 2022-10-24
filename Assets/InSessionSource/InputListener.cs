using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public interface InputListener 
{
   Action<InputAction.CallbackContext>[] ListenerFunction { get; }
}
