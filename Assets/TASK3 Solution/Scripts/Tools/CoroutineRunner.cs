using AxGrid.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineRunner : MonoBehaviourExt
{
    private static CoroutineRunner instance;
    public static Coroutine Start(IEnumerator coroutine)
    {
        if (instance == null)
        {
            GameObject runnerObject = new GameObject("CoroutineRunner");
            instance = runnerObject.AddComponent<CoroutineRunner>();
            DontDestroyOnLoad(runnerObject);
        }
        return instance.StartCoroutine(coroutine);
    }
}
