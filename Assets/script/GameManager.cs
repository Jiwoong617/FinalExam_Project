using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
