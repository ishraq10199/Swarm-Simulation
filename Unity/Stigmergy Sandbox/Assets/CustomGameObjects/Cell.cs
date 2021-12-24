using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * TODO:   Pheromone rendering
 * 
 *         Process: 
 *         i)   create static LUT of colors for rendering phermone
 *         ii)  use update function to render colors
 *         
 * 
 * TODO:   Public Utility Functions for Querying
 *         
 *         Process:
 *         i)   create pheromone class
 *         ii)  create pheromone object as Cell class property
 *         iii) ... 
 * 
 */



public class Cell : MonoBehaviour
{
    public Vector2Int cellPosition;
    public Cell topLeft, topRight, bottomLeft, bottomRight, top, left, bottom, right;
    public bool containsTarget;
    int id = 0;
    private static bool showPheromoneAsColor;
    public float previousPheromoneLevel;
    public bool neighbourContainsTarget;

    public Vector3 COMPONENTSTUFF;
    public float evaulatedLevel;



    public float GetPreviousPheromoneLevel() { return this.previousPheromoneLevel; }
    // Start is called before the first frame update
    public Pheromone pheromone;

    public static void EnablePheromoneColor(bool val = true) { showPheromoneAsColor = val; }


    private void OnTriggerEnter(Collider other)
    {
        Vector3 localAngles = other.transform.localEulerAngles;
        float y = localAngles.y;
        // direction (dir) can be north = 0, west = 1, south = 2, east = 3
        int dir;

        if (y >= 135.0f && y < 225.0f) dir = 1;
        else if (y >= 225.0f && y < 315.0f) dir = 2;
        else if (y >= 315.0f) dir = 3;
        else dir = 0;

        FlockAgent fa = other.gameObject.GetComponent<FlockAgent>();
        if(fa!=null) fa.setCurrentCell(this, dir);

        if (this.containsTarget) other.gameObject.GetComponent<FlockAgent>().ReleasePheromone(PheromoneType.Attractive, this);
    }
    private void OnTriggerExit(Collider other)
    {
        

    }

    public void notifyNeighbours()
    {
        foreach (Cell cell in new List<Cell> { top, right, bottom, left, topLeft, topRight, bottomLeft, bottomRight })
        {
            if (cell != null) cell.neighbourContainsTarget = true;
        }
    }

    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f);
        pheromone = gameObject.AddComponent<Pheromone>();
        pheromone.SetPheromone(PheromoneType.None, 0);
        previousPheromoneLevel = 0;
        
        //neighbourContainsTarget = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (showPheromoneAsColor)
        {
            if(this.pheromone.level > 0)
            {
                if(this.pheromone.type == PheromoneType.Attractive)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.Lerp(new Color(0.8f, 0.8f, 0.8f), Color.green, this.pheromone.level / 10f);
                }
                else if(this.pheromone.type == PheromoneType.Repulsive)
                {
                    gameObject.GetComponent<Renderer>().material.color = Color.Lerp(new Color(0.8f, 0.8f, 0.8f), Color.red, this.pheromone.level / 10f);
                }
                else
                {
                    this.pheromone.type = PheromoneType.Attractive;
                }
            }
            if(this.pheromone.level < 0.09f)
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0.8f, 0.8f, 0.8f);
            }

        }
    }



}
