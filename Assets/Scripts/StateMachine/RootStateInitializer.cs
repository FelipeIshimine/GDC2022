using UnityEngine;

public class RootStateInitializer : MonoBehaviour
{
    [Tooltip("Wait for RuntimeScriptableSingletonInitializer")]public bool waitForRssi = true;

    private void Awake()
    {
        if (waitForRssi && !RuntimeScriptableSingletonInitializer.InitializationCompleted)
            RuntimeScriptableSingletonInitializer.WhenInitializationIsDone(RootState.Initialize);
        else
            RootState.Initialize();
    }
}