using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P2Move : MonoBehaviour
{
    public float speed; //이동 속도
    public int jump_force; //점프 높이
    float x; //이동 입력 x축
    private Rigidbody2D rigid;

    private bool canjump; //뛸 수 있는지
    public GameObject dieEffect; //죽는 애니가 담긴 오브젝트
    [SerializeField]
    private Transform bow; //화살 오브젝트

    private Animator animator;

    private AudioSource audios; //오디오 컨트롤러
    public AudioClip jumpSound; //점프 소리
    public AudioClip walkSound; //걷는 소리
    bool iswalking = false; //걷고 있는지, 사운드 출력용 bool값

    void Start()
    {
        //초기값 세팅
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
        if (GameManager.instance.player2)
        {
            //이동
            x = Input.GetAxisRaw("Horizontal");
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            animator.SetFloat("speed", Mathf.Abs(x));

            //걷기 애니
            if (x != 0)
            {
                transform.localScale = new Vector3(x * 4.2f, 4.2f, 1);
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
