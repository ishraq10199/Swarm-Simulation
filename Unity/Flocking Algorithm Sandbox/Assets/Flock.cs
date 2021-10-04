using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public int[,] map;
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(.1f, 5f)]
    public float obstacleAvoidanceRadius = 2f;

    [Range(10, 500)]
    public int startingCount = 250;

    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius;  } }


    // Start is called before the first frame update
    void Start()
    {
        //transform.localEulerAngles = new Vector3(90f, 0f, 0f);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        
        // replace with deploy function in Agent base class later on
        for(int i=0; i<startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                    agentPrefab,
                    Random.insideUnitSphere * startingCount * AgentDensity,
                    Quaternion.Euler(Vector3.forward),
                    transform
                );
            newAgent.name = "Agent " + i;

            // mark flock type for filterling later on
            newAgent.Initialize(this);
            Vector3 pos = newAgent.transform.localPosition;
            newAgent.transform.localPosition = new Vector3(pos.x, 0f, pos.z);
            agents.Add(newAgent);
        }
        //transform.localEulerAngles = new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);


            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            
            if (move.sqrMagnitude > squareMaxSpeed)
                move = move.normalized * maxSpeed;

            
            agent.Move(move);

        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;

    }
}
