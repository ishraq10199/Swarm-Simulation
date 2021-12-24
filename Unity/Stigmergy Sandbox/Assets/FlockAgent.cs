using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : Agent
{
    Flock agentFlock;
    public Cells cellServer;
    public Flock AgentFlock {  get { return agentFlock;  } }

    public Cell topLeftCell, topRightCell, bottomLeftCell, bottomRightCell, topCell, leftCell, bottomCell, rightCell, currentCell;

    public void setCurrentCell(Cell c, int direction)
    {
        currentCell = c;

        // if agent is facing north
        if (direction == 0)
        {
            topLeftCell = c.topLeft;
            topRightCell = c.topRight;
            bottomLeftCell = c.bottomLeft;
            bottomRightCell = c.bottomRight;
            topCell = c.top;
            leftCell = c.left;
            rightCell = c.right;
            bottomCell = c.bottom;
        }
        // if agent is facing west
        else if (direction == 1)
        {
            topLeftCell = c.bottomLeft;
            topRightCell = c.topLeft;
            bottomLeftCell = c.bottomRight;
            bottomRightCell = c.topRight;
            topCell = c.left;
            leftCell = c.bottom;
            rightCell = c.top;
            bottomCell = c.right;
        }
        // if agent is facing south
        else if (direction == 2)
        {
            topLeftCell = c.bottomRight;
            topRightCell = c.bottomLeft;
            bottomLeftCell = c.topRight;
            bottomRightCell = c.topLeft;
            topCell = c.bottom;
            leftCell = c.right;
            rightCell = c.left;
            bottomCell = c.top;
        }
        // if agent is facing east
        else
        {
            topLeftCell = c.topRight;
            topRightCell = c.bottomRight;
            bottomLeftCell = c.topLeft;
            bottomRightCell = c.bottomLeft;
            topCell = c.right;
            leftCell = c.top;
            rightCell = c.bottom;
            bottomCell = c.left;
        }

        // trail paint test

        //if (currentCell.pheromone) currentCell.pheromone.SetPheromone(PheromoneType.Repulsive, 10f);

        /*
        List<Cell> neighbours = new List<Cell> { topCell, rightCell, bottomCell, leftCell, topLeftCell, topRightCell, bottomLeftCell, bottomRightCell };
        foreach (Cell cell in neighbours)
        {
            if (cell)
            {
                // pheromone placement on target
                
                if (cell.neighbourContainsTarget || cell.containsTarget)
                    cell.pheromone.SetPheromone(PheromoneType.Attractive, 10f);

                // pheromone placement on existing pheromone-containing cell
                
                if (cell.pheromone)
                    if (cell.pheromone.GetLevel() > 0)
                        cell.pheromone.SetPheromone(cell.pheromone.getPheromoneType(), (10f-cell.pheromone.GetLevel())/2);

            }

        }

        */



    }

    public void ReleasePheromone(PheromoneType type, Cell location)
    {
        location.pheromone.SetPheromone(type, 10f);
    }

    Collider agentCollider;
    public Collider AgentCollider {  get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();      
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
        agentFlock.transform.parent = flock.transform;
    }


    public void Move(Vector3 velocity)
    {
        /*
        Quaternion rot = Quaternion.FromToRotation(transform.forward, velocity);
        transform.forward = velocity;
        transform.forward = Quaternion.Euler(-rot.x, 0, -rot.z) * velocity;
        transform.position += velocity * Time.deltaTime;
        */
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }





    // Update is called once per frame
    void Update()
    {
        
    }

    
}
