using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutObstacle : MonoBehaviour
{
    public Vector3 xPosition1;
    public Vector3 xPosition2;
    public float moveSpeed = 5;
    bool waitCheck = false;

    void Start()
    {
        xPosition2 = new Vector3(0.15f, transform.localPosition.y, transform.localPosition.z);
        xPosition1 = new Vector3(-0.1f, transform.localPosition.y, transform.localPosition.z);
        //InvokeRepeating("MoveStick",0,3);
    }


    void Update()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, xPosition1, moveSpeed * Time.deltaTime);

        if (transform.localPosition == xPosition1)
        {
            
            Invoke("MoveStick", 2);
            
            if(waitCheck == true)
            {
                xPosition1 = xPosition2;
                if (xPosition1 == xPosition2)
                {
                    xPosition2 = transform.localPosition;
                }
                CancelInvoke("MoveStick");
            }
            waitCheck = false;
        }
    }

    void MoveStick()
    {
        waitCheck = true;
        
    }

    
}
