using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPersistentSingleton<T> : MonoBehaviour where T : NonPersistentSingleton<T>
{
    public static T Instance;

    public void Initialize()
    {
        if (Instance && Instance != (T)this)
        {
            Debug.LogError("Multiple " + Instance.name + " scripts found. There should be only one.");
            return;
        }
        else
        {
            Instance = (T)this;
        }
    }
}
