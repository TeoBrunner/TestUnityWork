using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[State("Stopping")]
public class StoppingState : FSMState
{
    [Enter]
    private void Enter()
    {
        Settings.Model.Set(C.IsStartInteractable, false);
        Settings.Model.Set(C.IsStopInteractable, false);

        Settings.Model.EventManager.Invoke(C.OnReelStopping);
    }
    [Bind(C.FSMStoppedSig)]
    private void Ready()
    {
        Parent.Change("Idle");
    }
}
