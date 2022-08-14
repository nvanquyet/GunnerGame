using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    private Vector2 m_Dir;
    private Rigidbody2D rb;
    private float speed = 10f;
    private Animator anim;
    private float timeToDes = 5f;
    [SerializeField] private float damage = 1.5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim.Play("Appear");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = m_Dir * speed;
    }

    public void setDir(Vector2 dir)
    {
        this.m_Dir = dir;
        this.m_Dir.Normalize();
        Destroy(gameObject, timeToDes);
    }

    public void setLocalScale(bool x)
    {
        Vector2 temp = this.transform.localScale;
        temp.x = Mathf.Abs(temp.x);
        if (!x)
        {
            temp.x = -temp.x;
        }
        this.transform.localScale = temp;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(target.gameObject.tag == "Player")
        {
            Player player = target.GetComponent<Player>();
            float blood = player.getBlood() - this.damage;
            Debug.Log("Mau player; " + blood);
            if (blood > 0)
            {
                player.setBlood(blood);
                player.setSliderValue();
            }
            else
            {
                player.endPanelAble();
            }
            Destroy(gameObject);
        }
    }
}
