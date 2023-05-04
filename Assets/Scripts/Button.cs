using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
// using AppEventMonitor;

namespace BongoCatUnity
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Button:  MonoBehaviour
    {
        [SerializeField]
        private GameObject Pressed;

        [SerializeField]
        private GameObject Released;

        [SerializeField]
        [InputControl]
        private string Path;

        [SerializeField]
        private HandController HandController;

        private InputControl _inputControl;

        private void Reset()
        {
            Released = transform.Find("Released")?.gameObject;
            Pressed = transform.Find("Pressed")?.gameObject;
        }

        private void Awake()
        {
            _inputControl = InputSystem.FindControl(Path);
        }
        // void Start(){
        //     AppEventMonitorManager.OnKeyDown += OnKeyDown;
        //     //AppEventMonitorManager.OnKeyUp += OnKeyUp;
        // }

        // void OnKeyDown(string key){
        //     Debug.Log(key);
        //     if(!string.Equals(string.Concat("<Keyboard>/",key), Path)) return;
        //     Released.SetActive(false);
        //     Pressed.SetActive(true);
        //     HandController.handPos = transform.position;
        //     HandController.handDown = _inputControl.IsPressed();

        // }
        
        // void OnKeyUp(string key){
        //     if(!string.Equals(string.Concat("<Keyboard>/",key), Path)) return;
        //     Released.SetActive(true);
        //     Pressed.SetActive(false);

        // }
        private void Update()
        {
            if (_inputControl is not null)
            {
                Pressed.SetActive(_inputControl.IsPressed());
                Released.SetActive(!_inputControl.IsPressed());
                
                if (_inputControl.IsPressed())
                {
                    HandController.handPos = transform.position;
                    HandController.handDown = _inputControl.IsPressed();
                }
            }
        }
    }
}