using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeComponent : MonoBehaviour
{

    [SerializeField]
    private float magnitude;

    private void Awake()
    {
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Random.insideUnitCircle * magnitude;
    }
}
