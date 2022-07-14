using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRunner : MonoBehaviour
{
    // Oyun objeleri
    public GameObject[] obstacles;
    public GameObject[] platforms;

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

        // Başlar başlamaz level yükle
        Instantiate(platforms[platformStart]);
        Platform_Maker(LevelFollow(PlayerPrefs.GetInt("level")));
    }

    
    void Update()
    {
       
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
            // Levele göre platform belirle
            int selectPlatform;
            if (PlayerPrefs.GetInt("level") < 10)
            {
                selectPlatform = 2; 
            }
            else
            {
                selectPlatform = Random.Range(2, 6);
            }

            // Dönen Platformlar veya düz platforma göre klonla
            if (selectPlatform >= 3)
            {
                Instantiate(platforms[selectPlatform], new Vector3(0, -9.55f, i * 30f), transform.rotation);

            }
            else
            {
                Instantiate(platforms[selectPlatform], new Vector3(0, 0, i * 30f), transform.rotation);
                
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
        Instantiate(platforms[platformFinish], new Vector3(0, 0, (i+1) * 30f), Quaternion.Euler(0,-90,0));

    }

    void Obstacle_Maker(int obs, float zPos)
    {
        // 0-> Static
        // 1-> Horizonal
        // 2-> Rotator
        // 3-> Half Donut
        switch(obs)
        { 
            case 0:
                float xPos = Random.Range(-1,2);
                Instantiate(obstacles[obs],new Vector3(xPos*5,0,zPos),transform.rotation);
                break;

            case 1:
                // X ekseninde hareket edecek kendi etrafında dönecek
                Instantiate(obstacles[obs], new Vector3(Random.Range(-1,1)*5, 0, zPos), transform.rotation);
                break;

            case 2:
                // Yerinde sabit kendi etrafında dönecek
                Instantiate(obstacles[obs], new Vector3(0, 0, zPos), transform.rotation);
                break;

            case 3:
                int xYon = Random.Range(0,2);
                if(xYon == 0)
                {
                    Instantiate(obstacles[obs], new Vector3(7.5f, 0, zPos), Quaternion.Euler(0,0,0));
                }
                else
                {
                    Instantiate(obstacles[obs], new Vector3(-7.5f, 0, zPos), Quaternion.Euler(0, -180, 0));
                }
                break;

            
        }

    }
}
