using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width;
    public int height;
    Tile[,] tiles;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;
    int[,] map;

    public bool enableEdgeWalls, enableSmoothing, enableMeshes;


    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {

        map = new int[width, height];
        tiles = new Tile[width, height];
        RandomFillMap();
        if (enableSmoothing)
        {
            int n = 5;
            while (n-- != 0)
                SmoothMap();
        }

        if (enableMeshes)
        {
            MeshGenerator meshGen = GetComponent<MeshGenerator>();
            meshGen.GenerateMesh(map, 1.0f);
        }
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
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
        System.Random pseudoRandom = new System.Random(seed.GetHashCode());

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
        if (map != null)
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
                    Gizmos.DrawCube(pos, Vector3.one);
                }

        }
    }

}
