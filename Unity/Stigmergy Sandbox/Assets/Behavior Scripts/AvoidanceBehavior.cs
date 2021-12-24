using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if there are no neighbors, then return no movement adjustments
        if (context.Count == 0)
            return Vector3.zero;

        // if neighbors do exist, then add the points and average
        Vector3 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        

        foreach (Transform item in filteredContext)
        {
            

            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += agent.transform.position - item.position;
            }
            
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
