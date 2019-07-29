using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bleedGreen : MonoBehaviour
{
    // Start is called before the first frame update
    void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
