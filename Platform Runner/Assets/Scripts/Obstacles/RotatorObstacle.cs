using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorObstacle : MonoBehaviour
{
    public float turnY;

    void Start()
    {
        // Her oluşturulan engelin dönüş yonü rastgele
        if (Random.Range(0, 2) == 0)
        {
            turnY = -90f;
        }
        else
        {
            turnY = 90f;
        }
    }


    void Update()
    {
        transform.Rotate(new Vector3(0, turnY, 0) * Time.deltaTime);
    }
}
