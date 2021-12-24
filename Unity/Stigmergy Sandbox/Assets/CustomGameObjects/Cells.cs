using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferPheromone
{
    public float level { get; set; }
    public PheromoneType type { get; set; }

    public BufferPheromone(Pheromone p)
    {
        this.level = p.level;
        this.type = p.type;
    }
    
    public void SetPheromone(PheromoneType type, float level)
    {
        this.type = type;
        this.level = level;
    }
}

public class Cells : MonoBehaviour
{
    static int t = 0;
    static Cell[,] cellGrid;
    static BufferPheromone[,] bufferPheromones;
    
    public void SetCellGrid(Cell[,] cg)
    {
        cellGrid = cg;
    }
    public float getPheromoneLevelAt(int x, int y) {
        return cellGrid[x, y].pheromone.GetLevel();
    
    }
    // Start is called before the first frame update
    void Start()
    {
        int w = Cells.cellGrid.GetLength(0);
        int h = Cells.cellGrid.GetLength(1);

        bufferPheromones = new BufferPheromone[Cells.cellGrid.GetLength(0), Cells.cellGrid.GetLength(1)];


        for(int i=0; i<w; i++)
        {
            for(int j=0; j<h; j++)
            {
                if (cellGrid[i, j] != null)
                    bufferPheromones[i, j] = new BufferPheromone(cellGrid[i, j].pheromone);
                else
                    bufferPheromones[i, j] = null;
            }
        }
    }

    void EvaluatePheromones()
    {
        int w = Cells.cellGrid.GetLength(0);
        int h = Cells.cellGrid.GetLength(1);

        // initialize buffer for modification
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (cellGrid[i, j] != null)
                    bufferPheromones[i, j] = new BufferPheromone(cellGrid[i, j].pheromone);
                else
                    bufferPheromones[i, j] = null;
            }
        }

        // modify buffer
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if(cellGrid[i, j] != null)
                {
                    float sigma = Pheromone.evaporationRate;
                    float delta = Pheromone.diffusionRate;

                    float avgNeighbor = 0;

                    foreach (Cell c in new List<Cell>() { cellGrid[i, j].bottom,
                                            cellGrid[i, j].top,
                                            cellGrid[i, j].left,
                                            cellGrid[i,j].right,
                                            cellGrid[i, j].topLeft,
                                            cellGrid[i, j].topRight,
                                            cellGrid[i, j].bottomLeft,
                                            cellGrid[i, j].bottomRight}
                        )
                    {
                        if (c != null) avgNeighbor += c.pheromone.level;
                    }
                    avgNeighbor = delta * avgNeighbor / 8.0f;
                    cellGrid[i, j].COMPONENTSTUFF = new Vector3(cellGrid[i, j].previousPheromoneLevel, cellGrid[i, j].pheromone.level - cellGrid[i, j].previousPheromoneLevel, avgNeighbor);

                    float evaluatedPheromoneLevel = sigma * ((1 - delta) * cellGrid[i, j].previousPheromoneLevel
                                                             + cellGrid[i, j].pheromone.level - cellGrid[i, j].previousPheromoneLevel
                                                             + avgNeighbor
                                                            );
                    cellGrid[i, j].evaulatedLevel = evaluatedPheromoneLevel;
                    
                    if(evaluatedPheromoneLevel > 0)
                    {
                        // Set it to attractive for now ... (._.  )
                        bufferPheromones[i, j].type = PheromoneType.Attractive;
                    }

                    if (evaluatedPheromoneLevel < 0.09f)
                    {
                        evaluatedPheromoneLevel = 0f;
                        bufferPheromones[i, j].type = PheromoneType.None;
                    }
                    bufferPheromones[i, j].level = evaluatedPheromoneLevel;
                }
            }
        }



        // set current pheromones to buffer values
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (cellGrid[i, j] != null)
                {
                    cellGrid[i, j].previousPheromoneLevel = cellGrid[i, j].pheromone.level;
                    cellGrid[i, j].pheromone.SetPheromone(bufferPheromones[i, j].type, bufferPheromones[i, j].level);
                }
                    
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        t = (t + 1) % 30;
        if (t == 0)
        {
            EvaluatePheromones();
        }
    }

    void DiffusePheromones()
    {

    }
}
