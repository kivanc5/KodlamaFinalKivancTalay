using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponData silahVeri;

    private float yolAlinanMesafe = 0f;

    void OnEnable()
    {
        yolAlinanMesafe = 0f;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        float adim = silahVeri.mermiHizi * Time.deltaTime;
        transform.Translate(Vector3.forward * adim);
        yolAlinanMesafe += adim;

        if (yolAlinanMesafe >= silahVeri.maxMenzil)
            BulletPool.Instance.MermiGeriAl(gameObject);
    }

    void OnTriggerEnter(Collider diger)
    {
        if (diger.CompareTag("Enemy"))
        {
            int hasar = silahVeri.mermiHasari * ScoreManager.Instance.HasarCarpan;
            diger.GetComponent<Enemy>().HasarAl(hasar);
            BulletPool.Instance.MermiGeriAl(gameObject);
        }
    }
}