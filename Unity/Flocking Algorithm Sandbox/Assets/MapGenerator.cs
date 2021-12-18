using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Material obstacleMaterial;
    int width;
    int height;

    public float scale = 1f;

    public bool placeTarget;
    public string seed;
    public bool useRandomSeed;
    public bool showPheromoneAsColors;

    public Cells cellScriptComponent;

    [Range(0, 100)]
    public int randomFillPercent;
    public int[,] map;


    public bool enableEdgeWalls, enableSmoothing, enableMeshes;
    GameObject parentOfBoxes;

    public int[,] getMap() { return this.map; }
    public int getSeed() { return seed.GetHashCode(); }
    public Vector2 MapSize { get { return new Vector2(width, height);  } }

    public GameObject targets;

    System.Random pseudoRandom;

    public GameObject cells;
    public Cell[,] cellGrid;
    
    [Range(0, 1)]
    public float diffusionRate;

    [Range(0, 1)]
    public float evaporationRate;

    public static float dRate;
    public static float eRate;

    public 

    void Start()
    {
        
        width = (int)(80f * scale);
        height = (int)(60f * scale);

        dRate = diffusionRate;
        eRate = evaporationRate;

        parentOfBoxes = new GameObject();
        parentOfBoxes.name = "parent_of_boxes";
        parentOfBoxes.transform.SetParent(GameObject.Find("Simulation").transform);
        GenerateMap();


    
    }

    void GenerateMap()
    {
        //parentOfBoxes.gameObject.tag = "Wall";
        map = new int[width, height];
        //tiles = new Tile[width, height];
        RandomFillMap();
        
        if (enableSmoothing)
        {
            int n = 5;
            while (n-- != 0)
                SmoothMap();
        }


        
        /*
        if (enableMeshes)
        {
            MeshGenerator meshGen = GetComponent<MeshGenerator>();
            meshGen.GenerateMesh(map, 1.0f);
        }
        */
        if (map != null)
        {
            
            if(cellGrid != null)
            {
                //Debug.Log("Entered Cell Destruction");
                Destroy(cells.gameObject);

                /*
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        
                        
                            Destroy(cells.gameObject);
                        
                    }
                */
                cells = new GameObject();
                cellScriptComponent = cells.AddComponent<Cells>();
                cells.transform.parent = GameObject.Find("Simulation").transform;
                cells.name = "Cells";
                cells.transform.position = new Vector3(0f,0f,0f);
                //Debug.Log("Exited Cell Destruction");
            }
            
            cellGrid = new Cell[width, height];
            cellScriptComponent.SetCellGrid(cellGrid);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    //cellGrid[x, y] = null;
                    // Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    // Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                    // Gizmos.DrawCube(pos, Vector3.one);
                    if (map[x, y] == 1)
                    {
                        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        box.transform.position = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                        box.name = "box";
                        box.tag = "Wall";
                        box.transform.parent = parentOfBoxes.transform;

                    }
                    else
                    {
                        //GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Plane);
                        GameObject cell = GameObject.CreatePrimitive(PrimitiveType.Quad);
                        //cell causes the bug, so disabling it for now

                        //cell.SetActive(false);

                        MeshCollider cellCollider = cell.GetComponent<MeshCollider>();
                        cellCollider.convex = true;
                        cellCollider.isTrigger = true;
                        //Destroy(cell.GetComponent<MeshCollider>());
                        //cell.transform.position= new Vector3(-width / 2 + x + .5f, -0.435f, -height / 2 + y + .5f);
                        cell.transform.position = new Vector3(-width / 2 + x + .5f, 0f, -height / 2 + y + .5f);
                        cell.transform.localScale = new Vector3(1f, 1f, 1f);
                        cell.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                        cell.name = "Cell [" + x + ", " + y + "]";
                        cell.tag = "cell";
                        Cell cellComponent = cell.AddComponent<Cell>();
                        cellGrid[x, y] = cellComponent;
                        cellGrid[x, y].cellPosition = new Vector2Int(x, y);

                        if (showPheromoneAsColors) Cell.EnablePheromoneColor(true);
                        cell.transform.parent = cells.transform;


                        
                    }

                }

            // Evaluate Cell bindings for pheromone diffusion later
            for(int x= 0; x <width; x++)
                for(int y= 0; y < height; y++)
                {
                    if (cellGrid[x, y])
                    {
                        cellGrid[x, y].bottom = (y == 0) ? null : cellGrid[x, y - 1];
                        cellGrid[x, y].bottomLeft = (y == 0 || x == 0) ? null : cellGrid[x - 1, y - 1];
                        cellGrid[x, y].bottomRight = (y == 0 || x == width - 1) ? null : cellGrid[x + 1, y - 1];
                        cellGrid[x, y].right = (x == width - 1) ? null : cellGrid[x + 1, y];
                        cellGrid[x, y].top = (y == height - 1) ? null : cellGrid[x, y + 1];
                        cellGrid[x, y].topLeft = (y == height - 1 || x == 0) ? null : cellGrid[x - 1, y + 1];
                        cellGrid[x, y].topRight = (y == height - 1 || x == width - 1) ? null : cellGrid[x + 1, y + 1];
                        cellGrid[x, y].left = (x == 0) ? null : cellGrid[x - 1, y];
                    }

                }


            // Target Placement 

            bool targetPlaced = false;

            // destroy any existing instance of Target
            foreach(Transform target in targets.transform)
            {
                Destroy(target.gameObject);
            }

            
            while (placeTarget && !targetPlaced)
            {
                int target_x = pseudoRandom.Next(0, width);
                int target_y = pseudoRandom.Next(0, height);

                if (cellGrid[target_x, target_y] == null) continue;
                else
                {
                    GameObject target = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                    target.transform.position = new Vector3(-width / 2 + target_x + .5f, 0f, -height / 2 + target_y + .5f);
                    target.transform.localScale = new Vector3(1f, 1f, 1f);
                    target.name = "Target";
                    target.GetComponent<Renderer>().material.color = new Color(3f, 3f, 1f);
                    target.transform.parent = targets.transform;
                    targetPlaced = true;
                    cellGrid[target_x, target_y].containsTarget = true;
                    cellGrid[target_x, target_y].notifyNeighbours();
                }
            }
            
        }
        parentOfBoxes.AddComponent<MeshFilter>();
        MeshFilter[] meshFilters = parentOfBoxes.GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int i = 0;
        while (i < meshFilters.Length)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            //meshFilters[i].gameObject.SetActive(false);

            i++;
        }
        parentOfBoxes.transform.GetComponent<MeshFilter>().mesh = new Mesh();
        parentOfBoxes.transform.GetComponent<MeshFilter>().mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        
        parentOfBoxes.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        parentOfBoxes.transform.gameObject.SetActive(true);
        MeshRenderer mr = parentOfBoxes.AddComponent<UnityEngine.MeshRenderer>();

        parentOfBoxes.GetComponent<Renderer>().material = obstacleMaterial;
        parentOfBoxes.AddComponent<MeshCollider>();
        GameObject ground = GameObject.Find("Ground");

        if (ground != null)
        {
            ground.transform.localScale = new Vector3(width, .1f, height);
            ground.SetActive(false);
        }



        }
    void Update()
    {
        if (showPheromoneAsColors) Cell.EnablePheromoneColor(true);
        else Cell.EnablePheromoneColor(false);

    }

    public void Randomize()
    {
        width = (int)(80f * scale);
        height = (int)(60f * scale);

        Destroy(parentOfBoxes);

        parentOfBoxes = new GameObject();
        parentOfBoxes.name = "parent_of_boxes";
        parentOfBoxes.transform.SetParent(GameObject.Find("Simulation").transform);

        GenerateMap();


        
        
    }

    void SmoothMap()
    {
        // Creates a copy of the map, and performs smoothing iteration on the copy, to prevent diagonal bias
        int[,] mapCopy = map.Clone() as int[,];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int neighbourWallTiles = GetSurroundingWallCount(x, y);
                if (neighbourWallTiles > 4) mapCopy[x, y] = 1;
                else if (neighbourWallTiles < 4) mapCopy[x, y] = 0;
            }

        map = mapCopy;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX++)
            for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY++)
            {
                if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
                    if (neighbourX != gridX || neighbourY != gridY)
                        wallCount += map[neighbourX, neighbourY];

                }
                // Else case for edges of the map
                else
                {
                    if (enableEdgeWalls)
                        wallCount++;
                }
            }
        return wallCount;

    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        pseudoRandom = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                if (enableEdgeWalls && (x == 0 || x == width - 1 || y == 0 || y == height - 1))
                    map[x, y] = 1;
                else
                    map[x, y] = (pseudoRandom.Next(0, 100) < randomFillPercent) ? 1 : 0;
            }
    }

    void OnDrawGizmos()
    {

    }

}
