using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    Transform player;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
        }
    }

    void TakeDamage(int damageCount)
    {
        health-=damageCount;
        if (health<=0)
        {
            Destroy(gameObject);
        }
    }
}
