using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationManager : MonoBehaviour
{
    public float mutation;
    public int mutationLevel;
    public float mutationMultiplier;
    UpgradesManager mutateManager;
    GameManager gameManager;
    PlayerStats stat;
    private void Awake() {
        stat=gameObject.GetComponent<PlayerStats>();
        mutateManager=GameObject.Find("MutationsManager").GetComponent<UpgradesManager>();
        gameManager=GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Mutate(int mutate){
        mutation+=mutate*(1+mutationMultiplier);
        if(mutation>=(mutationLevel+1)*25){
            mutationLevel++;
            if(mutationLevel==4){
                gameManager.EndingGame();
            }
            else{
                mutateManager.LevelUp();
            }
        }
    }
    public void HealMutate(int heal){
        mutation = Mathf.Max(mutationLevel-heal, mutationLevel*25);
    }
}
