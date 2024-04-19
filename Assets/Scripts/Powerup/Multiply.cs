using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiply : MonoBehaviour
{
    [SerializeField] private MeshRenderer child;

    private void Start()
    {
        child = GetComponentInChildren<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            child.enabled = false;
        }
    }

    

    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
}
