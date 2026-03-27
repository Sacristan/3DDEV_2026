using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    void Start()
    {
        Debug.Log($"{nameof(Pickable)} START @ {gameObject.name}", gameObject);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"{nameof(Pickable)} collided with {collision.gameObject.name}", collision.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{nameof(Pickable)} trigger zone entered by {other.gameObject.name}", other.gameObject);

        // if (other.gameObject.CompareTag("Player"))
        // {
        // }
        
        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            Destroy(gameObject);
        }
        

    }

}
