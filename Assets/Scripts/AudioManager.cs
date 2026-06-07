using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Ses Dosyaları")]
    public AudioClip atesSesi;
    public AudioClip dusmanOlumSesi;
    public AudioClip hasarAlmaSesi;
    public AudioClip powerUpSesi;
    public AudioClip dalgaBaslangicSesi;
    public AudioClip dusmanYaklasaSesi;

    [Header("Ses Seviyeleri (0-1)")]
    [Range(0f, 1f)] public float atesVolume = 0.5f;
    [Range(0f, 1f)] public float dusmanOlumVolume = 0.5f;
    [Range(0f, 1f)] public float hasarAlmaVolume = 0.5f;
    [Range(0f, 1f)] public float powerUpVolume = 0.5f;
    [Range(0f, 1f)] public float dalgaVolume = 0.5f;
    [Range(0f, 1f)] public float dusmanYaklasmVolume = 0.3f;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void AtesSesCal()
    {
        audioSource.PlayOneShot(atesSesi, atesVolume);
    }

    public void DusmanOlumSesCal()
    {
        audioSource.PlayOneShot(dusmanOlumSesi, dusmanOlumVolume);
    }

    public void HasarAlmaSesCal()
    {
        audioSource.PlayOneShot(hasarAlmaSesi, hasarAlmaVolume);
    }

    public void PowerUpSesCal()
    {
        audioSource.PlayOneShot(powerUpSesi, powerUpVolume);
    }

    public void DalgaSesCal()
    {
        audioSource.PlayOneShot(dalgaBaslangicSesi, dalgaVolume);
    }

    public void DusmanYaklasmaCal(Vector3 dusmanKonum)
    {
        AudioSource.PlayClipAtPoint(dusmanYaklasaSesi, dusmanKonum, dusmanYaklasmVolume);
    }
}