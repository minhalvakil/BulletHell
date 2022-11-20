using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour
{
    public float health;
    protected float maxHealth;
    // Use this for initialization
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void damage(float d)
    {
        this.health -= d;
        FindObjectOfType<AudioManager>().Play("Hurt");
        OnDamage();
        if (this.health<=0)
        {
            this.DestroyThis();
        }
        
    }
    public abstract void DestroyThis();
    public abstract void OnDamage();
}
