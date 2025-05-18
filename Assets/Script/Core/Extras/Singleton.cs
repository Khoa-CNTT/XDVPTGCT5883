using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (Instance != this)
        {
            Destroy(this.gameObject);
            Debug.LogError($"Multiple instances of {this} were found! All duplicates have been deleted.");
        }
        else
        {
            _instance = this as T;
        }
    }
}