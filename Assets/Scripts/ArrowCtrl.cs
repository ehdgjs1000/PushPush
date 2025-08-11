using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ArrowCtrl : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ArrowAnimLeft());
    }
    IEnumerator ArrowAnimLeft()
    {
        while (true)
        {
            this.transform.DOLocalMoveX(-200,1.5f);
            yield return new WaitForSeconds(1.7f);
            this.transform.DOLocalMoveX(200, 0.0f);
            yield return new WaitForSeconds(0.3f);
        }

    }

}
