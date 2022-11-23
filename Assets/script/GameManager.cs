using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
<<<<<<< HEAD

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
=======
>>>>>>> parent of 8b84782 (kjy)
}
