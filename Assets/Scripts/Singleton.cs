﻿using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
            }
            return instance;
        }
    }


}
