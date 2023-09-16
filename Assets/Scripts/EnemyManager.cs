using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject EnemyPrefab;
    [SerializeField] float spawnRate=0.5f;
    [SerializeField] float enemyStatMultiplier=1;
    [SerializeField]float time=0;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
    void Update(){
        time+=Time.deltaTime;
        if(time>=60){
            time=0;
            IncreaseDifficulty();
        }
    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Vector3 spawnPoint=Random.onUnitSphere*10;
            //check if spawnpoint is near player
            Vector3 spawnPoint = new Vector3(Random.Range(-9f,9f),Random.Range(-5f,5f),0);
            while(Vector3.Distance(player.transform.position,spawnPoint)<3){
                spawnPoint = new Vector3(Random.Range(-9f,9f),Random.Range(-5f,5f),0);
            }

            GameObject enemy = Instantiate(EnemyPrefab,spawnPoint,Quaternion.identity);
            CharacterStats stat=enemy.GetComponent<CharacterStats>();
            stat.MaxHealth=Mathf.RoundToInt(stat.MaxHealth*enemyStatMultiplier);
            stat.CurrentHealth=stat.MaxHealth;
            stat.AttackDamage=Mathf.RoundToInt(stat.AttackDamage*enemyStatMultiplier);
            yield return new WaitForSeconds(1/spawnRate);
        }
    }
    void IncreaseDifficulty()
    {
        enemyStatMultiplier*=1.15f;
        spawnRate*=1.1f;
            
    }
}
