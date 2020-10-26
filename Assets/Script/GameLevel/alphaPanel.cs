using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class alphaPanel : MonoBehaviour
{
    public GameObject panel1;
    void Start()
    {
        panel1.GetComponent<CanvasGroup>().DOFade(0, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
