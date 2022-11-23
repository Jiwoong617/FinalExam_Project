using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�̱���
    public static GameManager instance = null;


    //distanceUI
    public Text p1_distanceUI;
    public Text p2_distanceUI;
    public float distance = 0f;
    public GameObject p1;
    public GameObject p2;
    public GameObject arrow;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        //�� �̵�/���� ������ DontDestroyOnLoad ��� �ɵ�?
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    public bool player1 = true; //1p ī�޶�
    public bool player2 = false; //2p ī�޶�
    public bool followingArrow = false; //ȭ��

    [Header("p1 hp")]
    public Image hp1;
    [Header("p2 hp")]
    public Image hp2;

    void Start()
    {
        hp1.fillAmount = 1;
        hp2.fillAmount = 1;
    }

    void Update()
    {
        //if (player1 && followingArrow)
        //{
        //    arrow = GameObject.FindGameObjectWithTag("Arrow");
        //    distance = UnityEngine.Vector2.Distance(p1.transform.position, arrow.transform.position);
        //    p1_distanceUI.text = distance.ToString("F1") + "m";

        //} else if(player2 && followingArrow)
        //{
        //    arrow = GameObject.FindGameObjectWithTag("Arrow");
        //    distance = UnityEngine.Vector2.Distance(p2.transform.position, arrow.transform.position);
        //    p2_distanceUI.text = distance.ToString("F1") + "m";
        //}
        
        
    }

<<<<<<< HEAD
=======
    public void hpDecrease(int who)
    {
        if (who == 1)
            hp1.fillAmount -= 0.25f;
        else
            hp2.fillAmount -= 0.25f;
    }
>>>>>>> 968046f25c1f48e5aad53b00e0ce805bf5b34811
}
