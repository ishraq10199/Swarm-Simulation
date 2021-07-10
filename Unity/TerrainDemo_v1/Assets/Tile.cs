using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    bool isObstacle;
    bool hasTarget;

    bool hasPheromone;

    Pheromone pheromone;

    public void setAsObstacle()
    {
        isObstacle = true;
    }

    public void setAsTarget()
    {
        hasTarget = true;
    }

}
