using UnityEngine;
using UnityEngine.AI;

public class NavMeshFollower : MonoBehaviour
{
    public GameObject target; // The GameObject the AI will follow
    private NavMeshAgent agent;

    void Start()
    {
        // Get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<NavMeshAgent>();

        // Check if the NavMeshAgent exists
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent is missing from this GameObject!");
        }

        // Check if a target is assigned
        if (target == null)
        {
            Debug.LogError("No target assigned for the AI to follow!");
        }
    }

    void Update()
    {
        // Update the AI's destination to the target's position
        if (target != null && agent != null)
        {
            agent.SetDestination(target.transform.position);
        }
    }
}
