using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public NavMeshAgent agent;

    // For patroling
    public Transform Player;
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // For attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    // State
    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool playerInAttackRange;

    // LayerMasks to define what is considered the player and ground
    public LayerMask whatIsPlayer;  // The player layer
    public LayerMask whatIsGround;  // The ground layer

    // Start is called before the first frame update
    void Awake()
    {
        // Ensure player is properly assigned
        Player = FindObjectOfType<FirstPersonController>().transform; // Ensure it's using Transform for position
        if (Player != null)
        {
            Debug.Log("Player found: " + Player.name);
        }
        else
        {
            Debug.LogError("Player not found! Make sure the player has a FirstPersonController component.");
        }

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is in sight or attack range using the correct range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        // State-based behavior: patrol, chase, attack
        if (!playerInSightRange && !playerInAttackRange)
            Patroling();
        if (playerInSightRange && !playerInAttackRange)
            ChasePlayer();
        if (playerInSightRange && playerInAttackRange)
            AttackPlayer();
    }

    // Patrol behavior: move to random walk point
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        // Check if walk point is reached
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    // Search for a random walk point within range
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        // Check if the walk point is valid (on the ground)
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    // Chase the player when in sight range but not attack range
    private void ChasePlayer()
    {
        agent.SetDestination(Player.position);  // Use Player.position instead of Player.transform
    }

    // Attack the player when in attack range
    private void AttackPlayer()
    {
        // Stop the agent from moving during the attack
        agent.isStopped = true;

        // Ensure that Player is not null before trying to look at the player
        if (Player != null)
        {
            transform.LookAt(Player);  // Use Player (Transform) directly instead of Player.transform
        }
        else
        {
            Debug.LogError("Player reference is null, cannot look at player!");
        }

        if (!alreadyAttacked)
        {
            // Attack: instantiate projectile and apply force
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            alreadyAttacked = true;

            // Reset attack after a delay
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    // Reset the attack so it can be performed again
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}


// using System.Collections;
// using System.Collections.Generic;
// using StarterAssets;
// using UnityEngine;
// using UnityEngine.AI;

// public class Robot : MonoBehaviour
// {
//     FirstPersonController player;
//     NavMeshAgent agent;
//     //for shooting range
//         public Vector3 walkPoint;
//     bool walkPointSet;
//     public float walkPointRange;

//     //atack
//     public float timeBetweenAttacks;
//     bool alreadyAttacked;
//     public GameObject projectile;

//     //state
//     public float sightRange;
//     public float attackRange;
//     public bool playerInSightRange;
//      public bool playerInAttackRange;
    

//     // Start is called before the first frame update
//     void Awake()
//     {
//         player = GameObject.Find("Player").transform;
//         agent = GetComponent<NavMeshAgent>(); 

//     }

//     // Update is called once per frame
//     void Start()
//     {
//         player = FindAnyObjectByType<FirstPersonController>();
//     }
//      void Update()
//     {
//                 agent.SetDestination(player.transform.position);
//                 //chec for sight and attack range
//                 playerInSightRange = Physics.checkSphere(transform.position, sightRange, whatIsPlayer);
//                 playerInAttackRange = Physics.checkSphere(transform.position, sightRange, whatIsPlayer);

//                 if (!playerInSightRange && !playerInAttackRange) Patroling();
//                 if (playerInSightRange && !playerInAttackRange) chasePlayer();
//                 if (playerInSightRange && playerInAttackRange) AttackPlayer();

            
//     }
    
//     private void Patroling(){
//      if(!walkPointSet) SearchWalkPoint();
//      if (walkPointSet)
//      agent.SetDestination(walkPoint);

//      Vector3 distanceToWalkPoint = transform.position - walkPoint;

//      //walkpoint وصل
//      if (distanceToWalkPoint.magnitude <1f)
//      walkPointSet = false;

//     }
//     //calculate random point in range
//     private void SearchWalkPoint(){
//         float randomZ = Random.Range(-walkPointRange, walkPointRange);
//         float randomX = Random.Range(-walkPointRange, walkPointRange);
// //y رح يقعد نفسه
//         walkPoint = new Vector3(transform.position.x + randomX, transform.position.y , transform.position.z + randomZ);
        
//         if (PhysicsScene.Raycast(walkPoint, -transform.up, 2f, whatIsGroundo))
//         walkPointSet =true;
//     }
//     private void chasePlayer(){
//         agent.SetDestination(player.position);
//     }
    
//     private void AttackPlayer(){
//         //يوقف الانمي انه يتحرك
//         agent.SetDestination(transform.position);
//         transform.LookAt(player);

// if(!alreadyAttacked)
// {
//     //Attack code here
//     Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
//     rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
//      rb.AddForce(transform.up * 8f, ForceMode.Impulse);
//     alreadyAttacked =true;
//     Invoke(nameof(RasetAttack), timeBetweenAttacks);
// }
// }

// private void RasetAttack()
// {
//     alreadyAttacked = false;
// }

// }