using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerctrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    private Rigidbody2D HeroBody;
    public bool bFaceRight = true;
    public bool bJump = false;
    public float JumpForce = 100;
    private Transform mGroundCheak;
    GameObject UFO;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGroundCheak = transform.Find("GroundCheak");
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }
        if(Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x)*MaxSpeed,
                                            HeroBody.velocity.y);
        }
        //转身
        if(h>0 && !bFaceRight)
        {
            flip();
        }
        else if(h<0 && bFaceRight)
        {
            flip();
        }
        //地面检测
        if(Physics2D.Linecast(transform.position, mGroundCheak.position, 1<<LayerMask.NameToLayer("Ground")))
        {
            if(Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if(bJump)
        {
            HeroBody.AddForce(Vector2.up * JumpForce);
               bJump = false;
        }
    }
    private void flip() //转身代码
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
