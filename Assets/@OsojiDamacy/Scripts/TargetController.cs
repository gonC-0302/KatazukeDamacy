using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetController : MonoBehaviour
{
    private Rigidbody rb;
    private MeshCollider col;
    [SerializeField] private float _weight;
    private TextMeshPro _weightText;
    [SerializeField] private float _scale;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _textOffsetY;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<MeshCollider>();
        _weightText = GetComponentInChildren<TextMeshPro>();
        _weightText.text = _weight.ToString() + "kg";
        var time = Mathf.Clamp(_weight / 50f, 0, 1);
        _weightText.color = _gradient.Evaluate(time);
    }

    private void Update()
    {
        _weightText.transform.position = gameObject.transform.position + Vector3.zero + Vector3.up * _textOffsetY;
        _weightText.transform.rotation = Camera.main.transform.rotation;
    }

    public void HitBall()
    {
        col.enabled = false;
        rb.isKinematic = true;
        _weightText.enabled = false;
    }

    public float GetWeight()
    {
        return _weight;
    }

    public float GetScale()
    {
        return _scale;
    }
}
