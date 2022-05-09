using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject chilrenObj => transform.GetChild(0).gameObject;

    private void Update() {
        Debug.Log(chilrenObj.gameObject.transform.position);
    }
}
