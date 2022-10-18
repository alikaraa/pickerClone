using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonerKol : MonoBehaviour
{
    private bool Don;
    [SerializeField] private float DonusDegeri;
    public void DonmeyeBasla()
    {
        Don = true;
    }
    private void Update()
    {
        if(Don)
        transform.Rotate(0,0,DonusDegeri,Space.Self);
    }
}
