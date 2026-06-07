using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    private const string HIGH_SCORE_KEY = "HighScore";

    public int CurrentScore  { get; private set; }
    public int HighScore     { get; private set; }
    public int Carpan        { get; private set; } = 1;
    public int HasarCarpan   { get; private set; } = 1;

    public event System.Action<int> OnScoreChanged;
    public event System.Action<int> OnHighScoreChanged;
    public event System.Action<int> OnCarpanChanged;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        HighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    void Start()
    {
        GameManager.Instance.OnGameStart += SkorSifirla;
        GameManager.Instance.OnGameOver  += HighScoreKaydet;
    }

    void OnDisable()
    {
        GameManager.Instance.OnGameStart -= SkorSifirla;
        GameManager.Instance.OnGameOver  -= HighScoreKaydet;
    }

    void SkorSifirla()
    {
        CurrentScore = 0;
        Carpan = 1;
        HasarCarpan = 1;
        OnScoreChanged?.Invoke(CurrentScore);
        OnCarpanChanged?.Invoke(Carpan);
    }

    public void SkorEkle(int miktar = 1)
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;
        CurrentScore += miktar * Carpan;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void CarpanArttir()
    {
        Carpan = Mathf.Min(Carpan + 1, 5);
        OnCarpanChanged?.Invoke(Carpan);
    }

    public void CarpanSifirla()
    {
        Carpan = 1;
        OnCarpanChanged?.Invoke(Carpan);
    }

    public void HasarCarpanAktif()
    {
        HasarCarpan = 2;
        StartCoroutine(HasarCarpanSifirla());
    }

    System.Collections.IEnumerator HasarCarpanSifirla()
    {
        yield return new WaitForSeconds(5f);
        HasarCarpan = 1;
    }

    void HighScoreKaydet()
    {
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, HighScore);
            PlayerPrefs.Save();
            OnHighScoreChanged?.Invoke(HighScore);
        }
    }
}