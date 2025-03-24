using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlatformEasing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition.x == -9)
        {
            transform.DOMoveX(9, 3);
        }

        if(transform.localPosition.x == 9)
        {
            transform.DOMoveX(-9, 3);
        }
    }

   
}
