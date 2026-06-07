using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private Slider slider;
    private Transform kamera;

    void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        kamera = Camera.main.transform;
    }

    void Update()
    {
        if (kamera != null)
            transform.LookAt(transform.position + kamera.forward);
    }

    public void CanGuncelle(int mevcutCan, int maxCan)
    {
        if (slider == null)
            slider = GetComponentInChildren<Slider>();

        if (slider != null)
            slider.value = (float)mevcutCan / maxCan;
    }
}