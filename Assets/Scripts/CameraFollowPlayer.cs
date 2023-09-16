using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 offset;
    private void LateUpdate() {
        transform.position=player.transform.position+offset;
    }
}
