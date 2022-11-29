using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //�̱���
    public static GameManager instance = null;

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
    public Animator animator;

    [Header("p1 hp")]
    public Image hp1;
    [Header("p2 hp")]
    public Image hp2;
    void Start()
    {
        hp1.fillAmount = 0.25f;
        hp2.fillAmount = 0.25f;
    }

    void Update()
    {
        
    }

    public void hpDecrease(int who)
    {
        if (who == 1) {
            hp1.fillAmount -= 0.25f;
        }
        else
        {
            hp2.fillAmount -= 0.25f;
        }
            
    }

}
