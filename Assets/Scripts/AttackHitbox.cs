using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public List<GameObject> TriggeredList  = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!TriggeredList.Contains(col.gameObject) && col.gameObject.CompareTag("Enemy"))
        {
            TriggeredList.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        TriggeredList.Remove(col.gameObject);
    }


}
