﻿using System;
using SardineFish.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
//using AppEventMonitor;

public class Manipulator : MonoBehaviour
{
    public float scaleFactor = 0.1f;
    
    private Vector2 holdPos;
    private Vector2 holdLocalPos;
    private bool mouseHover = false;

    // private void Start() {
    //     AppEventMonitorManager.Start();
    // }

    // void OnDestroy()
    // {
    //     //Debug.Log("Stop!!");
    //     AppEventMonitorManager.Stop();
    // }
    private void OnMouseDown()
    {
        holdPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        holdLocalPos = transform.position;
    }

    private void OnMouseDrag()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition).ToVector2();
        var delta = pos - holdPos;
        transform.position = (holdLocalPos + delta).ToVector3(transform.position.z);
    }

    private void OnMouseEnter()
    {
        mouseHover = true;
        //TransparentWindowManager.Instance.clickThrough = false;
    }

    private void OnMouseExit()
    {
        mouseHover = false;
        //TransparentWindowManager.Instance.clickThrough = true;
    }

    private void Update()
    {
        var mouse = UnityEngine.InputSystem.Mouse.current;
        var keyboard = UnityEngine.InputSystem.Keyboard.current;
        if (mouseHover && Input.mouseScrollDelta.y != 0)
        {
            transform.localScale *= 1 + scaleFactor * Mathf.Sign(Input.mouseScrollDelta.y);
        }

        var pos = Camera.main.ScreenToWorldPoint(mouse.position.ReadValue());
        
        if (Physics2D.OverlapPoint(pos) is not null and var collider)
        {
            OnMouseEnter();
            if(mouse.leftButton.wasPressedThisFrame)
                OnMouseDown();
            
            if (mouse.leftButton.isPressed)
                OnMouseDrag();
        }
        else
        {
            OnMouseExit();
        }
    }
}
