using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGameMng : MonoBehaviour
{

    private static SGameMng _Intance = null;

    public static SGameMng I
    {
        get
        {
            if (_Intance == null)
            {
                Debug.Log("instance is null");
            }
            return _Intance;
        }
    }

    private void Awake()
    {
        _Intance = this;
    }

}
