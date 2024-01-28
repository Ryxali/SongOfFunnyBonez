using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnce : MonoBehaviour
{
    [SerializeField]
    private GameObject toInstantiate;

    private static bool instantiated;
    private void Awake()
    {
        if(!instantiated)
        {
            instantiated = true;
            var toInst = Instantiate(toInstantiate);
            Object.DontDestroyOnLoad(toInst);
        }
    }
}
