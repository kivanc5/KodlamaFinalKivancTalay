using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Game/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("Mermi")]
    public float mermiHizi = 20f;
    public int mermiHasari = 1;
    public float atisAraligi = 0.3f;

    [Header("Menzil")]
    public float maxMenzil = 50f;
}