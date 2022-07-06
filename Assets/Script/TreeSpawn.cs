using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    public GameObject tree;

    public void Spawn()
    {
        Instantiate(tree, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
