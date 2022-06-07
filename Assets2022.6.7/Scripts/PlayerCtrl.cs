//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public float MoveForce = 100.0f;
    public float MaxSpeed = 5;
    public Rigidbody2D HeroBody;
    [HideInInspector]
    public bool bFaceRight = true;
    [HideInInspector]
    public bool bJump = false;
    public float JumpForce = 100; 
    public AudioClip[] JumpClips;
    public AudioSource audiosource;
    public AudioMixer audiomixer;
    private Transform mGroundCheck;
    Animator anim;

    float mvolume = 0;
    void Start()
    {
        HeroBody = GetComponent<Rigidbody2D>();
        mGroundCheck = transform.Find("GroundCheck");
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (Mathf.Abs(HeroBody.velocity.x) < MaxSpeed)
        {
            HeroBody.AddForce(Vector2.right * h * MoveForce);
        }

        if (Mathf.Abs(HeroBody.velocity.x) > 5)
        {
            HeroBody.velocity = new Vector2(Mathf.Sign(HeroBody.velocity.x) * MaxSpeed,
                                            HeroBody.velocity.y);
        }

        anim.SetFloat("speed", Mathf.Abs(h));

        if (h > 0 && !bFaceRight)
        {
            flip();
        }
        else if (h < 0 && bFaceRight)
        {
            flip();
        }

        if (Physics2D.Linecast(transform.position, mGroundCheck.position,
                                1 << LayerMask.NameToLayer("Ground")))
        {
            if (Input.GetButtonDown("Jump"))
            {
                bJump = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            mvolume++;
            audiomixer.SetFloat("MasterVolume", mvolume);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            mvolume--;
            audiomixer.SetFloat("MasterVolume", mvolume);
        }
        if (bJump)
        {
            int i = Random.Range(0,JumpClips.Length);
            //AudioSource.PlayClipAtPoint(JumpClips[i],transform.position);
            audiosource.clip = JumpClips[i];
            audiosource.Play();
            HeroBody.AddForce(Vector2.up * JumpForce);
            bJump = false;
            anim.SetTrigger("jump");
        }
    }

    private void flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
