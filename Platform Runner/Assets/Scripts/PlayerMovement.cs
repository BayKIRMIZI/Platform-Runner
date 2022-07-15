using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // Karakterin hareket kabiliyetleri
    public float forwadSpeed = 5;
    public float horMoveSpeed = 3;
    public float horLimit = 6;
    Vector3 startPos;

    // Animasyon kotrolleri
    public bool isRunning = false;
    public bool isKnocked = false;
    public bool isIdle = true;
    public Animator anim;

    // UI 
    public GameObject visibleUI;

    // Level
    public PlatformRunner pr;

    private void Awake()
    {
        // Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        startPos = transform.position;
    }
    
    private void Update()
    {
        // Ekrana dokunma algılandıysa koşmaya başla
        ScreenTouch();
        
        // Animasyon ve Hareket kontrolleri
        AnimControl();
        
        SpeedControl();
    }

    void ScreenTouch()
    {
        if (!isRunning)
        {
            if (Input.touchCount > 0)
            {
                isRunning = true;
                isIdle = false;
                isKnocked = false;
            }
        }
    }

    void AnimControl()
    {
        // Çarpışma olmadığı sürece koşmaya devam et
        if (isRunning)
        {
            //PlayerMove();
            TouchAndMove();
            
            // Animasyon kontrolleri
            anim.SetBool("Running", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Knocked", false);
        }
        // Çarpışma olduysa animasyon değiştir
        if (isKnocked)
        {
            // Animasyon çarpma başlat koşma durdur 
            anim.SetBool("Running", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Knocked", true);
        }
        if (isIdle)
        {
            anim.SetBool("Running", false);
            anim.SetBool("Idle", true);
            anim.SetBool("Knocked", false);
        }
    }

    // Dokunma ile hareket
    void TouchAndMove()
    {
        // Sürekli ileri hareket
        transform.Translate(0, 0, forwadSpeed * Time.deltaTime);
        // X ekseninde hareket
        float horMovement = horMoveSpeed * Time.deltaTime;
        //**********************************************************//
        if (Input.GetTouch(0).position.x < Screen.width / 2 && -6 <= Mathf.Clamp(transform.position.x, -7, 7))
        {
            transform.Translate(-horMovement, 0, 0); // Sola
        }
        else if (Input.GetTouch(0).position.x >= Screen.width / 2 && Mathf.Clamp(transform.position.x, -7, 7) <= 6)
        {
            transform.Translate(horMovement, 0, 0); // Sağa
        }
    }

    // Klavye ile hareket
    void PlayerMove()
    {
        // Sürekli ileri hareket
        float horMovement = Input.GetAxis("Horizontal") * horMoveSpeed * Time.deltaTime;
        // Yatay eksende hareket
        transform.Translate(horMovement, 0, forwadSpeed * Time.deltaTime);
    }

    void SpeedControl()
    {
        if (PlayerPrefs.GetInt("level") <= 5)
        {
            forwadSpeed = 5;
            horMoveSpeed = 3;
        }
        else if (PlayerPrefs.GetInt("level") <= 5)
        {
            forwadSpeed = 6;
            horMoveSpeed = 3;
        }
        else if (PlayerPrefs.GetInt("level") <= 10)
        {
            forwadSpeed = 7;
            horMoveSpeed = 4;
        }
        else if (PlayerPrefs.GetInt("level") <= 20)
        {
            forwadSpeed = 8;
            horMoveSpeed = 4;
        }
        else if (PlayerPrefs.GetInt("level") <= 50)
        {
            forwadSpeed = 9;
            horMoveSpeed = 5;
        }
        else if (PlayerPrefs.GetInt("level") <= 100)
        {
            forwadSpeed = 10;
            horMoveSpeed = 5;
        }
        else 
        {
            forwadSpeed = 11;
            horMoveSpeed = 6;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Engele çarptıysa animasyon değiştir
        if (collision.gameObject.tag == "Obstacle")
        {
            isRunning = false;
            isKnocked = true;
            isIdle = false;

            // Çarptıktan sonra başa dön
            Invoke("BackToStart", 3);
        }

           
        if (collision.gameObject.tag == "Finish")
        {
            // Leveli artır, Koşma durdur, Duvar boyama işlemi? Yeni level ve başa dön
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level")+1);
            BackToStart();
            pr.LevelUpload();
        }
    }

    // Bitişe varınca 3-5 sn bekle yeni leveli yükle
    void BackToStart()
    {
        // Çarptıktan sonra başa dön
        transform.position = startPos;

        {
            // Sahneyi yeniden yükle
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Menü Ekranı Getir
        visibleUI.SetActive(true);

        // Bekleme Animasyonuna geç
        anim.SetBool("Idle", true);
        anim.SetBool("Running", false);
        anim.SetBool("Knocked", false);
        isRunning = false;
        isKnocked = false;
        isIdle = true;
        CancelInvoke("StartPosition");
        
    }

   
}
