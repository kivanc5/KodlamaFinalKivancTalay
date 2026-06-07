using UnityEngine;
using System.Collections.Generic;

public class PowerUpPool : MonoBehaviour
{
    public static PowerUpPool Instance;

    public GameObject canPrefab;
    public GameObject hizPrefab;
    public GameObject hasarPrefab;
    public int havuzBoyutu = 5;

    private List<GameObject> havuz = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        HavuzuDoldur(canPrefab);
        HavuzuDoldur(hizPrefab);
        HavuzuDoldur(hasarPrefab);
    }

    void HavuzuDoldur(GameObject prefab)
    {
        for (int i = 0; i < havuzBoyutu; i++)
        {
            GameObject p = Instantiate(prefab);
            p.SetActive(false);
            havuz.Add(p);
        }
    }

    public void PowerUpDusur(Vector3 konum, PowerUp.Tip tip)
    {
        foreach (GameObject p in havuz)
        {
            if (!p.activeInHierarchy && p.GetComponent<PowerUp>().tip == tip)
            {
                p.transform.position = konum;
                p.SetActive(true);
                return;
            }
        }
        GameObject hedefPrefab = tip == PowerUp.Tip.CanYenileme ? canPrefab :
            tip == PowerUp.Tip.HizArtisi   ? hizPrefab : hasarPrefab;
        GameObject yeni = Instantiate(hedefPrefab, konum, Quaternion.identity);
        havuz.Add(yeni);
    }

    public void GeriAl(GameObject p)
    {
        p.SetActive(false);
    }
}