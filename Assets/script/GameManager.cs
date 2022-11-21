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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
