using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : Entity
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float cooldownBetweenShots;
    [SerializeField]
    GameObject leftHand, rightHand;
    GameObject left, right;
    Animator anim;
    float timer;

    void Start()
    {
        timer = Random.value * cooldownBetweenShots;
        left = Instantiate(leftHand, this.transform.position - new Vector3(2, 0, 0), this.transform.rotation);
        left.transform.parent = this.transform;
        right = Instantiate(rightHand, this.transform.position + new Vector3(2, 0, 0), this.transform.rotation);
        right.transform.parent = this.transform;
        anim = this.GetComponent<Animator>();
    }
    public override void DestroyThis()
    {
        SceneManager.LoadScene(0);
        Destroy(this.left.gameObject);
        Destroy(this.right.gameObject);
        Destroy(this.gameObject);
    }

    public override void OnDamage()
    {
        //Do Damage Animation
        anim.Play("Damage");
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
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cooldownBetweenShots)
        {
            timer = 0;
            Shoot();
        }
        
    }
}
