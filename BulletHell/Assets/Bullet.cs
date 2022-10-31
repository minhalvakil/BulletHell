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
        
    }

    // Update is called once per frame
    void Update()
    {
        if((this.transform.position - owner.transform.position).magnitude > 50f)
        {
            Destroy(this.gameObject);
        }
    }
}
