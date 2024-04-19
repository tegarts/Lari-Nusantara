using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundUIManager : MonoBehaviour, IDataPersistence
{
    public Slider _musicSlider, _sfxSlider;

    public float musicValue, sfxValue;
    public void LoadData(GameData data)
    {
        musicValue = data.musicValue;
        sfxValue = data.sfxValue;
    }

    public void SaveData(ref GameData data)
    {
        data.musicValue = musicValue;
        data.sfxValue = sfxValue;
    }

    private void Start()
    {
        if (!DataPersistenceManager.instance.HasGameData())
        {
            _musicSlider.value = 1;
            _sfxSlider.value = 1;
        }
        else
        {
            _musicSlider.value = musicValue;
            _sfxSlider.value = sfxValue;
        }
    }

    private void Update()
    {
        musicValue = _musicSlider.value;
        sfxValue = _sfxSlider.value;
    }

    public void MusicVolume()
    {
        AudioManager.instance.MusicVolume(_musicSlider.value);
    }

    public void SFXVolume()
    {
        AudioManager.instance.SFXVolume(_sfxSlider.value);
    }
}
