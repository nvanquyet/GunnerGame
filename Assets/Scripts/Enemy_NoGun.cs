using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NoGun : MonoBehaviour
{
    [SerializeField] private Transform posAttack;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask m_whereIsPlayer;
    [SerializeField] private LayerMask m_whereIsGround;
    [SerializeField] private float damage = 1f;
    [SerializeField] private float blood = 20f;
    private float speed = 2f;
    private float m_speed;
    private float m_rageAttack = .5f;
    private float m_Range_CheckPlayer = 1f; //Radius to check attack zone
    private float posStart;
    private float posEnd;
    private float timeAttackDelay = 1f;
    private bool m_isAttack;
    private bool m_isGround;
    private bool m_facingRight;
    private const float m_Range_GroundCheck = .2f;                                  // Radius to check on ground
    private Animator animator;
    private Rigidbody2D rigid;
    private Vector2 dir;                                                             // Dir with player


    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_facingRight = this.transform.localScale.x > 0 ? true : false;
        m_speed = speed;
        posStart = this.transform.position.x - 1;
        posEnd = this.transform.position.x + 1;
    }
    private void Update()
    {
        m_speed -= Time.deltaTime / 5;
        if (m_speed < 0)
        {
            m_speed = speed;
        }
        animator.SetFloat("Run", m_speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onLanding();
        if (m_speed > 0.5f)
        {
            Move();
        }
        else
        {
            Debug.Log("Stop");
            rigid.velocity = Vector3.zero;
        }
        if (CheckPlayerInZoneAttack())
        {
            dircection();
            rigid.velocity = Vector3.zero;
            Attack();
        }
    }

    //Dir Bullet
    void dircection()
    {

        dir = GameObject.Find("Player").transform.position - this.transform.position;
    }

    void Move()
    {
        if (m_isGround)
        {
            if (m_facingRight)
            {
                rigid.velocity = Vector3.right * speed;
                if (this.transform.position.x > posEnd)
                {
                    Flip();
                }
            }
            else
            {
                rigid.velocity = Vector3.left * speed;
                if (this.transform.position.x < posStart)
                {
                    Flip();
                }
            }
        }
    }

    //Direction of enemy with player if player in attackzone
    void dir_with_Player()
    {
        if (this.transform.position.x > GameObject.Find("Player").transform.position.x)
        {
            if (m_facingRight)
            {
                Flip();
            }
        }
        else
        {
            if (!m_facingRight)
            {
                Flip();
            }
        }
    }

    //Check on Ground?
    void onLanding()
    {
        m_isGround = false;
        Collider2D[] collider = Physics2D.OverlapCircleAll(groundCheck.position, m_Range_GroundCheck, m_whereIsGround);
        if (collider.Length > 0)
        {
            m_isGround = true;
        }
    }

    //Check Player in attackzone
    bool CheckPlayerInZoneAttack()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(posAttack.position, m_Range_CheckPlayer, m_whereIsPlayer);
        if (collider.Length > 0)
        {
            return true;
        }
        return false;
    }

    void Attack()
    {
        dir_with_Player();
        if (!m_isAttack)
        {
            m_isAttack = true;
            animator.SetTrigger("Attack");
            Collider2D[] hit = Physics2D.OverlapCircleAll(posAttack.position, m_rageAttack, m_whereIsPlayer);
            if(hit.Length > 0)
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                float hp = player.getBlood() - this.damage;
                if(hp > 0)
                {
                    player.setBlood(hp);
                    Debug.Log("Máu player: " + player.getBlood());
                    player.setSliderValue();
                }
                else
                {
                    player.endPanelAble();
                }
            }
            this.Invoke("RestartAttack", timeAttackDelay);
        }
    }
    void RestartAttack()
    {
        m_isAttack = false;
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_facingRight = !m_facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
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
}
