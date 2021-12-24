using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPheromoneMemoryNode
{
    public ulong timestamp;
    public PheromoneType type;
    public int level;

    // Copy constructor, if required
    public AgentPheromoneMemoryNode(AgentPheromoneMemoryNode prev)
    {
        this.timestamp = prev.timestamp;
        this.type = prev.type;
        this.level = prev.level;
    }
}
