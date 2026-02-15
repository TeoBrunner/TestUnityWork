using AxGrid;
using AxGrid.FSM;
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
        
        CoroutineRunner.Start(ReelDeceleration());
    }
    private IEnumerator ReelDeceleration()
    {
        float elapsedTime = 0f;
        float startSpeed = Settings.Model.Get<float>(C.ReelSpeed);
        float targetSpeed = 0f;
        while (elapsedTime < C.ReelDecelerationTime)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, targetSpeed, elapsedTime / C.ReelDecelerationTime);
            Settings.Model.Set(C.ReelSpeed, currentSpeed);
            yield return null;
        }
        Settings.Model.Set(C.ReelSpeed, targetSpeed);
    }
    [One(C.ReelDecelerationTime)]
    private void Ready()
    {
        Parent.Change("Idle");
    }
}
