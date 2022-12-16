using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class P1Move : MonoBehaviour
{
    public float speed; //�̵� �ӵ�
    public int jump_force; //���� ����
    float x; //�̵� �Է� x��
    private Rigidbody2D rigid;

    private bool canjump; //�� �� �ִ���
    public GameObject dieEffect; //�״� �ִϰ� ��� ������Ʈ
    [SerializeField]
    private Transform bow; //ȭ�� ������Ʈ

    private Animator animator;

    private AudioSource audios; //����� ��Ʈ�ѷ�
    public AudioClip jumpSound; //���� �Ҹ�
    public AudioClip walkSound; //�ȴ� �Ҹ�
    bool iswalking = false; //�Ȱ� �ִ���, ���� ��¿� bool��

    void Start()
    {
        //�ʱⰪ ����
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

    //�÷��̾��� �̵� ���(�Ҹ� �� �ִϸ��̼Ǳ��� �������)
    void Moving()
    {
        //���� �Ŵ����� player1 ���� ���̸�
        if (GameManager.instance.player1)
        {
            //�Է��� �޾� ����(x��) �������� ����: -1 ������: 1 �� ����
            x = Input.GetAxisRaw("Horizontal");
            //translate�� ĳ���͸� x�� �Է¿� �°� �����̵�, deltaTime�� ���Ͽ� �ε巴��
            transform.Translate(Vector2.right * x * speed * Time.deltaTime);
            //�̵��ִϸ��̼��� Ʈ���� Ȱ��ȭ
            animator.SetFloat("speed", Mathf.Abs(x));

            if (x != 0)
            {
                //localScale�� Ȱ���Ͽ� ĳ������ ũ��, �¿� ���� ����
                transform.localScale = new Vector3(x * 2.5f, 2.5f, 1);
                bow.transform.localScale = new Vector3(x, 1, 1); //ȭ�쵵 �¿� ����
                iswalking = true; //�Ȱ� �ִ���
            }
            else iswalking = false; //�ȴ� ���� �ƴϹǷ� false

            //�� �� �ִ� �����̰�, �����̽��ٸ� ��������
            if (Input.GetKeyDown(KeyCode.Space) && canjump)
            {
                //���� �Ҹ��� �ѹ� ���
                audios.PlayOneShot(jumpSound);
                //(0,1)���Ϳ� ���� ���� ���Ͽ� �����ϰ� velocity�� ����
                rigid.velocity = Vector2.up * jump_force;
                canjump = false; //�پ����� ���� ���ٰ� false
            }

            // y�ӵ��� ���� �ִ�/�Ҹ�
            if (rigid.velocity.y != 0)
            {
                //���� ���̸�
                //�ȴ� �Ҹ��� ��ġ�� �ʰ� ������ҽ��� null������
                audios.clip = null; 
                //���� �ִ� Ȱ��ȭ
                animator.SetBool("jumpAnim", true);
            }
            else //������ �����ų� �ƴϸ� ���� �ִ� ��Ȱ��ȭ
                animator.SetBool("jumpAnim", false);

            //�Ȱ����� �� �Ҹ�
            if (iswalking)
            {
                //�Ȱ� ������ �����Ǿ����� �ٽ� ���
                if (!audios.isPlaying)
                    audios.Play();
            }
            else //����� �ҽ� ��� ����
                audios.Stop();
        }
        //�÷��̾�1�� �ƴϸ�
        else
        {
            //�÷��̾� ���� �� ����Ǵ� �ִϰ� ������ ��Ȱ��ȭ
            animator.SetFloat("speed", 0);
            animator.SetBool("jumpAnim", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�浹 �� �� ���� �ִ� ������ ����
        audios.clip = walkSound;//����� �ҽ��� �ȴ� �Ҹ� ����
        //���� �ε�������
        if (collision.gameObject.tag == "Land")
            canjump = true; //���� ����
    }

    //�÷��̾� ��� �޼ҵ�
    void Die()
    {
        //���� �Ŵ����� player1 hp�� 0 �����̸� ����
        if (GameManager.instance.hp1.fillAmount <= 0)
        {
            //�״� ����Ʈ�� ���� ������Ʈ�� �÷��̾� ��ġ�� ������ ����
            dieEffect.gameObject.SetActive(true);
            dieEffect.transform.position = this.transform.position;
            this.gameObject.SetActive(false); //�÷��̾� ��Ȱ��ȭ
            Invoke("SceneChange", 1f); //1�ʵ� �� ��ȯ �޼ҵ� ����(�ִϸ��̼��� ����ð��� 1��)
            //Destroy(gameObject);
        }
    }

    //�� ��ȯ �޼ҵ�
    private void SceneChange()
    {
        SceneManager.LoadScene("P2GameOver");
    }
}
