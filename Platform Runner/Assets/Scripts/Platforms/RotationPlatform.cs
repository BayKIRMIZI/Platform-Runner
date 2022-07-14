using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    int rand;
    public float turnZ = 90;

    void Start()
    {
        rand = Random.Range(0,2);
    }

    void Update()
    {
        // Dönme yönü rastgele
        if(rand == 1)//Sol dönüş
        {
            transform.Rotate(new Vector3(0, 0, turnZ) * Time.deltaTime);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, -turnZ) * Time.deltaTime);
        }
    }

    // velocity ile çarpma etkisine bak bir de onu dene

    private void OnCollisionStay(Collision collision)
    {
        // Karakter platform üzerindeyse karakteri dönüş yönünde sürükle
        if (collision.gameObject.tag == "Player")
        {
            if (rand == 1)
            { 
                collision.transform.Translate(-Time.deltaTime * 5f, 0, 0);
            }
            else
            {
                collision.transform.Translate(Time.deltaTime * 5f, 0, 0);
            }
        }
        
    }


}
