using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonalObstacle : MonoBehaviour
{

    public float turnY = 90;
    public float moveSpeed = 3;
    public Vector3 xPosition1;
    public Vector3 xPosition2;

    private void Start()
    {
        xPosition1 = new Vector3(-6, transform.position.y, transform.position.z);
        xPosition2 = new Vector3(6, transform.position.y, transform.position.z);
      
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, turnY, 0) * Time.deltaTime);

        transform.position = Vector3.MoveTowards(transform.position, xPosition1, moveSpeed * Time.deltaTime);

        if (transform.position == xPosition1)
        {
            xPosition1 = xPosition2;
            if (xPosition1 == xPosition2)
            {
                xPosition2 = transform.position;
            }
        }

    }
}
