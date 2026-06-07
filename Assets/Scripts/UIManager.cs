using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Ekranlar")]
    public GameObject menuEkrani;
    public GameObject tutorialEkrani;
    public GameObject oyunEkrani;
    public GameObject gameOverEkrani;
    public GameObject pauseEkrani;

    [Header("Oyun İçi")]
    public TextMeshProUGUI skorYazisi;
    public TextMeshProUGUI carpanYazisi;
    public TextMeshProUGUI dalgaYazisi;
    public Image ekranKizarma;

    [Header("Can Göstergesi")]
    public Image kalp1;
    public Image kalp2;
    public Image kalp3;

    [Header("Power-up Göstergesi")]
    public Transform powerUpListesi;
    public GameObject powerUpYaziPrefab;

    [Header("Game Over")]
    public TextMeshProUGUI sonSkorYazisi;
    public TextMeshProUGUI highScoreYazisi;

    void Awake() => Instance = this;

    void Start()
    {
        GameManager.Instance.OnGameStart  += OyunBasladi;
        GameManager.Instance.OnGameOver   += OyunBitti;
        GameManager.Instance.OnGamePause  += OyunDurdu;
        GameManager.Instance.OnGameResume += OyunDevamEtti;
        ScoreManager.Instance.OnScoreChanged  += SkoruGuncelle;
        ScoreManager.Instance.OnCarpanChanged += CarpanGuncelle;

        PlayerHealth ph = FindObjectOfType<PlayerHealth>();
        if (ph != null)
            ph.OnCanDegisti += CanlariGuncelle;

        menuEkrani.SetActive(true);
        tutorialEkrani.SetActive(false);
        oyunEkrani.SetActive(false);
        gameOverEkrani.SetActive(false);
        pauseEkrani.SetActive(false);
        ekranKizarma.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStart  -= OyunBasladi;
        GameManager.Instance.OnGameOver   -= OyunBitti;
        GameManager.Instance.OnGamePause  -= OyunDurdu;
        GameManager.Instance.OnGameResume -= OyunDevamEtti;
        ScoreManager.Instance.OnScoreChanged  -= SkoruGuncelle;
        ScoreManager.Instance.OnCarpanChanged -= CarpanGuncelle;
    }

    public void BaslaButonu()
    {
        menuEkrani.SetActive(false);
        GameManager.Instance.StartGame();
    }

    public void TutorialButonu()
    {
        menuEkrani.SetActive(false);
        tutorialEkrani.SetActive(true);
    }

    public void AnlasildiButonu()
    {
        tutorialEkrani.SetActive(false);
        menuEkrani.SetActive(true);
    }

    public void DevamButonu()  => GameManager.Instance.ResumeGame();
    public void TekrarButonu() => GameManager.Instance.RestartGame();

    void OyunBasladi()
    {
        menuEkrani.SetActive(false);
        tutorialEkrani.SetActive(false);
        oyunEkrani.SetActive(true);
        gameOverEkrani.SetActive(false);
        pauseEkrani.SetActive(false);
        CanlariGuncelle(3);
    }

    void OyunBitti()
    {
        oyunEkrani.SetActive(false);
        gameOverEkrani.SetActive(true);
        sonSkorYazisi.text   = "Skor: " + ScoreManager.Instance.CurrentScore;
        highScoreYazisi.text = "En Yüksek: " + ScoreManager.Instance.HighScore;
    }

    void OyunDurdu()     => pauseEkrani.SetActive(true);
    void OyunDevamEtti() => pauseEkrani.SetActive(false);

    void SkoruGuncelle(int skor)    => skorYazisi.text   = "Skor: " + skor;
    void CarpanGuncelle(int carpan) => carpanYazisi.text = "x" + carpan;

    void CanlariGuncelle(int can)
    {
        kalp1.gameObject.SetActive(can >= 1);
        kalp2.gameObject.SetActive(can >= 2);
        kalp3.gameObject.SetActive(can >= 3);
    }

    public void PowerUpGoster(string mesaj, float sure)
    {
        StartCoroutine(PowerUpYaziCoroutine(mesaj, sure));
    }

    IEnumerator PowerUpYaziCoroutine(string mesaj, float sure)
    {
        GameObject yazi = Instantiate(powerUpYaziPrefab, powerUpListesi);
        TextMeshProUGUI tmp = yazi.GetComponent<TextMeshProUGUI>();

        if (sure <= 0f)
        {
            tmp.text = mesaj;
            yield return new WaitForSeconds(2f);
        }
        else
        {
            float kalanSure = sure;
            while (kalanSure > 0f)
            {
                tmp.text = mesaj + " : " + Mathf.CeilToInt(kalanSure) + "s";
                yield return new WaitForSeconds(1f);
                kalanSure -= 1f;
            }
        }

        Destroy(yazi);
    }

    public void DalgaYazisiGoster(int dalga)
    {
        StartCoroutine(DalgaCoroutine(dalga));
    }

    IEnumerator DalgaCoroutine(int dalga)
    {
        dalgaYazisi.text = "DALGA " + dalga;
        dalgaYazisi.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        dalgaYazisi.gameObject.SetActive(false);
    }

    public void EkranKizarmaGoster() => ekranKizarma.gameObject.SetActive(true);
    public void EkranKizarmaGizle()  => ekranKizarma.gameObject.SetActive(false);
}