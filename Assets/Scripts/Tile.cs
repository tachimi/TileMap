using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tile : MonoBehaviour
{
    [SerializeField] private Vector3 _newScale;

    private Vector3 _originalScale;
    private MeshRenderer[] _tileRenderer;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _tileRenderer = GetComponentsInChildren<MeshRenderer>();
    }

    public void SwitchColorToGreen()
    {
        foreach (var tile in _tileRenderer)
        {
            tile.material.color = Color.green;
        }
    }

    public void SwitchColorToRed()
    {
        transform.localScale = _newScale;
        foreach (var tile in _tileRenderer)
        {
            tile.material.color = Color.red;
        }
    }

    public void ResetColor()
    {
        transform.localScale = _originalScale;
        foreach (var tile in _tileRenderer)
        {
            tile.material.color = Color.white;
        }
    }
}