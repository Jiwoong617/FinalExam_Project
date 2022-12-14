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
        //awake(update ���� 1������ ����)���� �̱��� ����
        if(instance == null)
            instance = this;
        //�� �̵�/���� ������ DontDestroyOnLoad ��� �ɵ�?
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    //ī�޶� ����, �÷��̾� ���� �οﰪ
    public bool player1 = true; //1p
    public bool player2 = false; //2p
    public bool followingArrow = false; //ȭ��
    public Animator animator;

    //�÷��̾� hp�̹��� �ν����Ϳ��� �ֱ�
    [Header("p1 hp")]
    public Image hp1;
    [Header("p2 hp")]
    public Image hp2;

    void Start()
    {
        //���۽� hp�� Ǯ�� ä���
        hp1.fillAmount = 1;
        hp2.fillAmount = 1;
    }

    //ȭ�쿡 ������ ���� �÷��̾� �޾ƿͼ� 25�۾� ��� �޼ҵ�
    public void hpDecrease(int who)
    {
        if (who == 1)
            hp1.fillAmount -= 0.25f;
        else
            hp2.fillAmount -= 0.25f;
    }

}
