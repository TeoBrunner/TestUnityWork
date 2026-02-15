using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

public class Bootstrapper : MonoBehaviourExt
{
    [OnStart]
    private void Init()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new IdleState());
        Settings.Fsm.Add(new SpinningState());
        Settings.Fsm.Add(new StoppingState());

        Settings.Fsm.Start("Idle");

        Settings.Model.EventManager.AddAction(C.OnStartClick, () => Settings.Fsm.Invoke(C.FSMStartSig));
        Settings.Model.EventManager.AddAction(C.OnStopClick, () => Settings.Fsm.Invoke(C.FSMStopSig));
    }

    [OnUpdate]
    private void FSMTick() => Settings.Fsm.Update(Time.deltaTime);
}
