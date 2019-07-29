using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingScreen : MonoBehaviour
{
    public Sprite loading1;
    public Sprite loading2;
    public Sprite loading3;
    public Sprite loading4;
    public Sprite loading5;

    void Awake()
    {
        Sprite[] loadingList = new Sprite[]
                        {
                       loading1,
                       loading2,
                       loading3,
                       loading4,
                       loading5
                        };
        SpriteRenderer image = GetComponent<SpriteRenderer>();
        image.sprite = loadingList[Random.Range(0, 4)];
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
