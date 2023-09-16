using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    //used by all pickup items(xp,medicine...)
    [SerializeField] PickUpType type;
    GameObject Player;
    PlayerStats stat;
    LevelManager levelManager;
    MutationManager mutationManager;
    enum PickUpType
    {
        Exp,
        Meds,
        Vaccine,
    }
    private void Awake() {
        Player=GameObject.Find("Player");
        stat=Player.GetComponent<PlayerStats>();
        levelManager=Player.GetComponent<LevelManager>();
        mutationManager=Player.GetComponent<MutationManager>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Player"){
            Debug.Log("player touch"+type.ToString());
            switch(type.ToString()){
                case "Exp":
                    //Debug.Log("Player get xp");
                    levelManager.GetXP(1);
                    break;
                case "Meds":
                    //Debug.Log("Player get meds");
                    stat.Heal(Mathf.RoundToInt(stat.MaxHealth*0.2f));
                    break;
                case "Vaccine":
                    //Debug.Log("Player get vaccine");
                    mutationManager.HealMutate(15);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
