using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxCan = 3;
    private int mevcutCan;

    public event System.Action<int> OnCanDegisti;

    void Start()
    {
        mevcutCan = maxCan;
        GameManager.Instance.OnGameStart += CanSifirla;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStart -= CanSifirla;
    }

    void CanSifirla()
    {
        mevcutCan = maxCan;
        OnCanDegisti?.Invoke(mevcutCan);
    }

    public void HasarAl(int miktar = 1)
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        mevcutCan -= miktar;
        OnCanDegisti?.Invoke(mevcutCan);
        ScoreManager.Instance.CarpanSifirla();
        AudioManager.Instance.HasarAlmaSesCal();

        if (UIManager.Instance != null)
            StartCoroutine(EkranKizar());

        if (mevcutCan <= 0)
            GameManager.Instance.TriggerGameOver();
    }

    public void CanEkle(int miktar = 1)
    {
        mevcutCan = Mathf.Min(mevcutCan + miktar, maxCan);
        OnCanDegisti?.Invoke(mevcutCan);
    }

    System.Collections.IEnumerator EkranKizar()
    {
        UIManager.Instance.EkranKizarmaGoster();
        yield return new WaitForSecondsRealtime(0.3f);
        UIManager.Instance.EkranKizarmaGizle();
    }
}