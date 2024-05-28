using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public PlayerMove player;
    public GameManager gameManager;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //Jump
        if (Input.GetButtonUp("Jump")/*!anim.GetBool("isJumping")*/)
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }


        /*//Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);

        }*/

        

        //Animation
        if ((anim.GetBool("isJumping")))/*Mathf.Abs(rigid.velocity.x) < 0.7*//*rigid.velocity.normalized.x == 0*/
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }
    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);
        if (rigid.velocity.x >= 0)
            rigid.velocity = new Vector2(0, rigid.velocity.y); //maxSpeed
        else if (rigid.velocity.x < 0)
            rigid.velocity = new Vector2(0, rigid.velocity.y);

        //Landing Platform
        if (rigid.velocity.y < 0)
        {


            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            //Next Stage
            gameManager.heart = 3;
            gameManager.NextStage();
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
