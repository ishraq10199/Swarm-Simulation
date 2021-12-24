using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics")]
public class PhysicsFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        /*
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original)
        {
            if (!item.CompareTag("cell"))
                filtered.Add(item);

        }
        */
        return original;
    }
}
