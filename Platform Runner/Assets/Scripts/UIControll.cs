using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControll : MonoBehaviour
{
    public Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        levelText.text = PlayerPrefs.GetInt("level").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetTouch(0) == true)
        {
            Application.Quit();
        }
        
    }
}
