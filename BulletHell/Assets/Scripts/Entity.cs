using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    public float health;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void damage(float d)
    {
        this.health -= d;
        FindObjectOfType<AudioManager>().Play("Hurt");
        if (this.health<=0)
        {
            Destroy(this.gameObject);
        }
        
    }
}
