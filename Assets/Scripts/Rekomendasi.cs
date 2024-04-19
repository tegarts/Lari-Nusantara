using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rekomendasi : MonoBehaviour, IDataPersistence
{
    public int rankSepatu;
    public void LoadData(GameData data)
    {
        rankSepatu = data.ranking;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    private void Update()
    {
        Debug.Log(rankSepatu);
    }
}
