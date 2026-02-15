public static class C
{
    //Model Keys
    public const string ReelSpeed = "ReelSpeed";
    public const string MaxReelSpeed = "MaxReelSpeed";
    public const string ReelAccelerationTime = "ReelAccelerationTime";
    public const string ReelDecelerationTime = "ReelDecelerationTime";

    //FSM Signals
    public const string FSMStartSig = "StartSignal";
    public const string FSMStartedSig = "StartedSignal";
    public const string FSMStopSig = "StopSignal";
    public const string FSMStoppedSig = "StoppedSignal";

    //Model Events
    public const string OnReelStarting = "OnReelStarted";
    public const string OnReelStopping = "OnReelStopped";

    //View Bindings
    public const string IsStartInteractable = "StartBtnInteractable";
    public const string IsStopInteractable = "StopBtnInteractable";
    public const string OnStartClick = "OnStartClick";
    public const string OnStopClick = "OnStopClick";

}