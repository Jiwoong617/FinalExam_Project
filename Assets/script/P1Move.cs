using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1Move : MonoBehaviour
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

    //플레이어의 이동 담당(소리 및 애니메이션까지 들어있음)
    void Moving()
    {
        //게임 매니저의 player1 값이 참이면
        if (GameManager.instance.player1)
        {
            //입력을 받아 수평(x축) 기준으로 왼쪽: -1 오른쪽: 1 값 리턴
            x = Input.GetAxisRaw("Horizontal");
            //translate로 캐릭터를 x의 입력에 맞게 평행이동, deltaTime을 곱하여 부드럽게
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            //이동애니메이션의 트리거 활성화
            animator.SetFloat("speed", Mathf.Abs(x));

            if (x != 0)
            {
                //localScale을 활용하여 캐릭터의 크기, 좌우 반전 조정
                transform.localScale = new Vector3(x * 2.5f, 2.5f, 1);
                bow.transform.localScale = new Vector3(x, 1, 1); //화살도 좌우 반전
                iswalking = true; //걷고 있는지
            }
            else iswalking = false; //걷는 중이 아니므로 false

            //뛸 수 있는 상태이고, 스페이스바를 눌렀으면
            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                //점프 소리를 한번 출력
                audios.PlayOneShot(jumpSound);
                //(0,1)벡터에 점프 힘을 곱하여 점프하게 velocity값 설정
                rigid.velocity = Vector2.up * jump_force;
                canjump = false; //뛰었으니 더는 못뛰게 false
            }

            // y속도로 점프 애니/소리
            if (rigid.velocity.y != 0)
            {
                //점프 중이면
                //걷는 소리와 겹치지 않게 오디오소스를 null값으로
                audios.clip = null; 
                //점프 애니 활성화
                animator.SetBool("jumpAnim", true);
            }
            else //점프가 끝나거나 아니면 점프 애니 비활성화
                animator.SetBool("jumpAnim", false);

            //걷고있을 때 소리
            if (iswalking)
            {
                //걷고 있으니 중지되었으면 다시 재생
                if (!audios.isPlaying)
                    audios.Play();
            }
            else //오디오 소스 재생 중지
                audios.Stop();
        }
        //플레이어1이 아니면
        else
        {
            //플레이어 변경 시 재생되는 애니가 없도록 비활성화
            animator.SetFloat("speed", 0);
            animator.SetBool("jumpAnim", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌 시 땅 위에 있는 것으로 가정
        audios.clip = walkSound;//오디오 소스에 걷는 소리 대입
        //땅과 부딛쳤으면
        if (collision.gameObject.tag == "Land")
            canjump = true; //점프 가능
    }

    //플레이어 사망 메소드
    void Die()
    {
        //게임 매니저의 player1 hp가 0 이하이면 실행
        if (GameManager.instance.hp1.fillAmount <= 0)
        {
            //죽는 이펙트를 가진 오브젝트를 플레이어 위치로 가져와 실행
            dieEffect.gameObject.SetActive(true);
            dieEffect.transform.position = this.transform.position;
            this.gameObject.SetActive(false); //플레이어 비활성화
            Invoke("SceneChange", 1f); //1초뒤 씬 변환 메소드 실행(애니매이션의 재생시간이 1초)
            //Destroy(gameObject);
        }
    }

    //씬 변환 메소드
    private void SceneChange()
    {
        SceneManager.LoadScene("P2GameOver");
    }
}
