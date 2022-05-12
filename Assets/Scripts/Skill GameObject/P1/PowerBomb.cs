using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBomb : AoeBomb
{
    private void Start()
    {
        StartCoroutine(ObjDestroyAnim());
    }
}
