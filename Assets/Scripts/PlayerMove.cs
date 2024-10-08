using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    //public float maxSpeed;
    public float jumpPower;
    public float speed = 3.0f;
    public PlayerMove player;
    public GameManager gameManager;
    public Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
   
    public bool isGrounded;
 

    void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
        rigid=GetComponent<Rigidbody2D>();
        
    }


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }
    }
    void Update()
    {  
        //Jump
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)//!anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGrounded = false;
            //anim.SetBool("isJumping", true);
 
        }
    }


    /*//Stop Speed
    if (Input.GetButtonUp("Horizontal"))
    {
        rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

    }*/



    //Animation
    /*if ((anim.GetBool("isJumping"))) Mathf.Abs(rigid.velocity.x) < 0.7*//*rigid.velocity.normalized.x == 0
    {
        anim.SetBool("isWalking", false);
    }
    else
    {
        anim.SetBool("isWalking", true);
    }
}*/
    void FixedUpdate()
    {
        // 키로 움직이는거 안함

        //Landing Platform
     
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
            /*  Debug.DrawRay(rigid.position, Vector3.down*4,   new Color(0, 1, 0));

              RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, 10,  LayerMask.GetMask("Platform"));

              if (rayHit.collider != null)
              {
                 // if (rayHit.distance < 0.5f)
                  {
                     // anim.SetBool("isJumping", false);
                  }
              }
          }*/
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
        if (collision.gameObject.CompareTag ("Platform"))
        {
            isGrounded = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.transform.position.y < -10) // 예: 낭떠러지에 떨어졌는지 확인
            {
                Debug.Log("Player Collision Detected");
                gameManager.HeartDown();
                gameManager.HeartDown();
                gameManager.HeartDown();
                Invoke("Gameover", 0.5f);
            }
        }

        if (collision.gameObject.CompareTag("finish"))
        {
            Debug.Log("Finish Collision Detected");
            gameManager.heart = 3;
            gameManager.Me.SetActive(false);
            gameManager.Stages[gameManager.stageIndex].SetActive(false); // 현재 스테이지 비활성화
            gameManager.StageClear();
            Debug.Log("Game Clear");
        }
    }



    void OnDamaged(Vector2 targetPos)
    {
        //하트 감소
        gameManager.HeartDown();

        //Change Layer(Immortal Active)
        gameObject.layer = 11;

        //View Alpha : 무적시간 투명하게
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Animation
        //anim.SetTrigger("doDamaged");

        Invoke("OffDamaged", 2); // 무적시간 3초 후 푸는 함수 호출
    }

    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    
}
