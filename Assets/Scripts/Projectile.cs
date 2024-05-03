using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Attack
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        CheckRange();  // Consistent check for out-of-range
    }

    protected override void OnTriggerEnter2D(Collider2D hitInfo)
    {
        base.OnTriggerEnter2D(hitInfo);
        if (hitInfo.CompareTag("Wall" ))
        {
            Destroy(gameObject);
        }
    }
}