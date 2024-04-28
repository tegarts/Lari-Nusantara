using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class Bobot : MonoBehaviour, IDataPersistence
{
    public PlayerController playerController;
    public PlayerPowerup powerup;

    public float _jumlahKoin, _jumlahSkor, _jarakTempuh, _jumlahPowerup, _jumlahLompat, _jumlahSlide, _jumlahBerpindah, _kecepatan;
    
    public float[] listKoin = new float[8];
    public float[] listSkor = new float[8];
    public float[] listJarak = new float[8];
    public float[] listPowerup = new float[8];
    public float[] listLompat = new float[8];
    public float[] listSlide = new float[8];
    public float[] listBerpindah = new float[8];
    public float[] listKecepatan = new float[8];

    public float[] hasilList = new float[8];
    public float[] hasilList2 = new float[8];

    public float[] hasilListKoin = new float[8];
    public float[] hasilListSkor = new float[8];
    public float[] hasilListJarak = new float[8];
    public float[] hasilListPowerup = new float[8];
    public float[] hasilListLompat = new float[8];
    public float[] hasilListSlide = new float[8];
    public float[] hasilListBerpindah = new float[8];
    public float[] hasilListKecepatan = new float[8];

    public float hasilSemua;

    public float bobotKoin, bobotSkor, bobotJarak, bobotPowerup, bobotLompat, bobotSlide, bobotBerpindah, bobotKecepatan;

    [Header("Nilai alternatif terhadap kriteria")]
    public float[] a1 = { 4, 2, 2, 4, 2, 3, 1, 1 };
    public float[] a2 = { 3, 3, 1, 2, 5, 3, 2, 2 };
    public float[] a3 = { 2, 2, 2, 1, 4, 4, 3, 1 };
    public float[] a4 = { 2, 1, 1, 3, 3, 5, 2, 2 };
    public float[] a5 = { 5, 1, 1, 3, 2, 3, 2, 3 };
    public float[] a6 = { 2, 2, 3, 5, 3, 2, 3, 1 };
    public float[] a7 = { 2, 2, 5, 3, 2, 2, 1, 3 };
    public float[] a8 = { 1, 2, 3, 4, 4, 2, 3, 2 };
    public float[] a9 = { 3, 3, 3, 2, 1, 2, 1, 5 };
    public float[] a10 = { 3, 2, 3, 1, 1, 4, 4, 2 };
    public float[] a11 = { 3, 1, 4, 2, 2, 3, 4, 2 };
    public float[] a12 = { 3, 2, 1, 2, 2, 2, 5, 1 };
    public float[] a13 = { 1, 4, 4, 2, 2, 3, 3, 2 };
    public float[] a14 = { 3, 4, 1, 2, 2, 2, 3, 4 };
    public float[] a15 = { 2, 5, 3, 2, 1, 2, 2, 2 };

    //[Header("Max Kriteria 1")]
    //public float[] gabunganK1 = new float[15];
    //public float maxK1;
    //public float minK1;
    

    public float[] normalisasiK1 = new float[15];
    public float[] normalisasiK2 = new float[15];
    public float[] normalisasiK3 = new float[15];
    public float[] normalisasiK4 = new float[15];
    public float[] normalisasiK5 = new float[15];
    public float[] normalisasiK6 = new float[15];
    public float[] normalisasiK7 = new float[15];
    public float[] normalisasiK8 = new float[15];

    public float[] normXbobot1 = new float[15];
    public float[] normXbobot2 = new float[15];
    public float[] normXbobot3 = new float[15];
    public float[] normXbobot4 = new float[15];
    public float[] normXbobot5 = new float[15];
    public float[] normXbobot6 = new float[15];
    public float[] normXbobot7 = new float[15];
    public float[] normXbobot8 = new float[15];

    public float[] nilaiS = new float[15];

    public float[] nilaiR = new float[15];

    public float minS;
    public float maxS;
    public float minR;
    public float maxR;

    public float[] nilaiQ = new float[15];

    public int[] ranking = new int[15];

    public int rankSepatu;

    [Header("AHP")]
    public float[] hasilAHP1 = new float[8];
    public float[] hasilAHP2 = new float[8];
    public float[] hasilAHP3 = new float[8];
    public float[] hasilAHP4 = new float[8];
    public float[] hasilAHP5 = new float[8];
    public float[] hasilAHP6 = new float[8];
    public float[] hasilAHP7 = new float[8];
    public float[] hasilAHP8 = new float[8];
    public float[] hasilAHP9 = new float[8];
    public float[] hasilAHP10 = new float[8];
    public float[] hasilAHP11 = new float[8];
    public float[] hasilAHP12 = new float[8];
    public float[] hasilAHP13 = new float[8];
    public float[] hasilAHP14 = new float[8];
    public float[] hasilAHP15 = new float[8];
    
    public float[] ahp = new float[15];

    public int[] rankingAHP = new int[15];
    public int rankSepatuAHP;
    public void LoadData(GameData data)
    {
        rankSepatu = data.ranking;
    }

    public void SaveData(ref GameData data)
    {
        data.ranking = rankSepatu;
    }

    private void Start()
    {

        playerController = FindAnyObjectByType<PlayerController>();
        powerup = FindAnyObjectByType<PlayerPowerup>();

        // Bagian Max 
        float[] gabunganK1 = { a1[0], a2[0], a3[0], a4[0], a5[0], a6[0], a7[0], a8[0], a9[0], a10[0], a11[0], a12[0], a13[0], a14[0], a15[0] };
        float maxK1 = Mathf.Max(gabunganK1);
        float minK1 = Mathf.Min(gabunganK1);

        float[] gabunganK2 = { a1[1], a2[1], a3[1], a4[1], a5[1], a6[1], a7[1], a8[1], a9[1], a10[1], a11[1], a12[1], a13[1], a14[1], a15[1] };
        float maxK2 = Mathf.Max(gabunganK2);
        float minK2 = Mathf.Min(gabunganK2);

        float[] gabunganK3 = { a1[2], a2[2], a3[2], a4[2], a5[2], a6[2], a7[2], a8[2], a9[2], a10[2], a11[2], a12[2], a13[2], a14[2], a15[2] };
        float maxK3 = Mathf.Max(gabunganK3);
        float minK3 = Mathf.Min(gabunganK3);

        float[] gabunganK4 = { a1[3], a2[3], a3[3], a4[3], a5[3], a6[3], a7[3], a8[3], a9[3], a10[3], a11[3], a12[3], a13[3], a14[3], a15[3] };
        float maxK4 = Mathf.Max(gabunganK4);
        float minK4 = Mathf.Min(gabunganK4);

        float[] gabunganK5 = { a1[4], a2[4], a3[4], a4[4], a5[4], a6[4], a7[4], a8[4], a9[4], a10[4], a11[4], a12[4], a13[4], a14[4], a15[4] };
        float maxK5 = Mathf.Max(gabunganK5);
        float minK5 = Mathf.Min(gabunganK5);

        float[] gabunganK6 = { a1[5], a2[5], a3[5], a4[5], a5[5], a6[5], a7[5], a8[5], a9[5], a10[5], a11[5], a12[5], a13[5], a14[5], a15[5] };
        float maxK6 = Mathf.Max(gabunganK6);
        float minK6 = Mathf.Min(gabunganK6);

        float[] gabunganK7 = { a1[6], a2[6], a3[6], a4[6], a5[6], a6[6], a7[6], a8[6], a9[6], a10[6], a11[6], a12[6], a13[6], a14[6], a15[6] };
        float maxK7 = Mathf.Max(gabunganK7);
        float minK7 = Mathf.Min(gabunganK7);

        float[] gabunganK8 = { a1[7], a2[7], a3[7], a4[7], a5[7], a6[7], a7[7], a8[7], a9[7], a10[7], a11[7], a12[7], a13[7], a14[7], a15[7] };
        float maxK8 = Mathf.Max(gabunganK8);
        float minK8 = Mathf.Min(gabunganK8);

        float[][] a = { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15 };

        for (int i = 0; i < normalisasiK1.Length; i++)
        {
            normalisasiK1[i] = (maxK1 - a[i][0]) / (maxK1 - minK1);
        }

        for (int i = 0; i < normalisasiK2.Length; i++)
        {
            normalisasiK2[i] = (maxK2 - a[i][1]) / (maxK2 - minK2);
        }

        for (int i = 0; i < normalisasiK3.Length; i++)
        {
            normalisasiK3[i] = (maxK3 - a[i][2]) / (maxK3 - minK3);
        }

        for (int i = 0; i < normalisasiK4.Length; i++)
        {
            normalisasiK4[i] = (maxK4 - a[i][3]) / (maxK4 - minK4);
        }

        for (int i = 0; i < normalisasiK5.Length; i++)
        {
            normalisasiK5[i] = (maxK5 - a[i][4]) / (maxK5 - minK5);
        }

        for (int i = 0; i < normalisasiK6.Length; i++)
        {
            normalisasiK6[i] = (maxK6 - a[i][5]) / (maxK6 - minK6);
        }

        for (int i = 0; i < normalisasiK7.Length; i++)
        {
            normalisasiK7[i] = (maxK7 - a[i][6]) / (maxK7 - minK7);
        }

        for (int i = 0; i < normalisasiK8.Length; i++)
        {
            normalisasiK8[i] = (maxK8 - a[i][7]) / (maxK8 - minK8);
        }
    }               

    private void Update()
    {
        if (PlayerManager.gameOver)
        {

            NormalisasiAwal();

            PerbandinganBobot();
            // TODO - bikin array aja buat gantiin hasillistkoin - hasillistkecepatan
            for (int i = 0; i < hasilList.Length; i++)
            {
                hasilList[i] = listKoin[i] + listSkor[i] + listJarak[i] + listPowerup[i] + listLompat[i] + listSlide[i] + listBerpindah[i] + listKecepatan[i];
            }

            //float[][] list8 = new float[][] { listKoin, listSkor, listJarak, listPowerup, listLompat, listSlide, listBerpindah, listKecepatan };

            for (int i = 0; i < hasilListKoin.Length; i++)
            {
                hasilListKoin[i] = listKoin[i] / hasilList[i];
                //Debug.Log("list: " + list8[i][0] + "hasil list: " + hasilList[i]);
            }

            for (int i = 0; i < hasilListSkor.Length; i++)
            {
                hasilListSkor[i] = listSkor[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListJarak.Length; i++)
            {
                hasilListJarak[i] = listJarak[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListPowerup.Length; i++)
            {
                hasilListPowerup[i] = listPowerup[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListLompat.Length; i++)
            {
                hasilListLompat[i] = listLompat[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListSlide.Length; i++)
            {
                hasilListSlide[i] = listSlide[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListBerpindah.Length; i++)
            {
                hasilListBerpindah[i] = listBerpindah[i] / hasilList[i];
            }

            for (int i = 0; i < hasilListKecepatan.Length; i++)
            {
                hasilListKecepatan[i] = listKecepatan[i] / hasilList[i];
            }

            bobotKoin = hasilListKoin.Sum() / 8;
            bobotSkor = hasilListSkor.Sum() / 8;
            bobotJarak = hasilListJarak.Sum() / 8;
            bobotPowerup = hasilListPowerup.Sum() / 8;
            bobotLompat = hasilListLompat.Sum() / 8;
            bobotSlide = hasilListSlide.Sum() / 8;
            bobotBerpindah = hasilListBerpindah.Sum() / 8;
            bobotKecepatan = hasilListKecepatan.Sum() / 8;

            //hasilListKoin = listKoin.Sum();
            //hasilListSkor = listSkor.Sum();
            //hasilListJarak = listJarak.Sum();
            //hasilListPowerup = listPowerup.Sum();
            //hasilListLompat = listLompat.Sum();
            //hasilListSlide = listSlide.Sum();
            //hasilListBerpindah = listBerpindah.Sum();
            //hasilListKecepatan = listKecepatan.Sum();

            //hasilSemua = hasilListKoin + hasilListSkor + hasilListJarak + hasilListPowerup + hasilListLompat + hasilListSlide + hasilListBerpindah + hasilListKecepatan;

            //bobotKoin = hasilListKoin / hasilSemua;
            //bobotSkor = hasilListSkor / hasilSemua;
            //bobotJarak = hasilListJarak / hasilSemua;
            //bobotPowerup = hasilListPowerup / hasilSemua;
            //bobotLompat = hasilListLompat / hasilSemua;
            //bobotSlide = hasilListSlide / hasilSemua;
            //bobotBerpindah = hasilListBerpindah / hasilSemua;
            //bobotKecepatan = hasilListKecepatan / hasilSemua;

            // S
            for (int i = 0; i < normalisasiK1.Length; i++)
            {
                normXbobot1[i] = bobotKoin * normalisasiK1[i];
            }

            for (int i = 0; i < normalisasiK2.Length; i++)
            {
                normXbobot2[i] = bobotSkor * normalisasiK2[i];
            }

            for (int i = 0; i < normalisasiK3.Length; i++)
            {
                normXbobot3[i] = bobotJarak * normalisasiK3[i];
            }

            for (int i = 0; i < normalisasiK4.Length; i++)
            {
                normXbobot4[i] = bobotPowerup * normalisasiK4[i];
            }

            for (int i = 0; i < normalisasiK5.Length; i++)
            {
                normXbobot5[i] = bobotLompat * normalisasiK5[i];
            }

            for (int i = 0; i < normalisasiK6.Length; i++)
            {
                normXbobot6[i] = bobotSlide * normalisasiK6[i];
            }

            for (int i = 0; i < normalisasiK7.Length; i++)
            {
                normXbobot7[i] = bobotBerpindah * normalisasiK7[i];
            }

            for (int i = 0; i < normalisasiK8.Length; i++)
            {
                normXbobot8[i] = bobotKecepatan * normalisasiK8[i];
            }

           

            for (int i = 0; i < 15; i++)
            {
                nilaiS[i] = normXbobot1[i] + normXbobot2[i] + normXbobot3[i] + normXbobot4[i] + normXbobot5[i] + normXbobot6[i] + normXbobot7[i] + normXbobot8[i];
            }

            for (int j = 0; j < 15; j++)
            {
                nilaiR[j] = Mathf.Max(normXbobot1[j], normXbobot2[j], normXbobot3[j], normXbobot4[j], normXbobot5[j], normXbobot6[j], normXbobot7[j], normXbobot8[j]);
            }

            minS = Mathf.Min(nilaiS);
            maxS = Mathf.Max(nilaiS);
            minR = Mathf.Min(nilaiR);
            maxR = Mathf.Max(nilaiR);

            for (int k = 0; k < 15; k++)
            {
                nilaiQ[k] = ((nilaiS[k] - minS) / (maxS - minS) * 0.5f) + ((nilaiR[k] - minR) / (maxR - minR) * 0.5f);
            }


            for (int l = 0; l < nilaiQ.Length; l++)
            {
                ranking[l] = l;
            }

            System.Array.Sort(ranking, (a, b) => nilaiQ[a].CompareTo(nilaiQ[b]));
            for (int i = 0; i < ranking.Length; i++)
            {
                //Debug.Log("Index: " + ranking[i] + ", Value: " + nilaiQ[ranking[i]]);
            }

            rankSepatu = ranking[0];

            HitungAHP();

        }
    }

    private void NormalisasiAwal()
    {
        if (PlayerManager.numberOfCoins > 200)
        {
            _jumlahKoin = 5;
        }
        else if (PlayerManager.numberOfCoins > 150)
        {
            _jumlahKoin = 4;
        }
        else if (PlayerManager.numberOfCoins > 100)
        {
            _jumlahKoin = 3;
        }
        else if (PlayerManager.numberOfCoins > 50)
        {
            _jumlahKoin = 2;
        }
        else if (PlayerManager.numberOfCoins <= 50)
        {
            _jumlahKoin = 1;
        }

        if (PlayerManager.score > 400)
        {
            _jumlahSkor = 5;
        }
        else if (PlayerManager.score > 300)
        {
            _jumlahSkor = 4;
        }
        else if (PlayerManager.score > 200)
        {
            _jumlahSkor = 3;
        }
        else if (PlayerManager.score > 100)
        {
            _jumlahSkor = 2;
        }
        else if (PlayerManager.score <= 100)
        {
            _jumlahSkor = 1;
        }

        if (PlayerManager.jarak > 200)
        {
            _jarakTempuh = 5;
        }
        else if (PlayerManager.jarak > 150)
        {
            _jarakTempuh = 4;
        }
        else if (PlayerManager.jarak > 100)
        {
            _jarakTempuh = 3;
        }
        else if (PlayerManager.jarak > 50)
        {
            _jarakTempuh = 2;
        }
        else if (PlayerManager.jarak <= 50)
        {
            _jarakTempuh = 1;
        }

        if (powerup.jumlahPowerup > 11)
        {
            _jumlahPowerup = 5;
        }
        else if (powerup.jumlahPowerup > 8)
        {
            _jumlahPowerup = 4;
        }
        else if (powerup.jumlahPowerup > 5)
        {
            _jumlahPowerup = 3;
        }
        else if (powerup.jumlahPowerup > 2)
        {
            _jumlahPowerup = 2;
        }
        else if (powerup.jumlahPowerup <= 2)
        {
            _jumlahPowerup = 1;
        }

        if (playerController.jumlahLompat > 20)
        {
            _jumlahLompat = 5;
        }
        else if (playerController.jumlahLompat > 15)
        {
            _jumlahLompat = 4;
        }
        else if (playerController.jumlahLompat > 10)
        {
            _jumlahLompat = 3;
        }
        else if (playerController.jumlahLompat > 5)
        {
            _jumlahLompat = 2;
        }
        else if (playerController.jumlahLompat <= 5)
        {
            _jumlahLompat = 1;
        }

        if (playerController.jumlahSlide > 20)
        {
            _jumlahSlide = 5;
        }
        else if (playerController.jumlahSlide > 15)
        {
            _jumlahSlide = 4;
        }
        else if (playerController.jumlahSlide > 10)
        {
            _jumlahSlide = 3;
        }
        else if (playerController.jumlahSlide > 5)
        {
            _jumlahSlide = 2;
        }
        else if (playerController.jumlahSlide <= 5)
        {
            _jumlahSlide = 1;
        }

        if (playerController.jumlahBerpindah > 20)
        {
            _jumlahBerpindah = 5;
        }
        else if (playerController.jumlahBerpindah > 15)
        {
            _jumlahBerpindah = 4;
        }
        else if (playerController.jumlahBerpindah > 10)
        {
            _jumlahBerpindah = 3;
        }
        else if (playerController.jumlahBerpindah > 5)
        {
            _jumlahBerpindah = 2;
        }
        else if (playerController.jumlahBerpindah <= 5)
        {
            _jumlahBerpindah = 1;
        }

        if (playerController.kecepatan > 40)
        {
            _kecepatan = 5;
        }
        else if (playerController.kecepatan > 30)
        {
            _kecepatan = 4;
        }
        else if (playerController.kecepatan > 20)
        {
            _kecepatan = 3;
        }
        else if (playerController.kecepatan > 10)
        {
            _kecepatan = 2;
        }
        else if (playerController.kecepatan <= 10)
        {
            _kecepatan = 1;
        }
    }

    private void HitungPerbandingan(float nilaiA, float nilaiB, float[] listKriteria,int index = 0)
    {
        float selisih = nilaiA - nilaiB;

        if (selisih == 4)
            listKriteria[index] = 5;
        else if (selisih == 3)
            listKriteria[index] = 4;
        else if (selisih == 2)
            listKriteria[index] = 3;
        else if (selisih == 1)
            listKriteria[index] = 2;
        else if (selisih == 0)
            listKriteria[index] = 1;
        else if (selisih == -1)
            listKriteria[index] = 0.5f;
        else if (selisih == -2)
            listKriteria[index] = 0.33f;
        else if (selisih == -3)
            listKriteria[index] = 0.25f;
        else if (selisih == -4)
            listKriteria[index] = 0.2f;

    }

    private void PerbandinganBobot()
    {
        // Kriteria 1
        HitungPerbandingan(_jumlahKoin, _jumlahKoin, listKoin, 0);
        HitungPerbandingan(_jumlahKoin, _jumlahSkor, listKoin, 1);
        HitungPerbandingan(_jumlahKoin, _jarakTempuh, listKoin, 2);
        HitungPerbandingan(_jumlahKoin, _jumlahPowerup, listKoin, 3);
        HitungPerbandingan(_jumlahKoin, _jumlahLompat, listKoin, 4);
        HitungPerbandingan(_jumlahKoin, _jumlahSlide, listKoin, 5);
        HitungPerbandingan(_jumlahKoin, _jumlahBerpindah, listKoin, 6);
        HitungPerbandingan(_jumlahKoin, _kecepatan, listKoin, 7);

        // Kriteria 2
        HitungPerbandingan(_jumlahSkor, _jumlahKoin, listSkor, 0);
        HitungPerbandingan(_jumlahSkor, _jumlahSkor, listSkor, 1);
        HitungPerbandingan(_jumlahSkor, _jarakTempuh, listSkor, 2);
        HitungPerbandingan(_jumlahSkor, _jumlahPowerup, listSkor, 3);
        HitungPerbandingan(_jumlahSkor, _jumlahLompat, listSkor, 4);
        HitungPerbandingan(_jumlahSkor, _jumlahSlide, listSkor, 5);
        HitungPerbandingan(_jumlahSkor, _jumlahBerpindah, listSkor, 6);
        HitungPerbandingan(_jumlahSkor, _kecepatan, listSkor, 7);

        // Kriteria 3
        HitungPerbandingan(_jarakTempuh, _jumlahKoin, listJarak, 0);
        HitungPerbandingan(_jarakTempuh, _jumlahSkor, listJarak, 1);
        HitungPerbandingan(_jarakTempuh, _jarakTempuh, listJarak, 2);
        HitungPerbandingan(_jarakTempuh, _jumlahPowerup, listJarak, 3);
        HitungPerbandingan(_jarakTempuh, _jumlahLompat, listJarak, 4);
        HitungPerbandingan(_jarakTempuh, _jumlahSlide, listJarak, 5);
        HitungPerbandingan(_jarakTempuh, _jumlahBerpindah, listJarak, 6);
        HitungPerbandingan(_jarakTempuh, _kecepatan, listJarak, 7);

        // Kriteria 4
        HitungPerbandingan(_jumlahPowerup, _jumlahKoin, listPowerup, 0);
        HitungPerbandingan(_jumlahPowerup, _jumlahSkor, listPowerup, 1);
        HitungPerbandingan(_jumlahPowerup, _jarakTempuh, listPowerup, 2);
        HitungPerbandingan(_jumlahPowerup, _jumlahPowerup, listPowerup, 3);
        HitungPerbandingan(_jumlahPowerup, _jumlahLompat, listPowerup, 4);
        HitungPerbandingan(_jumlahPowerup, _jumlahSlide, listPowerup, 5);
        HitungPerbandingan(_jumlahPowerup, _jumlahBerpindah, listPowerup, 6);
        HitungPerbandingan(_jumlahPowerup, _kecepatan, listPowerup, 7);

        // Kriteria 5
        HitungPerbandingan(_jumlahLompat, _jumlahKoin, listLompat, 0);
        HitungPerbandingan(_jumlahLompat, _jumlahSkor, listLompat, 1);
        HitungPerbandingan(_jumlahLompat, _jarakTempuh, listLompat, 2);
        HitungPerbandingan(_jumlahLompat, _jumlahPowerup, listLompat, 3);
        HitungPerbandingan(_jumlahLompat, _jumlahLompat, listLompat, 4);
        HitungPerbandingan(_jumlahLompat, _jumlahSlide, listLompat, 5);
        HitungPerbandingan(_jumlahLompat, _jumlahBerpindah, listLompat, 6);
        HitungPerbandingan(_jumlahLompat, _kecepatan, listLompat, 7);

        // Kriteria 6
        HitungPerbandingan(_jumlahSlide, _jumlahKoin, listSlide, 0);
        HitungPerbandingan(_jumlahSlide, _jumlahSkor, listSlide, 1);
        HitungPerbandingan(_jumlahSlide, _jarakTempuh, listSlide, 2);
        HitungPerbandingan(_jumlahSlide, _jumlahPowerup, listSlide, 3);
        HitungPerbandingan(_jumlahSlide, _jumlahLompat, listSlide, 4);
        HitungPerbandingan(_jumlahSlide, _jumlahSlide, listSlide, 5);
        HitungPerbandingan(_jumlahSlide, _jumlahBerpindah, listSlide, 6);
        HitungPerbandingan(_jumlahSlide, _kecepatan, listSlide, 7);

        // Kriteria 7
        HitungPerbandingan(_jumlahBerpindah, _jumlahKoin, listBerpindah, 0);
        HitungPerbandingan(_jumlahBerpindah, _jumlahSkor, listBerpindah, 1);
        HitungPerbandingan(_jumlahBerpindah, _jarakTempuh, listBerpindah, 2);
        HitungPerbandingan(_jumlahBerpindah, _jumlahPowerup, listBerpindah, 3);
        HitungPerbandingan(_jumlahBerpindah, _jumlahLompat, listBerpindah, 4);
        HitungPerbandingan(_jumlahBerpindah, _jumlahSlide, listBerpindah, 5);
        HitungPerbandingan(_jumlahBerpindah, _jumlahBerpindah, listBerpindah, 6);
        HitungPerbandingan(_jumlahBerpindah, _kecepatan, listBerpindah, 7);

        // Kriteria 8
        HitungPerbandingan(_kecepatan, _jumlahKoin, listKecepatan, 0);
        HitungPerbandingan(_kecepatan, _jumlahSkor, listKecepatan, 1);
        HitungPerbandingan(_kecepatan, _jarakTempuh, listKecepatan, 2);
        HitungPerbandingan(_kecepatan, _jumlahPowerup, listKecepatan, 3);
        HitungPerbandingan(_kecepatan, _jumlahLompat, listKecepatan, 4);
        HitungPerbandingan(_kecepatan, _jumlahSlide, listKecepatan, 5);
        HitungPerbandingan(_kecepatan, _jumlahBerpindah, listKecepatan, 6);
        HitungPerbandingan(_kecepatan, _kecepatan, listKecepatan, 7);
    }

    #region AHP

    private void HitungAHP()
    {
        float[] listBobot = new float[8];
        listBobot[0] = bobotKoin;
        listBobot[1] = bobotSkor;
        listBobot[2] = bobotJarak;
        listBobot[3] = bobotPowerup;
        listBobot[4] = bobotLompat;
        listBobot[5] = bobotSlide;
        listBobot[6] = bobotBerpindah;
        listBobot[7] = bobotKecepatan;

        for(int i = 0; i < hasilAHP1.Length; i++)
        {
            hasilAHP1[i] = a1[i] * listBobot[i]; 
            hasilAHP2[i] = a2[i] * listBobot[i];
            hasilAHP3[i] = a3[i] * listBobot[i];
            hasilAHP4[i] = a4[i] * listBobot[i];
            hasilAHP5[i] = a5[i] * listBobot[i];
            hasilAHP6[i] = a6[i] * listBobot[i];
            hasilAHP7[i] = a7[i] * listBobot[i];
            hasilAHP8[i] = a8[i] * listBobot[i];
            hasilAHP9[i] = a9[i] * listBobot[i];
            hasilAHP10[i] = a10[i] * listBobot[i];
            hasilAHP11[i] = a11[i] * listBobot[i];
            hasilAHP12[i] = a12[i] * listBobot[i];
            hasilAHP13[i] = a13[i] * listBobot[i];
            hasilAHP14[i] = a14[i] * listBobot[i];
            hasilAHP15[i] = a15[i] * listBobot[i];
        }

        ahp[0] = hasilAHP1.Average();
        ahp[1] = hasilAHP2.Average();
        ahp[2] = hasilAHP3.Average();
        ahp[3] = hasilAHP4.Average();
        ahp[4] = hasilAHP5.Average();
        ahp[5] = hasilAHP6.Average();
        ahp[6] = hasilAHP7.Average();
        ahp[7] = hasilAHP8.Average();
        ahp[8] = hasilAHP9.Average();
        ahp[9] = hasilAHP10.Average();
        ahp[10] = hasilAHP11.Average();
        ahp[11] = hasilAHP12.Average();
        ahp[12] = hasilAHP13.Average();
        ahp[13] = hasilAHP14.Average();
        ahp[14] = hasilAHP15.Average();

        for (int i = 0; i < ahp.Length; i++)
            {
                rankingAHP[i] = i;
            }

            System.Array.Sort(rankingAHP, (a, b) => ahp[b].CompareTo(ahp[a]));
        
            rankSepatuAHP = rankingAHP[0];
    }

    #endregion
}
