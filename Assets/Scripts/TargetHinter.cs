using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHinter : MonoBehaviour
{
    private Transform _Target;

    void Start()
    {
        _Target = GameObject.Find("Target").transform;
    }

    void Update()
    {
        transform.LookAt(_Target.transform);
    }
}
