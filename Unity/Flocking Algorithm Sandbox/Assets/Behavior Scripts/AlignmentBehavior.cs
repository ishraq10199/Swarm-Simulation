using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public ContextFilter ignoranceFilter;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if there are no neighbors, then maintain current alignment
        if (context.Count == 0)
            return agent.transform.forward;

        // if neighbors do exist, then add the directions and average
        Vector3 alignmentMove = Vector3.zero;

        // filter flock type
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //filteredContext = (ignoranceFilter == null) ? context : ignoranceFilter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            // sum of directions
            alignmentMove += item.transform.forward;
        }
        // avg operation
        alignmentMove /= context.Count;

        return alignmentMove;
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
