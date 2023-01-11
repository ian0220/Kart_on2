using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class manu : MonoBehaviour
{
    [SerializeField] private GameObject m_Canvas;
    [SerializeField]private GameObject m_OnePlayer;
    [SerializeField] private GameObject m_TwoPlayer;
    private int m_WichButton = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
  

    public void HandleMoveInput(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();
        Color _1playercolor = m_OnePlayer.GetComponent<RawImage>().color;
        Color _2playercolor = m_TwoPlayer.GetComponent<RawImage>().color;
        if (direction.y > 0.2f)
        {
            _1playercolor = new Color(_1playercolor.r, _1playercolor.g, _1playercolor.b, 200);
            _1playercolor = new Color(_2playercolor.r, _2playercolor.g, _2playercolor.b, 255);
        }
        else if(direction.y < -0.2f)
        {
            _1playercolor = new Color(_1playercolor.r, _1playercolor.g, _1playercolor.b, 255);
            _2playercolor = new Color(_2playercolor.r, _2playercolor.g, _2playercolor.b, 200);
        }

        m_OnePlayer.GetComponent<RawImage>().color = _1playercolor;
        m_TwoPlayer.GetComponent<RawImage>().color = _2playercolor;
        //  Debug.Log(direction);
    }
}
