using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Shoes", order = 1)]
public class ShoesStat : ScriptableObject
{
    public string shoesName;
    public int jKoin;
    public float jPowerup;
    public float jLompat;
    public float jMenunduk;
    public float jBerpindah;
    public float jJarak;
    public float jSkor;
    public float jKecepatan;
}
