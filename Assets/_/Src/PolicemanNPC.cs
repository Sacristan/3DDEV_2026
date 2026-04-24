using UnityEngine;
using UnityEngine.AI;

public class PolicemanNPC : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Player _player;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = FindAnyObjectByType<Player>();
        
        _agent.SetDestination(_player.transform.position);
    }

}
