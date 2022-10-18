using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Vector3 Target_offset;


    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Target.position + Target_offset, .125f);
    }
}
