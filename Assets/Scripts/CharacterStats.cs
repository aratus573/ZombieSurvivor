using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shared by player and enemy

public class CharacterStats : MonoBehaviour
{

    //basic stats
    public int MaxHealth;
    public int CurrentHealth;
    public float MoveSpeed;

    public float deathDelay = 0.5f;

    //split to WeaponStats ?
    public int AttackDamage;

    public GameObject expLoot,medsLoot,vaccineLoot;

    public void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if(CurrentHealth==0){
            CharacterDeath();
        }
        else if(this.tag == "Enemy")
        {
            GetComponent<EnemyMovement>().Hurt();
        }
    }
    void CharacterDeath(){
        //play death animation;
        //drop loot;
        this.GetComponent<Collider2D>().enabled = false;
        if (this.tag=="Enemy"){
            DropLoot();
            EnemyAnimation enemyAnimation = GetComponent<EnemyAnimation>();
            enemyAnimation.isDead = true;
            Destroy(gameObject,deathDelay);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void DropLoot(){
        Instantiate(expLoot,gameObject.transform.position,Quaternion.identity);
        int loot=Random.Range(0,100);
        if(loot<6){
            Instantiate(medsLoot,gameObject.transform.position,Quaternion.identity);
        }
        else if(loot<11){
            Instantiate(vaccineLoot,gameObject.transform.position,Quaternion.identity);
        }
    }

}
