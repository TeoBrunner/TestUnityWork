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
    public void Enter()
    {
        Settings.Model.Set(C.IsStartInteractable, false);
        Settings.Model.Set(C.IsStopInteractable, false);

        CoroutineRunner.Start(ReelAcceleration());
    }
    private IEnumerator ReelAcceleration()
    {
        float elapsedTime = 0f;
        float startSpeed = 0f;
        float targetSpeed = C.MaxReelSpeed;
        while (elapsedTime < C.ReelAccelerationTime)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / C.ReelAccelerationTime);
            Settings.Model.Set(C.ReelSpeed, currentSpeed);
            yield return null;
        }
        Settings.Model.Set(C.ReelSpeed, targetSpeed);
    }

    [One(C.ReelAccelerationTime)]
    public void Ready()
    {
        Settings.Model.Set(C.IsStartInteractable, false);
        Settings.Model.Set(C.IsStopInteractable, true);

        isReady = true;
    }

    [Bind(C.FSMStopSig)]
    public void OnStopSignal()
    {
        if (isReady)
        {
            Parent.Change("Stopping");
        }
    }
}