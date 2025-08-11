using UnityEngine;
using System.Collections.Generic;

public class CameraResolution : MonoBehaviour
{
    public enum FitMode { FitWidth, FitHeight, Cover, Contain }
    public FitMode mode = FitMode.FitHeight;

    public float designWorldWidth = 9f;   // 예: 9유닛
    public float designWorldHeight = 16f; // 예: 16유닛

    Camera cam;

    void Update()
    {
        cam = GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;

        float sizeByHeight = designWorldHeight * 0.5f;                // 세로 고정
        float sizeByWidth = (designWorldWidth / (2f * aspect));      // 가로 고정

        switch (mode)
        {
            case FitMode.FitHeight: cam.orthographicSize = sizeByHeight; break;   // 세로가 항상 같음, 좌우가 더 보이거나 잘림
            case FitMode.FitWidth: cam.orthographicSize = sizeByWidth; break;   // 가로가 항상 같음, 상하가 더 보이거나 잘림
            case FitMode.Cover: cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth); break;  // 화면 꽉 채우기(밖은 잘라냄)
            case FitMode.Contain: cam.orthographicSize = Mathf.Min(sizeByHeight, sizeByWidth); break;  // 전부 보이기(레터박스 가능)
        }
    }
}