using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Waiting,
        Traveling,

    }
    
    State currentState;
    NavMeshAgent agent;

    public Transform[] destinationPoints;
    int destinationIndex;

    public Transform player;
    [SerializeField]
    float visionRange;
    [SerializeField]
    float attackRange;

    /*[SerializeField]
    float patrolRange = 10f; */

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Patrolling;
        //destinationIndex = Random.Range(0, destinationPoints.Length);
        

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case State.Patrolling:
                Patrol();
            break;

            case State.Chasing:
                Chase();
            break;

            case State.Attacking:
                Attack();
            break;

            case State.Waiting:
                Waite();
            break;

            /*case State.Traveling:
                Travel();
            break;*/

            default:
                Chase();
            break;
            
        }
    }
  
    void Patrol()
    {
        agent.destination = destinationPoints[destinationIndex].position;

        if(Vector3.Distance(transform.position, destinationPoints[destinationIndex].position)<1)
        {
            
            
            if(destinationIndex < destinationPoints.Length)
            {
                Debug.Log("Siguiente");
                destinationIndex += 1;
                currentState = State.Waiting;
            }
            if(destinationIndex == destinationPoints.Length)
            {
                destinationIndex = 0;     
            }
           
        }

        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            Debug.Log("Persiguiendo");
            currentState = State.Chasing;
        }
        
      
    }

    /*void Patrol()
    {
        Vector3 randomPosition;
        if(RandomPoint(transform.position, patrolRange, out randomPosition))
        {
            agent.destination = randomPosition;
        }

        if(Vector3.Distance(transform.position, player.position) < visionRange)
        {
            Debug.Log("Persiguiendo");
            currentState = State.Chasing;
        }

        currentState = State.Traveling
        
    }

    bool RandomPoint(vector3 center, float range, out Vector3 point)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 4, NavMesh.AllAreas)
        {
            point = hit.position;
            return true;
        }
        point = Vector3.zero;
        return false;
    }

    void Travel()
    {
        if(agent.remainingDistance <= 0.2)
        {
            currentState = State.Patrolling
        }
        if(Vector3.Distance(transform.position, player.position) < visionRange))
        {
            currentState = State.Chasing
        }
    }*/

    void Attack()
    {
        
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) > attackRange)
        {
            currentState = State.Chasing;
        }

        
    }

    void Chase()
    {
        agent.destination = player.position;

        if(Vector3.Distance(transform.position, player.position) > visionRange)
        {
            currentState = State.Patrolling;
        }
        
        if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            Debug.Log("Atacando");
            currentState = State.Attacking;
            
        }
        
    }

    void Waite()
    {
        //Invoke("EsperarTiempo", 3f);
        StartCoroutine("Esperar");
        Debug.Log("Esperando");
       
        
        
        
    }

    void OnDrawGizmos()
    {
        foreach (Transform point in destinationPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(point.position, 1);

        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    /*public void EsperarTiempo()
    {
        currentState = State.Patrolling;
    }*/
    IEnumerator Esperar()
    {
        
        yield return new WaitForSeconds (5);
        currentState = State.Patrolling;
          
    }

}
