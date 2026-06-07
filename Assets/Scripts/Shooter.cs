using UnityEngine;

public class Shooter : MonoBehaviour
{
    public WeaponData silahVeri;
    public Transform atisNoktasi;

    private float sonAtisZamani = 0f;
    private bool oyunBasladi = false;

    void Start()
    {
        GameManager.Instance.OnGameStart  += OyunBasladi;
        GameManager.Instance.OnGameOver   += OyunBitti;
        GameManager.Instance.OnGamePause  += OyunDurdu;
        GameManager.Instance.OnGameResume += OyunDevamEtti;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStart  -= OyunBasladi;
        GameManager.Instance.OnGameOver   -= OyunBitti;
        GameManager.Instance.OnGamePause  -= OyunDurdu;
        GameManager.Instance.OnGameResume -= OyunDevamEtti;
    }

    void Update()
    {
        if (!oyunBasladi) return;

        if (Input.GetMouseButtonDown(0))
            AtesEt();

        if (Input.GetMouseButton(1))
            if (Time.time - sonAtisZamani >= silahVeri.atisAraligi)
                AtesEt();
    }

    void AtesEt()
    {
        sonAtisZamani = Time.time;
        BulletPool.Instance.MermiFirlat(atisNoktasi.position, atisNoktasi.rotation);
        AudioManager.Instance.AtesSesCal();
    }

    void OyunBasladi()   => oyunBasladi = true;
    void OyunBitti()     => oyunBasladi = false;
    void OyunDurdu()     => oyunBasladi = false;
    void OyunDevamEtti() => oyunBasladi = true;
}