using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShoesManager : MonoBehaviour, IDataPersistence
{
    //public GameObject player;
    public SkinnedMeshRenderer smr;
    public List<Material> shoes = new();
    public List<Button> buttons = new();
    public static int selectedShoes = 0;
    public int rekomendasi;
    public int rekomendasiAHP;
    public GameObject panelRekomendasiVIKOR;
    public Animator panelRekVIKOR;
    public GameObject panelRekomendasiAHP;
    public Animator panelRekAHP;
    public List<GameObject> jempol = new();

    public void LoadData(GameData data)
    {
        rekomendasi = data.ranking;
        rekomendasiAHP = data.rankingAHP;
    }

    public void SaveData(ref GameData data)
    {
        
    }

    private void Start()
    {
        for (int i = 0; i < jempol.Count; i++)
        {
            jempol[i].SetActive(false);
        }
        panelRekomendasiVIKOR.SetActive(false);
        panelRekomendasiAHP.SetActive(false);
    }

    private void Update()
    {
        buttons[selectedShoes].interactable = false;
    }

    public void TombolRekomendasi()
    {
            panelRekomendasiVIKOR.SetActive(true);
            jempol[rekomendasi].SetActive(true);
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].interactable = true;
            }
            selectedShoes = rekomendasi;
            smr.material = shoes[rekomendasi];

        // if (!DataPersistenceManager.instance.HasGameData())
        // {
        //     panelRekomendasi.SetActive(true);
        // }
        // else
        // {
        //     panelRekomendasi2.SetActive(true);
        //     jempol[rekomendasi].SetActive(true);
        //     for (int i = 0; i < buttons.Count; i++)
        //     {
        //         buttons[i].interactable = true;
        //     }
        //     selectedShoes = rekomendasi;
        //     smr.material = shoes[rekomendasi];
        // }
    }

    public void TombolRekomendasiAHP()
    {
        panelRekomendasiAHP.SetActive(true);
        jempol[rekomendasiAHP].SetActive(true);
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        selectedShoes = rekomendasiAHP;
        smr.material = shoes[rekomendasiAHP];
    }

    public void TutupPanelVIKOR()
    {
        StartCoroutine(AnimTutupPanelVIKOR());
    }
    IEnumerator AnimTutupPanelVIKOR()
    {
        panelRekVIKOR.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        panelRekomendasiVIKOR.SetActive(false);
    }
    
    public void TutupPanelAHP()
    {
        StartCoroutine(AnimTutupPanelAHP());
    }
    IEnumerator AnimTutupPanelAHP()
    {
        panelRekAHP.SetTrigger("close");
        yield return new WaitForSeconds(0.5f);
        panelRekomendasiAHP.SetActive(false);
    }
    public void Shoes1()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[0];
        
        for(int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[0].interactable = false;
        selectedShoes = 0;
    }

    public void Shoes2()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[1];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[1].interactable = false;
        selectedShoes = 1;
    }

    public void Shoes3()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[2];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[2].interactable = false;
        selectedShoes = 2;
    }

    public void Shoes4()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[3];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[3].interactable = false;
        selectedShoes = 3;
    }

    public void Shoes5()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[4];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[4].interactable = false;
        selectedShoes = 4;
    }

    public void Shoes6()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[5];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[5].interactable = false;
        selectedShoes = 5;
    }

    public void Shoes7()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[6];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[6].interactable = false;
        selectedShoes = 6;
    }

    public void Shoes8()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[7];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[7].interactable = false;
        selectedShoes = 7;
    }

    public void Shoes9()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[8];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[8].interactable = false;
        selectedShoes = 8;
    }

    public void Shoes10()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[9];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[9].interactable = false;
        selectedShoes = 9;
    }

    public void Shoes11()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[10];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[10].interactable = false;
        selectedShoes = 10;
    }

    public void Shoes12()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[11];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[11].interactable = false;
        selectedShoes = 11;
    }

    public void Shoes13()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[12];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[12].interactable = false;
        selectedShoes = 12;
    }

    public void Shoes14()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[13];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[13].interactable = false;
        selectedShoes = 13;
    }

    public void Shoes15()
    {
        AudioManager.instance.PlaySFX("button");
        smr.material = shoes[14];

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].interactable = true;
        }
        //buttons[14].interactable = false;
        selectedShoes = 14;
    }

}
