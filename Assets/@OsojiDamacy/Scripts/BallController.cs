using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using System;
using DG.Tweening;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private SphereCollider col;
    //[SerializeField]
    //private GameObject _virtualBall;
    private float _currentTotalWeight;
    [SerializeField] private TextMeshProUGUI _weightText;
    [SerializeField] private CinemachineFreeLook _virtualCam;
    private bool _isStart;

    public void StartControl()
    {
        _isStart = true;
        rb.isKinematic = false;
    }

    private enum CameraState
    {
        Low,
        Middle,
        Top
    }
    private CameraState _currentCameraState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        col = GetComponent<SphereCollider>();
        _currentTotalWeight = 0.1f;
        UpdateWeightText();
    }

    void FixedUpdate()
    {
        if (!_isStart) return;
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        Vector3 move = cameraForward * dz + Camera.main.transform.right * dx;
        rb.AddForce(move * 15f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isStart) return;
        if (collision.gameObject.tag == "Target")
        {
            var target = collision.gameObject.GetComponent<TargetController>();
            var targetWeight = target.GetWeight();
            if (_currentTotalWeight < targetWeight) return;
            target.HitBall();
            var upScale = target.GetScale();
            col.radius += upScale;
            //_virtualBall.transform.localScale = Vector3.one * col.radius * 2;
            var pos = UnityEngine.Random.onUnitSphere * col.radius;
            collision.transform.SetParent(this.transform);
            collision.transform.localPosition = pos;
            collision.transform.rotation = UnityEngine.Random.rotation;
            _currentTotalWeight += targetWeight;
            UpdateWeightText();
            switch(_currentCameraState)
            {
                case CameraState.Low:
                    if (_currentTotalWeight >= 10)
                    {
                        // 数値の変更
                        DOTween.To(
                            () => _virtualCam.m_YAxis.Value,          // 何を対象にするのか
                            num => _virtualCam.m_YAxis.Value = num,   // 値の更新
                            0.5f,                  // 最終的な値
                            1                  // アニメーション時間
                        );
                        _currentCameraState = CameraState.Middle;
                    }
                    break;
                case CameraState.Middle:
                    if (_currentTotalWeight >= 50)
                    {
                        // 数値の変更
                        DOTween.To(
                            () => _virtualCam.m_YAxis.Value,          // 何を対象にするのか
                            num => _virtualCam.m_YAxis.Value = num,   // 値の更新
                            1,                  // 最終的な値
                            1                  // アニメーション時間
                        );
                        _virtualCam.m_YAxis.Value = 1f;
                        _currentCameraState = CameraState.Top;
                    }
                    break;
            }
        }
    }

    private void UpdateWeightText()
    {
        _weightText.text = _currentTotalWeight.ToString("N2") + "kg";
    }
}
