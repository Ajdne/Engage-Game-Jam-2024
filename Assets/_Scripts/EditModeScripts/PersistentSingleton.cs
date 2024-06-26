using UnityEngine;

/// <summary>
/// Used to make the Singleton scripts shorter.
/// </summary>
/// <typeparam name="T">The script you want to make a Singleton of.</typeparam>
public class PersistentSingleton<T> : MonoBehaviour where T : PersistentSingleton<T>
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
            gameObject.name = Instance.name + "_DDOL";
            DontDestroyOnLoad(gameObject);
        }
    }
}
