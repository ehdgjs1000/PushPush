using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageZoom : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(ImageAnim());
    }
    IEnumerator ImageAnim()
    {
        while (true)
        {
            transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.8f);
            yield return new WaitForSeconds(0.85f);
            transform.DOScale(new Vector3(0.9f, 0.9f, 0.9f), 1.0f);
            yield return new WaitForSeconds(1.05f);
        }
    }

}
