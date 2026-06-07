using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum Tip { CanYenileme, HizArtisi, CiftHasar }
    public Tip tip;
    public float yasomSuresi = 8f;

    private float gecenSure = 0f;

    void OnEnable()
    {
        gecenSure = 0f;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        gecenSure += Time.deltaTime;
        if (gecenSure >= yasomSuresi)
            PowerUpPool.Instance.GeriAl(gameObject);
    }

    void OnTriggerEnter(Collider diger)
    {
        if (!diger.CompareTag("Player")) return;

        AudioManager.Instance.PowerUpSesCal();

        switch (tip)
        {
            case Tip.CanYenileme:
                diger.GetComponent<PlayerHealth>().CanEkle(1);
                UIManager.Instance.PowerUpGoster("+ 1 Can alındı", 0f);
                break;
            case Tip.HizArtisi:
                diger.GetComponent<PlayerController>().HizArttir(5f);
                UIManager.Instance.PowerUpGoster("Hız power-up alındı", 5f);
                break;
            case Tip.CiftHasar:
                ScoreManager.Instance.HasarCarpanAktif();
                UIManager.Instance.PowerUpGoster("2x Hasar power-up alındı", 5f);
                break;
        }

        PowerUpPool.Instance.GeriAl(gameObject);
    }
}