using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RotateCamera : MonoBehaviour
{
    private bool isGameStart;
    [SerializeField] private GameObject _cam;
    [SerializeField] private PlayableDirector _director;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isGameStart)
        {
            _cam.SetActive(false);
            StartCoroutine(PlayTimeline());
            isGameStart = true;
        }
        if (isGameStart) return;
        // ワールド座標基準で、現在の回転量へ加算する
        transform.Rotate(0, 0.01f, 0, Space.World);
    }

    private IEnumerator PlayTimeline()
    {
        yield return new WaitForSeconds(1);
        _director.Play();
    }
}
