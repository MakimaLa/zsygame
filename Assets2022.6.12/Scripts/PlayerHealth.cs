using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer healthBar;
    public float health = 100f;
    public float repeatDamagePeriod = 2f;
    public float hurtForce = 10f;
    public float damageAmount = 10f;
    public AudioClip[] ocheClips;
    public AudioSource audiosource;
    public AudioMixer audiomixer;

    private float lastHitTime;
    private Vector3 healthScale;
    private PlayerCtrl playerControl;
    private Animator anim;
    void Awake()
    {
        playerControl = GetComponent<PlayerCtrl>();
        healthBar = GameObject.Find("HealthBar").GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        healthScale = healthBar.transform.localScale;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //可以再次减血
            if (Time.time > lastHitTime + repeatDamagePeriod)
            {
                if (health > 0f)
                {
                    TakeDamage(col.transform);
                    lastHitTime = Time.time;
                }

            }
        }
    }
    void death()
    {
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer s in spr)
        {
            s.sortingLayerName = "UI";
        }

        //GetComponent<PlayerCtrl>().enabled = false;
        playerControl.enabled = false;
        GetComponentInChildren<Gun>().enabled = false;
        anim.SetTrigger("Die");

        //销毁血条
        GameObject go = GameObject.Find("UI_HealthBar");
        Destroy(go);
    }
    void TakeDamage(Transform enemy)
    {
        int i = Random.Range(0, ocheClips.Length);
        audiosource.clip = ocheClips[i];
        audiosource.Play();
        playerControl.bJump = false;
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
        GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
        health -= damageAmount;
        if (health <= 0)
        {
            death();
            // return;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }

}
