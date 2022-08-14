using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Bullet : MonoBehaviour
{
    private float timeToDestroyBullet = 2f;
    private Rigidbody2D rb;
    private float speed = 100f;
    private int dir;
    private float damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * dir * speed;  
    }

    public void direction(float direct)
    {
        if(direct > 0)
        {
            dir = 1;
        }
        else if(direct < 0) 
        { 
            dir = -1;
        }
        Destroy(gameObject, timeToDestroyBullet);
    }
    public void setDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Enemy")
        {
            Enemy enemy = target.GetComponent<Enemy>();
            float blood = enemy.getBlood() - this.damage;
            Debug.Log("Mau enemy; " + blood);
            if (blood > 0)
            {
                enemy.setBlood(blood);
            }
            else
            {
                Destroy(enemy.gameObject);
            }
            Destroy(gameObject);
        }
        if (target.gameObject.tag == "Enemy_NoGun")
        {
            Enemy_NoGun enemy = target.GetComponent<Enemy_NoGun>();
            float blood = enemy.getBlood() - this.damage;
            Debug.Log("Mau enemy; " + blood);
            if (blood > 0)
            {
                enemy.setBlood(blood);
            }
            else
            {
                Destroy(enemy.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
