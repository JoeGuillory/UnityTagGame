using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlatformEasing : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveX(9, 3).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

}
