using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float exp;
    public float expToNextLevel=20;
    public int level;

    public float expMultiplier;
    UpgradesManager upgradesManager;

    PlayerStats stats;
    // Start is called before the first frame update
    private void Awake() {
        upgradesManager=GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        stats=gameObject.GetComponent<PlayerStats>();
    }
    public void GetXP(float amount){
        exp+=amount*(1+expMultiplier);
        if(exp>=expToNextLevel){
            exp-=expToNextLevel;
            expToNextLevel*=1.1f;
            level++;
            upgradesManager.LevelUp();
        }
    }
}
