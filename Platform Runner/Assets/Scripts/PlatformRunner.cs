using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRunner : MonoBehaviour
{
    // Oyun objeleri
    public GameObject[] obstacles; // Editörden aktarılan engeller
    public GameObject[] platforms; // Editörden aktarılan platformlar
    List<GameObject> objectClones; // Klonlanan tüm objeler

    // Levele göre objelerin dinamik oluşturulması
    int platformCount;



    int platformStart = 0;
    int platformFinish = 1;

    void Start()
    {
        // Level bilgisi yoksa oluştur varsa değişkene ata
        if (!PlayerPrefs.HasKey("level"))
        {
            PlayerPrefs.SetInt("level",1);
        }
        objectClones = new List<GameObject>();

        // Başlangıç platformunu ekle standart 0,0,0 konumu
        Instantiate(platforms[platformStart]);
        // Levele göre platform sayısı belirle
        Platform_Maker(LevelFollow(PlayerPrefs.GetInt("level")));
    }

    
    void Update()
    {
       // print(objectClones.Count);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            // Leveli artır, Koşma durdur, Duvar boyama işlemi? Yeni level ve başa dön
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            LevelUpload();
        }
    }

    int LevelFollow(int level)
    {
        // forwad speed -> 5
        // hor Speed -> 3
        if (level <= 2)
        {
            platformCount = 5;
        }
        else if (level > 2 && level <=5)
        {
            platformCount = 6;
        }
        else if (level > 5 && level <=10)
        {
            platformCount = 7;
        }
        else if (level > 10 && level <=20)
        {
            platformCount = 8;
        }
        else if (level > 20 && level <=50)
        {
            platformCount = 9;
        }
        else if (level > 50 && level <=100)
        {
            platformCount = 10;
        }
        else
        {
            platformCount = 11;
        }

        return platformCount;
    }

    void Platform_Maker(int maxPlatformCount)
    {
        int i;

        for ( i=0; i<maxPlatformCount; i++)
        {
            int selectPlatform;
            // 10. levelden öncesi sadece düz platform ve engellerden oluşur
            if (PlayerPrefs.GetInt("level") < 10)
            {
                selectPlatform = 2; // Düz platform index -> 2
            }
            else // Sonrası dönen platformlarda dahil olur
            {
                // 0 ve 1. index start ve finish platformları
                selectPlatform = Random.Range(2, 6); // 2-6 index düz ve dönen platformlar
            }

            // Dönen Platformlar veya düz platforma göre klonla
            if (selectPlatform >= 3)
            {
                // Dönen Platformlar
                objectClones.Add(Instantiate(platforms[selectPlatform], new Vector3(0, -9.55f, i * 30f), transform.rotation));
            }
            else
            {
                // Düz Platform
                objectClones.Add(Instantiate(platforms[selectPlatform], new Vector3(0, 0, i * 30f), transform.rotation));
                // Her düz platformda sabit 3 engel
                for(int obsCount=0; obsCount<3; obsCount++)
                {
                    // Levele göre engellerin seçimi
                    int obsSelect;
                    if (PlayerPrefs.GetInt("level") <= 5)
                    {
                        obsSelect = Random.Range(0, 1);
                    }
                    else if (PlayerPrefs.GetInt("level") <= 10)
                    {
                        obsSelect = Random.Range(0, 2);
                    }
                    else if (PlayerPrefs.GetInt("level") <= 20)
                    {
                        obsSelect = Random.Range(0, 3);
                    }
                    else
                    {
                        obsSelect = Random.Range(0, 4);
                    }
                    // Her engel bi öncekinden 10birim ileride
                    // İlk engel platform başlangıcından 3 birim ileride
                    Obstacle_Maker(obsSelect, i*30 + obsCount*10 + 3);
                } 
            }
        }
        objectClones.Add(Instantiate(platforms[platformFinish], new Vector3(0, 0, (i+1) * 30f), Quaternion.Euler(0,-90,0)));
    }

    void Obstacle_Maker(int obs, float zPos)
    {
        // 0-> Static engel
        // 1-> Horizonal engel
        // 2-> Rotator engel
        // 3-> Half Donut engel
        switch(obs)
        { 
            case 0:
                float xPos = Random.Range(-1,2);
                objectClones.Add(Instantiate(obstacles[obs],new Vector3(xPos*5,0,zPos),transform.rotation));
                break;

            case 1:
                // X ekseninde hareket edecek kendi etrafında dönecek
                objectClones.Add(Instantiate(obstacles[obs], new Vector3(Random.Range(-1,1)*5, 0, zPos), transform.rotation));
                break;

            case 2:
                // Yerinde sabit kendi etrafında dönecek
                objectClones.Add(Instantiate(obstacles[obs], new Vector3(0, 0, zPos), transform.rotation));
                break;

            case 3:
                int xYon = Random.Range(0,2);
                // Rotator engeli dönme yönü
                if(xYon == 0)
                {
                    objectClones.Add(Instantiate(obstacles[obs], new Vector3(7.5f, 0, zPos), Quaternion.Euler(0,0,0)));
                }
                else
                {
                    objectClones.Add(Instantiate(obstacles[obs], new Vector3(-7.5f, 0, zPos), Quaternion.Euler(0, -180, 0)));
                }
                break;

            
        }

    }

    public void LevelUpload()
    {
        for (int i = objectClones.Count - 1; i >= 0; i--)
        {
            print(objectClones.Count);
            Destroy(objectClones[i]);
        }
        objectClones.Clear();
        Platform_Maker(LevelFollow(PlayerPrefs.GetInt("level")));
    }
}
