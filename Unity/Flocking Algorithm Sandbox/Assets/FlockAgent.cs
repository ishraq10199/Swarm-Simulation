using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : Agent
{
    Flock agentFlock;

    public Flock AgentFlock {  get { return agentFlock;  } }

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
    }


    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
        
        //Vector3 rot = transform.localEulerAngles;
        //transform.localEulerAngles = new Vector3(90f, rot.y, rot.z);
    }


    


    // Update is called once per frame
    void Update()
    {
        
    }
}
