using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_script : MonoBehaviour
{
    public void close_prefab(GameObject what) {
        Destroy(what);
    }
}
