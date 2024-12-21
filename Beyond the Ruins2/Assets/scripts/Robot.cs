using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Start()
    {
        agent.SetDestination(target.position);
    }
     void Update()
    {
        
    }
}
