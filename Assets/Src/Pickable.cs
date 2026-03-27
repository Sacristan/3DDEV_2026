using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    // void OnCollisionEnter(Collision collision)
    // {
    //     Debug.Log($"{nameof(Pickable)} collided with {collision.gameObject.name}", collision.gameObject);
    // }

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log($"{nameof(Pickable)} trigger zone entered by {other.gameObject.name}", other.gameObject);
        // if (other.gameObject.CompareTag("Player"))

        if (other.gameObject.TryGetComponent<Player>(out var player))
        {
            PickedUpByPlayer();
        }
    }

    void PickedUpByPlayer()
    {
        // FindAnyObjectByType<GameManager>().OnPickableCollected(this);
        GameManager.instance.OnPickableCollected(this);
        Destroy(gameObject);
    }
}