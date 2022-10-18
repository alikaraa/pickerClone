using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopItem : MonoBehaviour
{
    [SerializeField] private GameManager _GameManager;
    [SerializeField] private string ItemTuru;
    [SerializeField] private int BonusTopIndex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ToplayiciSinirObjesi"))
        {

            if (ItemTuru == "Palet")
            {
                _GameManager.PaletleriOrtayaCikar();
                gameObject.SetActive(false);
            }
            else
            {
                _GameManager.BonusToplariEkle(BonusTopIndex);
                gameObject.SetActive(false);
            }
            
        }
    }
}
