using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FilteredFlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if weights aren't given for each behavior OR weights are given for undefined behavior
        if (weights.Length != behaviors.Length)
        {
            Debug.Log("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        Vector3 move = Vector3.zero;
        

        // iterate through behaviors
        for(int i=0; i < behaviors.Length; i++)
        {
            Vector3 partialMove = behaviors[i].CalculateMove(agent, context, flock) * weights[i];
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

        return move;
    }


}
