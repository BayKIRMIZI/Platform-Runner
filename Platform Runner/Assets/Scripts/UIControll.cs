using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControll : MonoBehaviour
{
    public GameObject visibleClose;
    public Text lvlText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lvlText.text = PlayerPrefs.GetInt("level").ToString();
        if (Input.touchCount > 0)
        {
            visibleClose.SetActive(false);
        }
    }
}
