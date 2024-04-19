using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxManager : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset;

        transform.position = new Vector3(transform.position.x, transform.position.y, desiredPosition.z); ;
    }

    private void Start()
    {
        
    }
}
