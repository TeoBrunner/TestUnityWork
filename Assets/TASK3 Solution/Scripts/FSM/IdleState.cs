using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

[State("Idle")]
public class IdleState : FSMState
{
    [Enter]
    public void Enter()
    {
        Settings.Model.Set(C.IsStartInteractable, true);
        Settings.Model.Set(C.IsStopInteractable, false);
        Settings.Model.Set(C.ReelSpeed, 0);
    }
    [Bind(C.FSMStartSig)]
    public void OnStartSignal()
    {
        Parent.Change("Spinning");
    }
}
