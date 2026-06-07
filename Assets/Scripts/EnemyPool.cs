using UnityEngine;
using System.Collections.Generic;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;

    public GameObject normalPrefab;
    public GameObject hizliPrefab;
    public GameObject tankerPrefab;
    public int havuzBoyutu = 10;

    private List<GameObject> havuz = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        HavuzuDoldur(normalPrefab);
        HavuzuDoldur(hizliPrefab);
        HavuzuDoldur(tankerPrefab);
    }

    void HavuzuDoldur(GameObject prefab)
    {
        for (int i = 0; i < havuzBoyutu; i++)
        {
            GameObject d = Instantiate(prefab);
            d.SetActive(false);
            havuz.Add(d);
        }
    }

    public void DusmanYerlestir(Vector3 konum, EnemyData veri)
    {
        GameObject hedefPrefab = normalPrefab;

        if (veri.hiz >= 5f && veri.can == 1)
            hedefPrefab = hizliPrefab;
        else if (veri.can >= 5)
            hedefPrefab = tankerPrefab;

        foreach (GameObject d in havuz)
        {
            if (!d.activeInHierarchy && d.GetComponent<Enemy>().veri == veri)
            {
                d.transform.position = konum;
                d.SetActive(true);
                return;
            }
        }

        GameObject yeni = Instantiate(hedefPrefab, konum, Quaternion.identity);
        yeni.GetComponent<Enemy>().veri = veri;
        havuz.Add(yeni);
    }

    public void DusmanGeriAl(GameObject d)
    {
        d.SetActive(false);
    }
}