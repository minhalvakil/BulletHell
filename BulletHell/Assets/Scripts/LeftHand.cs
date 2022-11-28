using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : Entity
{
    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    float shootCooldown;
    [SerializeField]
    GameObject bullet;
    bool attacking;
    bool returning;
    float timer;
    float transformTimer;
    [SerializeField]
    float meleeDamage;
    [SerializeField]
    float speed;
    Rigidbody2D rb;
    Vector3 originalPosition;
    Animator anim;
    public override void DestroyThis()
    {
    }

    public override void OnDamage()
    {
    }

    void launchAttack()
    {
        originalPosition = this.transform.position;
        attacking = true;
        anim.SetBool("Attacking", attacking);
        returning = false;
        Vector3 dist = (-this.transform.position + FindObjectOfType<ShipController>().transform.position);
        this.rb.velocity = dist.normalized * speed;
    }
    void Shoot()
    {
        Vector3 dist = (-this.transform.position + FindObjectOfType<ShipController>().transform.position);
        float angle = (Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg) + 180f;
        GameObject b = Instantiate(bullet, this.transform.position + dist.normalized, Quaternion.AngleAxis(angle, Vector3.forward));
        b.GetComponent<Rigidbody2D>().velocity = dist.normalized * b.GetComponent<Bullet>().speed;
        b.GetComponent<Bullet>().ownerTag = this.gameObject.tag;
    }
    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        returning = false;
        timer = Random.value * shootCooldown;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(!attacking)
        {
           transformTimer += Time.deltaTime;
        }
        if (returning)
        {
            if ((this.transform.position - originalPosition).magnitude < 0.1f)
            {
                rb.velocity = Vector3.zero;
                attacking = false;
                anim.SetBool("Attacking", attacking);
                returning = false;
            }
        }
        if(transformTimer > timeBetweenAttacks)
        {
            transformTimer = 0;
            launchAttack();
        }
        if (!attacking && timer > shootCooldown)
        {
            timer = 0;
            Shoot();
        }

    }
    private void OnBecameInvisible()
    {
        if(attacking)
        {
            rb.velocity *= -1;
            returning = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (attacking && collision.gameObject.tag != this.tag)
        {
            collision.gameObject.GetComponent<Entity>().damage(this.meleeDamage);
        }
    }
}
