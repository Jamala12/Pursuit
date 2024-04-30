using System.Collections;
using UnityEngine;

public class LargeSlash : Slash
{
    protected override void Move()
    {
        transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
}
