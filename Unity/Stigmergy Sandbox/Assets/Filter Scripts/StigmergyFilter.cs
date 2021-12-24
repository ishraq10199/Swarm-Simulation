using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Allow Stigmergy")]
public class StigmergyFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        Transform currentMaxTransform = null;
        float currentMaxLevel = 0;

        foreach (Transform item in original)
        {
            if (item.gameObject.CompareTag("cell"))
            {
                Cell c = item.gameObject.GetComponent<Cell>();
                if (c.pheromone)
                    if(c.pheromone.getPheromoneType() == PheromoneType.Attractive)
                    {
                        if(c.pheromone.GetLevel() > currentMaxLevel)
                        {
                            currentMaxLevel = c.pheromone.GetLevel();
                            currentMaxTransform = item;
                        }
                    }
            }

        }
        if(currentMaxLevel != 0)
            filtered.Add(currentMaxTransform);
        return filtered;
    }
}
