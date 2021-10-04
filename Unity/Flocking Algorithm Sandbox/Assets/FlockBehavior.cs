using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    // Start is called before the first frame update
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);

}
