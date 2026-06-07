using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Can ve Hasar")]
    public int can = 2;
    public int oyuncuyaVerdigiHasar = 1;

    [Header("Hareket")]
    public float hiz = 3f;
    public float maxHiz = 8f;

    [Header("Zorluk")]
    public float hizArtisi = 0.2f;
    public int dalgaBasiDusmanSayisi = 3;
}