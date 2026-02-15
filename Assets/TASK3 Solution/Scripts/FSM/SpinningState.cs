using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;
using System.Collections;
using UnityEngine;

[State("Spinning")]
public class SpinningState : FSMState
{
    private bool isReady = false;
    [Enter]
    private void Enter()
    {
        Settings.Model.Set(C.IsStartInteractable, false);
        Settings.Model.Set(C.IsStopInteractable, false);

        Settings.Model.EventManager.Invoke(C.OnReelStarting);
    }

    [Bind(C.FSMStartedSig)]
    private void Ready()
    {
        Settings.Model.Set(C.IsStartInteractable, false);
        Settings.Model.Set(C.IsStopInteractable, true);

        isReady = true;
    }

    [Bind(C.FSMStopSig)]
    private void OnStopSignal()
    {
        if (isReady)
        {
            Parent.Change("Stopping");
        }
    }
}