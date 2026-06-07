using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket")]
    public float yuruyusHizi = 5f;
    public float sprintHizi = 8f;

    [Header("Kamera")]
    public Transform kamera;
    public float fareHassasiyeti = 2f;
    public float maxAci = 80f;

    private CharacterController cc;
    private float kamaraAcisi = 0f;
    private float geciciHiz = 0f;
    private float hizSuresi = 0f;
    private bool oyunBasladi = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameManager.Instance.OnGameStart += OyunBasladi;
        GameManager.Instance.OnGameOver  += OyunBitti;
        GameManager.Instance.OnGamePause  += OyunDurdu;
        GameManager.Instance.OnGameResume += OyunDevamEtti;
    }

    void Update()
    {
        if (!oyunBasladi) return;

        Hareket();
        KameraKontrol();
        HizSayaci();

        if (Input.GetKeyDown(KeyCode.Escape))
            GameManager.Instance.PauseGame();
    }

    void Hareket()
    {
        float aktifHiz = geciciHiz > 0 ? geciciHiz :
                         Input.GetKey(KeyCode.LeftShift) ? sprintHizi : yuruyusHizi;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 hareket = transform.right * x + transform.forward * z;
        cc.Move(hareket * aktifHiz * Time.deltaTime);
        cc.Move(Vector3.down * 9.81f * Time.deltaTime);
    }

    void KameraKontrol()
    {
        float fareX = Input.GetAxis("Mouse X") * fareHassasiyeti;
        float fareY = Input.GetAxis("Mouse Y") * fareHassasiyeti;

        kamaraAcisi -= fareY;
        kamaraAcisi = Mathf.Clamp(kamaraAcisi, -maxAci, maxAci);

        kamera.localRotation = Quaternion.Euler(kamaraAcisi, 0f, 0f);
        transform.Rotate(Vector3.up * fareX);
    }

    void HizSayaci()
    {
        if (hizSuresi > 0)
        {
            hizSuresi -= Time.deltaTime;
            if (hizSuresi <= 0) geciciHiz = 0f;
        }
    }

    public void HizArttir(float ekstraHiz)
    {
        geciciHiz = yuruyusHizi + ekstraHiz;
        hizSuresi = 5f;
    }

    void OyunBasladi()
    {
        oyunBasladi = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OyunBitti()
    {
        oyunBasladi = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OyunDurdu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OyunDevamEtti()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDestroy()
    {
        GameManager.Instance.OnGameStart  -= OyunBasladi;
        GameManager.Instance.OnGameOver   -= OyunBitti;
        GameManager.Instance.OnGamePause  -= OyunDurdu;
        GameManager.Instance.OnGameResume -= OyunDevamEtti;
    }
}