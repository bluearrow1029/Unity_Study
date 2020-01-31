using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anime;
    public float speed;
    public float jumpPower;
    float horizontalMove;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Ground" && anime.GetBool("isJump"))
        {
            anime.SetBool("isJump", false);
        }
    }

    void Update()
    {
        //단발 입력은 업데이트에서 쓰는 것이 좋음
        if (Input.GetButtonDown("Jump") && !anime.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anime.SetBool("isJump", true);
        }

        if (Input.GetButtonDown("Horizontal"))
            sprite.flipX = Input.GetAxisRaw("Horizontal") == -1;

        if (horizontalMove == 0)
            anime.SetBool("isWalk", false);
        else
            anime.SetBool("isWalk", true);
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        //물리연산 이동. 가속도가 있음.
        rigid.AddForce(Vector2.right * horizontalMove, ForceMode2D.Impulse);

        if (rigid.velocity.x > speed)
            rigid.velocity = new Vector2(speed, rigid.velocity.y);
        else if (rigid.velocity.x < speed * -1)
            rigid.velocity = new Vector2(speed * -1, rigid.velocity.y);
    }

}
