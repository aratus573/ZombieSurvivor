using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public float damageReduction,maxHealthBeforePenalty;
    public float healRate;
    public int BowDamage;
    GameManager gameManager;

    MutationManager mutationManager;
    private void Awake() {
        mutationManager=gameObject.GetComponent<MutationManager>();
        maxHealthBeforePenalty=MaxHealth;
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update() {
        //health penalty
        MaxHealth=Mathf.RoundToInt(maxHealthBeforePenalty*(1-mutationManager.mutation*0.01f));
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }
    public override void TakeDamage(int damage)
    {
        damage=Mathf.RoundToInt(damage*(1-damageReduction));
        //Debug.Log("player TakeDamage");
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if(CurrentHealth<=0){
            PlayerDeath();
        }
        if (!GetComponent<Animator>().GetBool("Attacking")){
            GetComponent<Animator>().SetTrigger("Hurt");
        }
    }

    private void PlayerDeath(){
        gameManager.EndingGame();
    }

    public void Heal(int heal){
        heal*=Mathf.RoundToInt(1+healRate);
        CurrentHealth = Mathf.Min(CurrentHealth + heal, MaxHealth);
    }
}
