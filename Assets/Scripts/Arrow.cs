using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int Damage;
    public int Speed;
    public Vector3 Direction;
    Animator animator;
    bool stop = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        if (!stop)
        {
            transform.position += Direction * Time.deltaTime * Speed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySFX("ArrowHit");
            Debug.Log("ArrowHit");
            animator.SetTrigger("Hit");
            col.GetComponent<CharacterStats>().TakeDamage(Damage);
            stop = true;
            this.GetComponent<Collider2D>().enabled = false;
        }
        // else if ( hit wall ) destroy(gameObject)
    }

    void OnAnimEnd()
    {
        Destroy(gameObject);
    }
}
