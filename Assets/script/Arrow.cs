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
        //�� ������ �� �ٴ� ����
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

        rb.isKinematic = true; //ȭ�� ����
        rotate = false; //ȸ�� ����
        rb.velocity = Vector2.zero; //�ӵ� ����
        Invoke("wait", 1);
    }

    private void wait()
    {
        GameManager.instance.followingArrow = false;
        gameObject.tag = "Stick";
    }
}
