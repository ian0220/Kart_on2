using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewInputTest : MonoBehaviour
{
    private InputActionAsset m_InputAsset;
    private InputActionMap m_Player;
    private InputAction m_Move;
    
    // Start is called before the first frame update

    private void OnDisable()
    {
        m_Player.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        //print(m_Move);
    }

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

      //  Debug.Log(direction);
    }
}
