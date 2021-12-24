using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FilteredFlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;
    private new readonly ContextFilter filter;
    public ContextFilter[] filters;

    public StigmergyFilter stigFilter;
    public StigmergyCohesionBehavior stigmergyCohesionBehavior;
    public float stigmergyWeight = 1f;
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if weights aren't given for each behavior OR weights are given for undefined behavior
        if (weights.Length != behaviors.Length)
        {
            Debug.Log("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        Vector3 move = Vector3.zero;
        HashSet<Transform> hset = new HashSet<Transform>();

        List<Transform> stigmergyContext = (stigFilter == null) ? context : stigFilter.Filter(agent, context);


        foreach (ContextFilter filter in filters)
        {
            List<Transform> temp =  ((filter == null) ? context : filter.Filter(agent, context));
            foreach(Transform t in temp)
            {
                hset.Add(t);
            }
        }

        List<Transform> filteredContext = hset.ToList<Transform>();


        // iterate through behaviors
        for (int i=0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, filteredContext, flock) * weights[i];
            if(partialMove != Vector3.zero)
            {
                if(partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        

        Vector3 stigMove = stigmergyCohesionBehavior.CalculateMove(agent, stigmergyContext, flock) * stigmergyWeight;
        if(stigMove != Vector3.zero)
        {
            if(stigMove.sqrMagnitude > stigmergyWeight * stigmergyWeight)
            {
                stigMove.Normalize();
                stigMove *= stigmergyWeight;
            }
            move += stigMove;
        }


        return move;
    }


}
