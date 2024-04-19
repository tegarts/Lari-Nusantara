using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour, IDataPersistence
{
    [SerializeField] private GameObject child;

    public Material silverMaterial;
    public List<Material> shoes = new();

    public int jumlahPowerup;

    public TMP_Text multiText;
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;

    public float duration = 10f;
    public UIManager uiManager;
    public void LoadData(GameData data)
    {

    }

    public void SaveData(ref GameData data)
    {
        //data.jumlahPowerup = jumlahPowerup;
    }
    private void Start()
    {
        multiText.text = "x1";
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Multiply"))
        {
            AudioManager.instance.PlaySFX("multi");
            StartCoroutine(MultiplyStart());
            jumlahPowerup += 1;
        }
        else if (other.CompareTag("Invincible"))
        {
            AudioManager.instance.PlaySFX("multi");
            StartCoroutine(InvincibleStart());
            jumlahPowerup += 1;
        }
    }

    IEnumerator MultiplyStart()
    {
        PlayerManager.scoreMulti = 2;
        Debug.Log("proses x2: " + PlayerManager.scoreMulti);
        multiText.text = "x2";
        yield return new WaitForSeconds(duration + uiManager.statChoose.jPowerup);
        PlayerManager.scoreMulti = 1;
        Debug.Log("Normal lagi: " + PlayerManager.scoreMulti);
        multiText.text = "x1";
    }

    IEnumerator InvincibleStart()
    {
        PlayerController.isInvincible = true;
        child.GetComponent<SkinnedMeshRenderer>().material = silverMaterial;
        yield return new WaitForSeconds(8f + uiManager.statChoose.jPowerup);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            child.GetComponent<SkinnedMeshRenderer>().material = shoes[ShoesManager.selectedShoes];
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            child.GetComponent<SkinnedMeshRenderer>().material = silverMaterial;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        child.GetComponent<SkinnedMeshRenderer>().material = shoes[ShoesManager.selectedShoes];
        PlayerController.isInvincible = false;
        

    }

    

}
