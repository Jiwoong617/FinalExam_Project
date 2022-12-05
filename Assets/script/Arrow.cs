using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool rotate;

    private AudioSource audios;

    private void Start()
    {
        rotate = true;
        rb = GetComponent<Rigidbody2D>();
        audios = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(rotate)
            transform.right = GetComponent<Rigidbody2D>().velocity;

        destroyArrow();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            GameManager.instance.hpDecrease(2);
            GetComponent<ParticleSystem>().Play();
            audios.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player2")
        {
            stuckArrow();
            transform.SetParent(collision.transform); // 몸에 화살 박히는거
        }
        else if (collision.tag == "Land")
            stuckArrow();


        GameManager.instance.player1 = false;
        GameManager.instance.player2 = true;
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
            GameManager.instance.player1 = false;
            GameManager.instance.player2 = true;

            GameManager.instance.followingArrow = false;
            Destroy(gameObject);
        }
    }
}
