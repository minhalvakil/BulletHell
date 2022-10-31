using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 5f;
    [SerializeField] float braking = 0f;
    [SerializeField] GameObject bullet;
    float h, v;
    Rigidbody2D body;
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 direction = this.transform.position - Camera.main.ScreenToWorldPoint (Input.mousePosition);
            direction.z = 0;
            GameObject b = Instantiate(bullet, this.transform.position + direction.normalized *-1, this.transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = direction.normalized * -1 * b.GetComponent<Bullet>().speed;
            b.GetComponent<Bullet>().owner = this.gameObject;
        }
        
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(h * speed, v * speed);
    }

}
