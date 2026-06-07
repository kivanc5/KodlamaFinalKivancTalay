using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData normalDusman;
    public EnemyData hizliDusman;
    public EnemyData tankerDusman;

    public Transform[] spawnNoktalari;

    private int mevcutDalga = 0;
    private bool oyunBasladi = false;

    void Start()
    {
        GameManager.Instance.OnGameStart += SpawnBaslat;
        GameManager.Instance.OnGameOver  += SpawnDurdur;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStart -= SpawnBaslat;
        GameManager.Instance.OnGameOver  -= SpawnDurdur;
    }

    void SpawnBaslat()
    {
        oyunBasladi = true;
        mevcutDalga = 0;
        StartCoroutine(DalgaDongusu());
    }

    void SpawnDurdur()
    {
        oyunBasladi = false;
        StopAllCoroutines();
    }

    IEnumerator DalgaDongusu()
    {
        while (oyunBasladi)
        {
            mevcutDalga++;
            UIManager.Instance.DalgaYazisiGoster(mevcutDalga);
            AudioManager.Instance.DalgaSesCal();

            int dusmanSayisi = normalDusman.dalgaBasiDusmanSayisi + (mevcutDalga - 1) * 2;

            for (int i = 0; i < dusmanSayisi; i++)
            {
                SpawnDusman();
                yield return new WaitForSeconds(1.5f);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    void SpawnDusman()
    {
        int nokta = Random.Range(0, spawnNoktalari.Length);
        EnemyData secilenVeri = SecilenDusmanTipi();
        EnemyPool.Instance.DusmanYerlestir(spawnNoktalari[nokta].position, secilenVeri);
    }

    EnemyData SecilenDusmanTipi()
    {
        if (mevcutDalga >= 3)
        {
            float sans = Random.value;
            if (sans < 0.2f) return tankerDusman;
            if (sans < 0.5f) return hizliDusman;
        }
        else if (mevcutDalga >= 2)
        {
            if (Random.value < 0.4f) return hizliDusman;
        }
        return normalDusman;
    }
}