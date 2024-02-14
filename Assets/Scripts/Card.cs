using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private Renderer _renderer;

    private int _code;
    private bool _isOpen;
    public int code => _code;
    public bool isOpen => _isOpen;

    public void Init(int code, Texture texture)
    {
        _code = code;
        _renderer.material.mainTexture = texture;
    }

    public void Open()
    {
        animator.SetTrigger("Open");
        _isOpen = true;
    }

    public void Close()
    {
        animator.SetTrigger("Close");
        _isOpen = false;
    }
}
