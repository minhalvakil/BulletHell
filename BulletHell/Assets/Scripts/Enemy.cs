using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    Rigidbody2D body;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float moveSpeed, cooldown, wanderTime;
    float timer;
    float wanderTimer;
    [SerializeField]
    float seekRange;
    public behaviorState behavior;
    public EnemyType enemyType;
    

    // Start is called before the first frame update
    void Start()
    {
        timer = Random.value * cooldown;
        behavior = behaviorState.Approach;
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        timer += Time.deltaTime;
        if(timer > cooldown)
        {
            timer = 0;
            Shoot();
        }
        ClampToScreen();
    }
    void Move()
    {
        Vector3 dist = (-this.transform.position + FindObjectOfType<ShipController>().transform.position);
        switch (behavior)
        {
            case behaviorState.Approach:
                this.body.velocity = dist.normalized * moveSpeed;
                break;
            case behaviorState.Circle:
                this.body.velocity = Vector3.Cross(dist, Vector3.forward).normalized * moveSpeed;
                break;
            case behaviorState.Wander:
                if (wanderTimer > wanderTime)
                {
                    this.body.velocity = Random.insideUnitCircle.normalized * moveSpeed;
                    wanderTimer = 0;
                }
                wanderTimer += Time.deltaTime;
                break;
        }
        float angle = (Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg) +90f;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        CheckState();
    }
    void CheckState()
    {
        if ((this.transform.position - FindObjectOfType<ShipController>().transform.position).magnitude < seekRange)
        {
            switch (enemyType)
            {
                case EnemyType.Standard:
                    behavior = behaviorState.Wander;
                    break;
                default:
                    behavior = behaviorState.Circle;
                    break;
            }
        }
        else
        {
            behavior = behaviorState.Approach;
        }
        
    }
    void Shoot()
    {
        Vector3 dist = (-this.transform.position + FindObjectOfType<ShipController>().transform.position);
        float angle = (Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg)+180f;
        GameObject b = Instantiate(bullet, this.transform.position + dist.normalized, Quaternion.AngleAxis(angle, Vector3.forward));
       b.GetComponent<Rigidbody2D>().velocity = dist.normalized * b.GetComponent<Bullet>().speed;
       b.GetComponent<Bullet>().ownerTag = this.gameObject.tag;
    }
    void ClampToScreen()
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minScreenBounds.x + 0.5f, maxScreenBounds.x - 0.5f), Mathf.Clamp(transform.position.y, minScreenBounds.y + 0.5f, maxScreenBounds.y - 0.5f), transform.position.z);
    }

    public override void DestroyThis()
    {
        Destroy(this.gameObject);
    }
    public override void OnDamage()
    {

    }
    public enum behaviorState
    {
        Approach,
        Circle,
        Wander
    }
    public enum EnemyType
    {
        Standard,
        Slash,
        Homing
    }
}
