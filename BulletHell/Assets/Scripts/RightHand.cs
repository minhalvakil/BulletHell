using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : Entity
{
    [SerializeField]
    float timeBetweenAttacks;
    [SerializeField]
    float shootCooldown;
    [SerializeField]
    GameObject bullet;
    bool attacking;
    float timer;
    [SerializeField]
    float laserSpeed;
    [SerializeField]
    GameObject laserHelper;
    GameObject laser;
    float transformTimer;
    public override void DestroyThis()
    {
    }

    public override void OnDamage()
    {
    }
    /*void ShootLaser()
    {
        attacking = true;
        Vector3 dist = (-this.transform.position + FindObjectOfType<ShipController>().transform.position);
        float angle = (Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg) + 180f;
        laser = Instantiate(laserHelper, this.transform.position + dist.normalized, Quaternion.AngleAxis(angle, Vector3.forward));
        laser.GetComponent<Rigidbody2D>().velocity = dist.normalized * laserSpeed;
    }*/

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
        timer = Random.value * shootCooldown;
        transformTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
       /* if (!attacking)
        {
            transformTimer += Time.deltaTime;
            if(transformTimer> timeBetweenAttacks)
            {
                transformTimer = 0;
                ShootLaser();
            }
        }*/
        timer += Time.deltaTime;
        if(!attacking && timer > shootCooldown)
        {
            timer = 0;
            Shoot();
        }
    }
}
