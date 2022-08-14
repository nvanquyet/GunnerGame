using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Transform groundL;
    [SerializeField] private Transform groundR;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform posAttack;
    [SerializeField] private LayerMask m_whereIsGround;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Player_Bullet bullet;
    [SerializeField] private Slider slider;

    [SerializeField] private float blood = 20;
    [SerializeField] private float damage = 10;

    private float horizontalMove;
    private float ShootDelay = .75f;
    private bool m_isLanding;
    private bool m_isAttacking;
    private Rigidbody2D rb2d;
    private Animator anim;
    private bool m_facingLeft;

    public void endPanelAble()
    {
        this.endPanel.SetActive(true);
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void onLanding()
    {
        m_isLanding = false;
        if(Physics2D.Linecast(transform.position, groundCheck.position , 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundL.position, 1 << LayerMask.NameToLayer("Ground")) ||
           Physics2D.Linecast(transform.position, groundR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            m_isLanding = true;
        }

        anim.SetBool("Jump", !m_isLanding);
    }

    void Move()
    {
        //horizontalMove *= speed;
        Vector3 move = new Vector2(horizontalMove * 10f * Time.fixedDeltaTime, rb2d.velocity.y);
        if(horizontalMove > 0 && m_facingLeft)
        {
            Flip();
        }
        if(horizontalMove < 0 && !m_facingLeft)
        {
            Flip();
        }
        rb2d.velocity = move;
        anim.SetFloat("Run",Mathf.Abs(horizontalMove));
    }

    public void Jump()
    {
        if (m_isLanding)
        {
            rb2d.velocity = Vector2.up * jumpForce;
            anim.SetBool("Jump",true);
        }
    }

    public void Shoot()
    {
        if (!m_isAttacking)
        {
            m_isAttacking = true;
            RaycastHit2D hitInfo = Physics2D.Raycast(posAttack.position, posAttack.right);
            Player_Bullet m_bullet = Instantiate(bullet);
            m_bullet.direction(this.transform.rotation.y);
            m_bullet.setDamage(this.damage);
            m_bullet.transform.position = posAttack.position;
            m_bullet.GetComponent<Animator>().Play("Shoot");
            Debug.Log("Shoot");
            this.Invoke("ResetShoot", ShootDelay);
        }
    }

    void ResetShoot()
    {
        m_isAttacking = false;
    }

    private void Start()
    {
        slider.maxValue = blood;
        slider.value = blood;
    }
    public void Crounch()
    {
        if (m_isLanding)
        {
            Debug.Log("Crounch");
        }
    }

    private void FixedUpdate()
    {
        onLanding();
        Move();
        /*if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
        }
        if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && m_isLanding)
        {
            Crounch();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }*/
    }

    void Flip()
    {
        m_facingLeft = !m_facingLeft;
        transform.Rotate(0f, 180f, 0f);
    }


    public float getBlood()
    {
        return this.blood;
    }

    public float getDamage()
    {
        return this.damage;
    }

    public void setBlood(float blood)
    {
        this.blood = blood;
    }

    public void setDamage(float dame)
    {
        this.damage = dame;
    }
    /*
    public float getHorizontalMove()
    {
        return this.horizontalMove;
    }*/

    public void setHorizontalMove(float move)
    {
        this.horizontalMove = move;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            Enemy enemy = target.GetComponent<Enemy>();
            float damage = enemy.getDamage();
            Debug.Log("Damage enemy; " + damage);
            float hp = this.blood - damage; 
            if (hp > 0)
            {
                this.blood = hp;
            }
            else
            {
                this.endPanelAble();
            }
        }
    }

    public void setSliderValue() {
        slider.value = this.getBlood();
    }

}
