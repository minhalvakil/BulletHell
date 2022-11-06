using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : Entity
{
    // Start is called before the first frame update
    [SerializeField] float speed = 5f;
    [SerializeField] float braking = 0f;
    [SerializeField] GameObject bullet;
    [SerializeField] Text healthText;
    float h, v;
    Rigidbody2D body;
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        healthText.text = string.Format("Health: {0}", health);

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
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f), Mathf.Clamp(transform.position.y, minScreenBounds.y + 0.5f, maxScreenBounds.y - 0.5f), transform.position.z);
        healthText.text = string.Format("Health: {0}", health);

    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(h * speed, v * speed);
    }

}
