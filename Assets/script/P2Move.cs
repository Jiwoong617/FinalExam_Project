using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class P2Move : MonoBehaviour
{
    public float speed;
    public int jump_force;
    float x; //�̵� �Է�
    Rigidbody2D rigid;
    private bool canjump;
    public GameObject dieEffect;
    public Transform bow; //Ȱ ��ġ
    public Animator animator;
    public AudioSource walkSound;
    public AudioSource jumpSound;
    bool iswalking = false;

    void Start()
    {
        speed = 10;
        jump_force = 12;
        canjump = true;
        rigid = GetComponent<Rigidbody2D>();
        walkSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Moving();
        Die();
        
    }

    void Moving()
    {
        if (GameManager.instance.player2)
        {
            x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            animator.SetFloat("speed", Mathf.Abs(x));

            if (x != 0)
            {
                transform.localScale = new Vector3(x * 4.2f, 4.2f, 1);
                bow.transform.localScale = new Vector3(x, 1, 1);
                iswalking = true;
            }
            else
            {
                iswalking = false;
            }
            // �ȱ� �Ҹ�
            if (iswalking)
            {
                if(!walkSound.isPlaying)
                {
                    walkSound.Play();
                }
            }
            else
            {
                walkSound.Stop();
            }
            
            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                jumpSound.Play();
                rigid.velocity = Vector2.up * jump_force;
                canjump = false;
            }

            if (rigid.velocity.y != 0)
            {
                walkSound.Stop();
                animator.SetBool("jumpAnim", true);
            }
            else
                animator.SetBool("jumpAnim", false);
        }
        else
        {
            walkSound.Stop();
            animator.SetFloat("speed", 0);
            animator.SetBool("jumpAnim", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Land")
            canjump = true;
    }

    void Die()
    {
        if (GameManager.instance.hp2.fillAmount <= 0)
        {
            dieEffect.gameObject.SetActive(true);
            dieEffect.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
            Invoke("SceneChange", 1f);
            //Destroy(gameObject);
        }
            
    }
    private void SceneChange()
    {
        SceneManager.LoadScene("P1GameOver");
    }
}
