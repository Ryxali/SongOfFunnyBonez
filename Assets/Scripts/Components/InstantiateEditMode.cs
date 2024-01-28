using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateEditMode : MonoBehaviour
{
    [SerializeField]
    private GameObject toInstantiate;

#if UNITY_EDITOR
    private void Awake()
    {
        var go = Instantiate(toInstantiate, transform);
        go.transform.localPosition = Vector3.forward;
        go.transform.localScale = Vector3.one;
        go.transform.localRotation = Quaternion.identity;
    }
#endif
}
