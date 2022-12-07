using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1Move : MonoBehaviour
{
    public float speed;
    public int jump_force;
    float x; //이동 입력
    private Rigidbody2D rigid;

    private bool canjump;
    public GameObject dieEffect;
    [SerializeField]
    public Transform bow;

    private Animator animator;

    private AudioSource audios;
    public AudioClip jumpSound;
    public AudioClip walkSound;
    bool iswalking = false;

    void Start()
    {
        speed = 10;
        jump_force = 20;
        canjump = true;
        rigid = GetComponent<Rigidbody2D>();
        audios = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
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
            //이동
            x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            animator.SetFloat("speed", Mathf.Abs(x));

            //걷기 애니
            if (x != 0)
            {
                transform.localScale = new Vector3(x * 2.5f, 2.5f, 1);
                bow.transform.localScale = new Vector3(x, 1, 1);
                iswalking = true;
            }
            else iswalking = false;

            //점프
            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                audios.PlayOneShot(jumpSound);
                rigid.velocity = Vector2.up * jump_force;
                canjump = false;
            }

            // y속도로 점프 애니/소리
            if (rigid.velocity.y != 0)
            {
                audios.clip = null;
                animator.SetBool("jumpAnim", true);
            }
            else
                animator.SetBool("jumpAnim", false);

            //걷고있을 때 소리
            if (iswalking)
            {
                if (!audios.isPlaying)
                    audios.Play();
            }
            else
                audios.Stop();
        }
        else
        {
            animator.SetFloat("speed", 0);
            animator.SetBool("jumpAnim", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audios.clip = walkSound;
        if (collision.gameObject.tag == "Land")
            canjump = true;
    }

    void Die()
    {
        if (GameManager.instance.hp1.fillAmount <= 0)
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
        SceneManager.LoadScene("P2GameOver");
    }
}
