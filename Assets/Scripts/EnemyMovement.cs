using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject Player;

    private CharacterStats stat;
    private Vector2 playerPos,direction;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float attackCD;
    bool inPlayer=false;
    PlayerStats playerStat;
    MutationManager mutationManager;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        playerStat = Player.GetComponent<PlayerStats>();
        mutationManager = Player.GetComponent<MutationManager>();
        stat = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackCD>0){
            attackCD-=Time.deltaTime;
        }
        while(inPlayer&&attackCD<=0){
            attackCD=1;
            //Debug.Log("enemy hit player");
            playerStat.TakeDamage(stat.AttackDamage);
            mutationManager.Mutate(1);
        }

        playerPos = Player.transform.position;
        direction = (playerPos - (Vector2)transform.position).normalized;
    }
    private void FixedUpdate()
    {
        if(!inPlayer){
            Movement(direction);
        }


        spriteRenderer.flipX = direction.x < 0 ;

    }

    void Movement(Vector2 direction)
    {
        if (!GetComponent<Animator>().GetBool("Running"))
        {
            return;
        }
        rb.MovePosition((Vector2)transform.position + direction * stat.MoveSpeed* Time.fixedDeltaTime);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            inPlayer = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            inPlayer = false;
        }
    }

    public void Hurt()
    {
        GetComponent<Animator>().SetBool("Running", false);
        GetComponent<Animator>().SetTrigger("Hurt");
        rb.velocity = direction * -1.5f;
    }
    void OnHurtEnd()
    {
        GetComponent<Animator>().SetBool("Running", true);
    }
}
