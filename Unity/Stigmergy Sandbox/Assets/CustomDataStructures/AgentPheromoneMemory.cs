using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AgentPheromoneMemory: Dictionary<Tuple<int, int>, AgentPheromoneMemoryNode>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"> Tuple of (x,y) coordinates of a cell </param>
    /// <param name="value"></param>
    /// <returns>Most updated AgentPheromoneMemoryNode gets returned. VITAL USE.</returns>
    public new AgentPheromoneMemoryNode Add(Tuple<int, int> key, AgentPheromoneMemoryNode value)
    {
        if (this.ContainsKey(key))
        {
            // current dictionary has old value - Update
            if (this[key].timestamp < value.timestamp)
                base.Add(key, value);
        }
        // blind add to dictionary if current memory doesn't have data on particular cell
        else
            base.Add(key, value);

        // Return the AgentPheromoneMemoryNode that has updated timestamp and attributes
        return this[key];

    }

}
