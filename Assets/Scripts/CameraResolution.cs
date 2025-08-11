using UnityEngine;
using System.Collections.Generic;

public class CameraResolution : MonoBehaviour
{
    public enum FitMode { FitWidth, FitHeight, Cover, Contain }
    public FitMode mode = FitMode.FitHeight;

    public float designWorldWidth = 9f;   // ��: 9����
    public float designWorldHeight = 16f; // ��: 16����

    Camera cam;

    void Update()
    {
        cam = GetComponent<Camera>();
        float aspect = (float)Screen.width / Screen.height;

        float sizeByHeight = designWorldHeight * 0.5f;                // ���� ����
        float sizeByWidth = (designWorldWidth / (2f * aspect));      // ���� ����

        switch (mode)
        {
            case FitMode.FitHeight: cam.orthographicSize = sizeByHeight; break;   // ���ΰ� �׻� ����, �¿찡 �� ���̰ų� �߸�
            case FitMode.FitWidth: cam.orthographicSize = sizeByWidth; break;   // ���ΰ� �׻� ����, ���ϰ� �� ���̰ų� �߸�
            case FitMode.Cover: cam.orthographicSize = Mathf.Max(sizeByHeight, sizeByWidth); break;  // ȭ�� �� ä���(���� �߶�)
            case FitMode.Contain: cam.orthographicSize = Mathf.Min(sizeByHeight, sizeByWidth); break;  // ���� ���̱�(���͹ڽ� ����)
        }
    }
}