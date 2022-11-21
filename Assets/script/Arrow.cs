using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool rotate;

    private void Start()
    {
        rotate = true;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(rotate)
            transform.right = GetComponent<Rigidbody2D>().velocity;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //몸 박히면 피 다는 구현
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (GameManager.instance.player1)
        {
            GameManager.instance.player1 = false;
            GameManager.instance.player2 = true;
        }
        else if (GameManager.instance.player2)
        {
            GameManager.instance.player2 = false;
            GameManager.instance.player1 = true;
        }

        rb.isKinematic = true; //화살 고정
        rotate = false; //회전 고정
        rb.velocity = Vector2.zero; //속도 삭제
        Invoke("wait", 1);
    }

    private void wait()
    {
        GameManager.instance.followingArrow = false;
        gameObject.tag = "Stick";
    }
}
