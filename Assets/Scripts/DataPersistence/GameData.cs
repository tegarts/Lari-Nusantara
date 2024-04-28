using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int totalCoins;
    public float highScore;
    public float longestDistance;
    public int ranking;
    public int rankingAHP;
    public bool isRetry;
    public float musicValue, sfxValue;
    public GameData()
    {
        this.totalCoins = 0;
        this.highScore = 0;
        this.longestDistance = 0;
        this.ranking = 0;
        this.rankingAHP = 0;

        this.isRetry = false;

        this.musicValue = 0;
        this.sfxValue = 0;
    }
}
