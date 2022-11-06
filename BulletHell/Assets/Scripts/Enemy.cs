using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    Rigidbody2D body;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float moveSpeed, cooldown;
    float timer;
    [SerializeField]
    float seekRange;
    [SerializeField]
    float retreatRange;
    public behaviorState behavior;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.value * cooldown;
        behavior = behaviorState.Seeking;
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dist = (this.transform.position - FindObjectOfType<ShipController>().transform.position);
        if (behavior == behaviorState.Seeking)
        {
            this.transform.position += dist.normalized * Time.deltaTime * moveSpeed * -1;
            if (dist.magnitude <= seekRange && dist.magnitude >= retreatRange)
            {
                behavior = behaviorState.Set;
            }
            else if(dist.magnitude < retreatRange)
            {
                behavior = behaviorState.Retreating;
            }
        }
        else if (behavior == behaviorState.Retreating)
        {
            this.transform.position += dist.normalized * Time.deltaTime * moveSpeed;
            if (dist.magnitude <= seekRange && dist.magnitude >= retreatRange)
            {
                behavior = behaviorState.Set;
            }
            else if (dist.magnitude > seekRange)
            {
                behavior = behaviorState.Seeking;
            }
        }
        else if (behavior == behaviorState.Set)
        {
            if (dist.magnitude > seekRange)
            {
                behavior = behaviorState.Seeking;
            }
            else if (dist.magnitude < retreatRange)
            {
                behavior = behaviorState.Retreating;
            }
        }
        timer += Time.deltaTime;
        if(timer > cooldown)
        {
            timer = 0;
            GameObject b = Instantiate(bullet, this.transform.position + dist.normalized * -1, this.transform.rotation);
            b.GetComponent<Rigidbody2D>().velocity = dist.normalized * -1 * b.GetComponent<Bullet>().speed;
            b.GetComponent<Bullet>().owner = this.gameObject;
        }
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f), Mathf.Clamp(transform.position.y, minScreenBounds.y + 0.5f, maxScreenBounds.y - 0.5f), transform.position.z);

    }
    public enum behaviorState
    {
        Seeking,
        Set,
        Retreating
    }
}
