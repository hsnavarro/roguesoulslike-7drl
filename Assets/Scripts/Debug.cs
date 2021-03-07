using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour
{
    [SerializeField]
    public bool DebugOn = true;
    
    private static bool isDebugActive;


    new static public void print(object message)
    {
        if (isDebugActive) MonoBehaviour.print(message);
    }

    private void Start()
    {
        isDebugActive = DebugOn;
    }
}
