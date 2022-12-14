using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤
    public static GameManager instance = null;
    private void Awake()
    {
        //awake(update 보다 1프레임 빠름)에서 싱글톤 구현
        if(instance == null)
            instance = this;
        //씬 이동/조작 없으니 DontDestroyOnLoad 없어도 될듯?
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    //카메라 추적, 플레이어 동작 부울값
    public bool player1 = true; //1p
    public bool player2 = false; //2p
    public bool followingArrow = false; //화살
    public Animator animator;

    //플레이어 hp이미지 인스펙터에서 넣기
    [Header("p1 hp")]
    public Image hp1;
    [Header("p2 hp")]
    public Image hp2;

    void Start()
    {
        //시작시 hp값 풀로 채우기
        hp1.fillAmount = 1;
        hp2.fillAmount = 1;
    }

    //화살에 맞으면 맞은 플레이어 받아와서 25퍼씩 까는 메소드
    public void hpDecrease(int who)
    {
        if (who == 1)
            hp1.fillAmount -= 0.25f;
        else
            hp2.fillAmount -= 0.25f;
    }

}
