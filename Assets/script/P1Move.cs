using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class P1Move : MonoBehaviour
{
    public float speed;
    public int jump_force;
    float x; //이동 입력
    Rigidbody2D rigid;
    private bool canjump;

    public Animator animator;

    void Start()
    {
        speed = 10;
        jump_force = 10;
        canjump = true;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Moving();
        Die();
    }

    void Moving()
    {
        if (GameManager.instance.player1)
        {
            x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            animator.SetFloat("speed", Mathf.Abs(x));

            //Debug.Log(x);
            if (x != 0)
            {
                transform.localScale = new Vector3(x*2.5f,2.5f,1);
            }

            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                rigid.AddForce(Vector2.up * jump_force, ForceMode2D.Impulse);
                canjump = false;
            }
        }
        else
            animator.SetFloat("speed", 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
            canjump = true;
    }

    void Die()
    {
        if (GameManager.instance.hp1.fillAmount <= 0)
            Destroy(gameObject);
    }
}
