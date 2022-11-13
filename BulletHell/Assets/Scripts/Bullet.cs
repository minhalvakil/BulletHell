using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField]
    float damage;
    [SerializeField]
    public float speed;
    public string ownerTag;
    Animator animator;
    bool active;

    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        FindObjectOfType<AudioManager>().Play("Shoot");
        animator = this.GetComponent<Animator>();
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        if (this.transform.position.x < minScreenBounds.x || this.transform.position.y < minScreenBounds.y || this.transform.position.x > maxScreenBounds.x || this.transform.position.y > maxScreenBounds.y)
        {
            Destroy(this.gameObject);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(active && collision.gameObject.tag != ownerTag)
        {
            collision.gameObject.GetComponent<Entity>().damage(this.damage);
            active = false;
            body.velocity *= 0.1f;
            animator.SetBool("HasHit", true);
        }
    }
}
