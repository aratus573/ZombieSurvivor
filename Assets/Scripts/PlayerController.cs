using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerStats playerStats;
    Animator animator;
    float horizontalInput;
    float verticalInput;
    Quaternion Rotation;
    AttackHitbox attackHitbox;
    float AttackKeyCD;
    Transform ArrowSpawn;
    public GameObject Arrow;
    private float objectWidth;
    private float objectHeight;



    void Awake()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();
        attackHitbox = transform.GetChild(0).GetComponent<AttackHitbox>();
        ArrowSpawn = transform.GetChild(1).transform;
    }
    void Start(){
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
    }

    void Update()
    {

        // Manage Input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if(AttackKeyCD > 0)
        {
            AttackKeyCD -= Time.deltaTime;
        }

        if (AttackKeyCD <= 0 && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)))
        {
            AttackKeyCD = 0.15f;
            animator.SetBool("Attacking", true);
            animator.SetTrigger("ChainAttack");
        }

        if(!animator.GetBool("Attacking") && (Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.Mouse1)))
        {
            animator.SetBool("Attacking", true);
            animator.SetTrigger("BowAttack");
        }

        if (horizontalInput < 0)
        {
            Rotation.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if(horizontalInput > 0)
        {
            Rotation.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        transform.rotation = Rotation;
        if (!animator.GetBool("Attacking") && (Math.Abs(horizontalInput) > 0 || Math.Abs(verticalInput) > 0))
        {
            transform.position += new Vector3(horizontalInput, verticalInput, 0).normalized * Time.deltaTime * playerStats.MoveSpeed;
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }



    }
    void LateUpdate(){
        //set player in boundary
        Vector3 Pos = transform.position;
        Pos.x = Mathf.Clamp(Pos.x, -8.7f, 8.7f);
        Pos.y = Mathf.Clamp(Pos.y, -4.7f, 4.7f);
        transform.position = Pos;
    }

    void Attack()
    {
        if (attackHitbox.TriggeredList.Count > 0)
        {
            AudioManager.Instance.PlaySFX("Hit");
        }
        else
        {
            AudioManager.Instance.PlaySFX("Miss");
        }
        for (int i = 0; i < attackHitbox.TriggeredList.Count; ++i)
        {   
            if (attackHitbox.TriggeredList[i] == null)
            {
                attackHitbox.TriggeredList.Remove(attackHitbox.TriggeredList[i]);
            }
            else
            {
            //Debug.Log("Player hit something");
                attackHitbox.TriggeredList[i].GetComponent<CharacterStats>().TakeDamage(playerStats.AttackDamage);
            }
        }
    }

    void Shoot()
    {
        AudioManager.Instance.PlaySFX("Fire");
        Vector3 arrowDirection;
        if(horizontalInput == 0 && verticalInput == 0)//no input
        {
            if(Rotation.eulerAngles == Vector3.zero)//face left
            {
                arrowDirection = new Vector3(-1, 0, 0);
            }
            else
            {
                arrowDirection = new Vector3(1, 0, 0);
            }
        }
        else
        {
            arrowDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
        }

        GameObject arrow = Instantiate(Arrow, ArrowSpawn.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().Damage = playerStats.BowDamage;
        arrow.GetComponent<Arrow>().Direction = arrowDirection;
        float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;
        arrow.transform.Rotate(0f, 0f, angle+90f);
    }

    void OnAttackEnd()
    {
        if (!animator.GetBool("ChainAttack"))
        {
            animator.SetBool("Attacking", false);
        }
    }

}
