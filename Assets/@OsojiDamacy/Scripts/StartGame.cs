using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartGame : MonoBehaviour
{
    [SerializeField] private TargetSpawner _spawner;
    [SerializeField] private Image _startImage;
    [SerializeField] private GameObject _timer,_weightText;
    [SerializeField] private CountDown _countDown;
    [SerializeField] private GameObject  _vertualCam2,_mainCam;
    [SerializeField] private BallController _ball;

    private void Start()
    {
        CameraExtensions.LayerCullingToggle(Camera.main,"WeightText");
    }

    public void StartMainGame()
    {
        _spawner.Spawn();
        CameraExtensions.LayerCullingToggle(Camera.main, "WeightText");
        StartCoroutine(DisplayStartImage());
    }

    private IEnumerator DisplayStartImage()
    {
        _startImage.enabled = true;
        _startImage.transform.DOScale(1.0f, 0.5f);
        yield return new WaitForSeconds(1.0f);
        _vertualCam2.SetActive(false);
        MoveUI();
        _ball.StartControl();
        _startImage.transform.DOScale(0, 0.5f).OnComplete(() => _startImage.enabled = false);
    }

    private void MoveUI()
    {
        _timer.transform.DOLocalMoveX(_timer.transform.localPosition.x + 300, 0.5f).OnComplete(() => _countDown.StartCountDown());
        _weightText.transform.DOLocalMoveX(_weightText.transform.localPosition.x - 300, 0.5f);
    }
}
