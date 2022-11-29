using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow2 : MonoBehaviour
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

        destroyArrow();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
            GameManager.instance.hpDecrease(1);

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player1")
        {
            stuckArrow();
            transform.SetParent(collision.transform); // 몸에 화살 박히는거
        }
        else if (collision.tag == "Land")
            stuckArrow();

        GameManager.instance.player2 = false;
        GameManager.instance.player1 = true;
    }

    private void wait()
    {
        GameManager.instance.followingArrow = false;
        gameObject.tag = "Stick";

        GetComponent<Collider2D>().enabled = false;
    }

    private void stuckArrow()
    {
        rb.isKinematic = true; //화살 고정
        rotate = false; //회전 고정
        rb.velocity = Vector2.zero; //속도 삭제

        Invoke("wait", 1);
    }

    private void destroyArrow()
    {
        if(transform.position.y < -20f)
        {
            GameManager.instance.player2 = false;
            GameManager.instance.player1 = true;

            GameManager.instance.followingArrow = false;
            Destroy(gameObject);
        }
    }
}
