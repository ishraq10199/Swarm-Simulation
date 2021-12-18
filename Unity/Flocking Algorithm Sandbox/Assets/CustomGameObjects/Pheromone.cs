using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PheromoneType
{
    Repulsive,
    None,
    Attractive
}

public class Pheromone : MonoBehaviour
{
    public static float evaporationRate;
    public static float diffusionRate;

    private bool active;
    // TODO: set to private
    public PheromoneType type;

    [Range(0, 10)]
    public float level;


    public bool IsActive() { return active;  }
    public PheromoneType getPheromoneType() { return type; }
    public float GetLevel() { return this.level; }
    public void SetPheromone(PheromoneType _type, float _level = 10f) {
        //gameObject.GetComponent<Cell>().previousPheromoneLevel = this.level;
        this.type = _type;
        if (_level < 0f) _level = 0f;
        else if (_level > 10f) _level = 10f;
        if (this.type == PheromoneType.None || _level == 0f)
        {
            this.level = 0f;
            this.type = PheromoneType.None;
        }
        else
        {
            if (this.level > 10f) this.level = 10f;
            else if (this.level < 0f)
            {
                this.level = 0f;
                this.type = PheromoneType.None;
            }
            else this.level = _level;
        }
    }
    public void SetLevel(float _level) {
        //gameObject.GetComponent<Cell>().previousPheromoneLevel = this.level;
        if (this.type == PheromoneType.None) this.level = 0;
        else
        {
            if (this.level > 10f) this.level = 10f;
            else if (this.level < 0f)
            {
                this.level = 0f;
                this.type = PheromoneType.None;
            }
            else this.level = _level;
        }
    }

    // Linear interpolation of Color of a cell, is done for the current level of pheromone
    public Color GetColor()
    {
        if (this.type == PheromoneType.Attractive) return Color.Lerp(Color.white, Color.green, this.level / 10f);
        if (this.type == PheromoneType.Repulsive) return Color.Lerp(Color.white, Color.red, this.level / 10f);
        else return Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        type = PheromoneType.None;
        level = 0f;
        diffusionRate = MapGenerator.dRate;
        evaporationRate = MapGenerator.eRate;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
