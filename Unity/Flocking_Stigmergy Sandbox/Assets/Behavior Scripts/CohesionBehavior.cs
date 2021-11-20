using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if there are no neighbors, then return no movement adjustments
        if (context.Count == 0)
            return Vector3.zero;

        // if neighbors do exist, then add the points and average
        Vector3 cohesionMove = Vector3.zero;
        // filter flock type
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            // sum of points
            cohesionMove += item.position;
        }
        // avg operation
        cohesionMove /= context.Count;
        cohesionMove -= agent.transform.position;
        return cohesionMove;
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
