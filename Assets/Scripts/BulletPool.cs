using UnityEngine;
using System.Collections.Generic;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public GameObject mermiPrefab;
    public int havuzBoyutu = 15;

    private List<GameObject> havuz = new List<GameObject>();

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < havuzBoyutu; i++)
        {
            GameObject m = Instantiate(mermiPrefab);
            m.SetActive(false);
            havuz.Add(m);
        }
    }

    public void MermiFirlat(Vector3 konum, Quaternion rotasyon)
    {
        foreach (GameObject m in havuz)
        {
            if (!m.activeInHierarchy)
            {
                m.transform.position = konum;
                m.transform.rotation = rotasyon;
                m.SetActive(true);
                return;
            }
        }
        GameObject yeni = Instantiate(mermiPrefab, konum, rotasyon);
        havuz.Add(yeni);
    }

    public void MermiGeriAl(GameObject m)
    {
        m.SetActive(false);
    }
}