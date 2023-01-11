using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var _keyboard = Keyboard.current;
        if (_keyboard == null)
            return;

        if(_keyboard.bKey.isPressed)
        {
            print("HOND");
        }

    }
}
