using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData veri;

    private int mevcutCan;
    private NavMeshAgent agent;
    private Transform oyuncu;
    private float hasarBekleme = 0f;
    private float sesBekleme = 0f;
    private EnemyHealthBar canBar;

    void OnEnable()
    {
        mevcutCan = veri.can;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = veri.hiz;
        oyuncu = GameObject.FindWithTag("Player").transform;
        hasarBekleme = 0f;
        sesBekleme = 0f;
        canBar = GetComponentInChildren<EnemyHealthBar>(true);

        if (canBar != null)
            canBar.CanGuncelle(mevcutCan, veri.can);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        if (oyuncu == null) return;

        agent.SetDestination(oyuncu.position);

        float mesafe = Vector3.Distance(transform.position, oyuncu.position);

        // Hasar ver
        if (mesafe < 1.2f)
        {
            hasarBekleme -= Time.deltaTime;
            if (hasarBekleme <= 0f)
            {
                oyuncu.GetComponent<PlayerHealth>().HasarAl(veri.oyuncuyaVerdigiHasar);
                hasarBekleme = 1f;
            }
        }

        // Hızlı düşman için daha erken ses
        float sesMesafesi = (veri.hiz >= 5f && veri.can == 1) ? 10f : 5f;

        if (mesafe < sesMesafesi)
        {
            sesBekleme -= Time.deltaTime;
            if (sesBekleme <= 0f)
            {
                AudioManager.Instance.DusmanYaklasmaCal(transform.position);
                sesBekleme = 3f;
            }
        }
    }

    public void HasarAl(int miktar)
    {
        mevcutCan -= miktar;

        if (canBar != null)
            canBar.CanGuncelle(mevcutCan, veri.can);

        ScoreManager.Instance.CarpanArttir();

        if (mevcutCan <= 0)
        {
            ScoreManager.Instance.SkorEkle(1);
            PowerUpDusur();
            AudioManager.Instance.DusmanOlumSesCal();
            EnemyPool.Instance.DusmanGeriAl(gameObject);
        }
    }

    void PowerUpDusur()
    {
        float sans = Random.value;
        if (sans < 0.03f)
            PowerUpPool.Instance.PowerUpDusur(transform.position, PowerUp.Tip.CanYenileme);
        else if (sans < 0.06f)
            PowerUpPool.Instance.PowerUpDusur(transform.position, PowerUp.Tip.HizArtisi);
        else if (sans < 0.10f)
            PowerUpPool.Instance.PowerUpDusur(transform.position, PowerUp.Tip.CiftHasar);
    }
}