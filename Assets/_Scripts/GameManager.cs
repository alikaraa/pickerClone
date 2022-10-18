using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]

public class TopAlaniTeknikIslemler
{
    public Animator TopAlaniAsansor;
    public TextMeshProUGUI SayiText;
    public int AtilmasiGerekenTop;
    public GameObject[] Toplar;


}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ToplayiciObje;
    [SerializeField] private GameObject[] ToplayiciPaletler;
    [SerializeField] private GameObject[] BonusToplar;
    private bool PaletlerVarmi;
    [SerializeField] private GameObject TopKontrolObjesi;
    public bool ToplayiciHareketDurumu; // bazi yerlerde durucak bazi yerlerde devam edicek

    int AtilanTopSayisi;
    private int ToplamCheckPointSayisi;
    private int MevcutCheckPointIndex;

    private float parmakPosX;
    
    [SerializeField] private List<TopAlaniTeknikIslemler> _TopAlaniTeknikIslemler = new List<TopAlaniTeknikIslemler>();

    void Start()
    {
        ToplayiciHareketDurumu = true;
        for (int i = 0; i < _TopAlaniTeknikIslemler.Count; i++)
        {
            _TopAlaniTeknikIslemler[i].SayiText.text =
                AtilanTopSayisi + "/" + _TopAlaniTeknikIslemler[i].AtilmasiGerekenTop;
        }
        

        ToplamCheckPointSayisi = _TopAlaniTeknikIslemler.Count -1;
    }

    void Update()
    {
        if (ToplayiciHareketDurumu)
        {
            ToplayiciObje.transform.position += 5f * Time.deltaTime * ToplayiciObje.transform.forward; //ileri gitsin diye

            if (Time.timeScale != 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10f));
                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            parmakPosX = TouchPosition.x - ToplayiciObje.transform.position.x;
                            break;
                        case TouchPhase.Moved:
                            if (TouchPosition.x - parmakPosX > -1.15 && TouchPosition.x - parmakPosX < 1.15)
                            {
                                ToplayiciObje.transform.position = Vector3.Lerp(ToplayiciObje.transform.position
                                ,new Vector3(TouchPosition.x - parmakPosX, ToplayiciObje.transform.position.y,
                                    ToplayiciObje.transform.position.z),3f);
                            }
                            break;
                    }
                }
                
            }
        }
    }

    public void SiniraGelindi()
    {
        if (PaletlerVarmi)
        {
            ToplayiciPaletler[0].SetActive(false);
            ToplayiciPaletler[1].SetActive(false);
        }
        ToplayiciHareketDurumu = false;
        Invoke("AsamaKontrol",2f);
        Collider[] HitColl = Physics.OverlapBox(TopKontrolObjesi.transform.position,TopKontrolObjesi.transform.localScale / 2,Quaternion.identity);

        int i = 0;
        while (i < HitColl.Length)
        {
            HitColl[i].GetComponent<Rigidbody>().AddForce(new Vector3(0,0,0.8f),ForceMode.Impulse);  //toplari ileri atiyoruz 
            i++;
        }
    }

    public void ToplariSay()
    {
        AtilanTopSayisi++;
        _TopAlaniTeknikIslemler[MevcutCheckPointIndex].SayiText.text =
            AtilanTopSayisi + "/" + _TopAlaniTeknikIslemler[MevcutCheckPointIndex].AtilmasiGerekenTop;
    }

    void AsamaKontrol()
    {
        if (AtilanTopSayisi >= _TopAlaniTeknikIslemler[MevcutCheckPointIndex].AtilmasiGerekenTop)
        {
            
            _TopAlaniTeknikIslemler[MevcutCheckPointIndex].TopAlaniAsansor.Play("Asansor");
            foreach (var item in _TopAlaniTeknikIslemler[MevcutCheckPointIndex].Toplar)
            {
                item.SetActive(false);
            }

            if (MevcutCheckPointIndex == ToplamCheckPointSayisi)
            {
                Debug.Log("Oyun Bitti - Kazandin Paneli");
                Time.timeScale = 0;
            }
            else
            {
                MevcutCheckPointIndex++;
                AtilanTopSayisi = 0;
                if (PaletlerVarmi)
                {
                    ToplayiciPaletler[0].SetActive(true);
                    ToplayiciPaletler[1].SetActive(true);
                }
            }

            
        }
        else
        {
            Debug.Log("Kaybettin - Kaybettin Paneli");
        }
        
    }

    public void PaletleriOrtayaCikar()
    {
        PaletlerVarmi = true;
        ToplayiciPaletler[0].SetActive(true);
        ToplayiciPaletler[1].SetActive(true);
    }

    public void BonusToplariEkle(int BonusTopIndex)
    {
        BonusToplar[BonusTopIndex].SetActive(true);
    }
}
