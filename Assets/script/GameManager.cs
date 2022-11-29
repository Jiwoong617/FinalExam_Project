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
        if(instance == null)
            instance = this;
        //씬 이동/조작 없으니 DontDestroyOnLoad 없어도 될듯?
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    public bool player1 = true; //1p 카메라
    public bool player2 = false; //2p 카메라
    public bool followingArrow = false; //화살
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
