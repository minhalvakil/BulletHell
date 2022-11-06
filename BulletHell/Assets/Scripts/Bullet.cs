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
    public GameObject owner;

    
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != this.owner.tag)
        {
            collision.gameObject.GetComponent<Entity>().damage(this.damage);
            Destroy(this.gameObject);
        }
    }
}
