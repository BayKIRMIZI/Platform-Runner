using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject character;
    // Kameranın takip mesafesi
    public Vector3 distance;
    public float lerpSpeed = 1;

    void Update()
    {
        
    }
    
    // Sağa sola hareketlerde kamera da karakterle birlikte sağa sola hareket eder
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, character.transform.position + distance, Time.deltaTime * lerpSpeed);

    }
}
