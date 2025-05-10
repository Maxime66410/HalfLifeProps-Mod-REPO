using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManHackPhysics : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody rb;

    public ManHackPhysics(Rigidbody rb)
    {
        this.rb = rb;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Moon gravity
        rb.useGravity = false;
        rb.isKinematic = false;
    }
}
