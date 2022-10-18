using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haberci : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ToplayiciSinirObjesi"))
        {
            _GameManager.SiniraGelindi();
        }
    }
}


