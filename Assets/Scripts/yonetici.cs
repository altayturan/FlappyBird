using UnityEngine;
using System.Collections.Generic;

public class Yonetici : MonoBehaviour
{
    [Header("Boru Ayarları")]
    public GameObject boruPrefab; // Boru prefabı
    public int havuzBoyutu = 5; // Kaç boru önceden oluşturulacak
    public float boruOlusturmaAraligi = 3.0f; // Kaç saniyede bir boru oluşturulacak
    public float spawnX = 1f; // Borunun ekranın sağında doğacağı X konumu

    private List<GameObject> boruHavuzu = new List<GameObject>(); // Object Pool (Obje Havuzu)
    private int mevcutBoruIndex = 0;

    void Start()
    {
        // Object Pooling: Başta belirli sayıda boru oluştur ve sakla
        for (int i = 0; i < havuzBoyutu; i++)
        {
            GameObject yeniBoru = Instantiate(boruPrefab);
            yeniBoru.SetActive(false); // Başlangıçta kapalı olsun
            boruHavuzu.Add(yeniBoru);
        }

        // Belirli aralıklarla boru ekleme işlemini başlat
        InvokeRepeating(nameof(BoruEkle), 0f, boruOlusturmaAraligi);
    }

    void BoruEkle()
    {
        // Havuzdan bir boru seç
        GameObject boru = boruHavuzu[mevcutBoruIndex];

        // Boruyu etkinleştir ve pozisyonunu güncelle
        boru.SetActive(true);
        float rastgeleY = Random.Range(-2.5f, -1.3f);
        boru.transform.position = new Vector3(spawnX, rastgeleY, 0f);

        // Sıradaki boruyu seç
        mevcutBoruIndex = (mevcutBoruIndex + 1) % havuzBoyutu;
    }
}
