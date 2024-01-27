using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    private static T _instance;

    virtual protected void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(this.gameObject);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        print("0");
    }

    public static T Instance
    {
        get
        {
            if (_instance is null)
            {
                Debug.LogError("Scene Context is NULL");
            }
            return _instance;
        }

        protected set { }
    }
}
