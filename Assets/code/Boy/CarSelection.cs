using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] racingCars;
    [SerializeField] private int carNumber;
    [SerializeField] private float moveAmmount;
    [SerializeField] private float minMoveAmmount;
    [SerializeField] private float maxMoveAmmount;
    [SerializeField] private float lerpTime;
    public Vector3 point;
    public GameObject currentCar;
    private bool moveToTarget = false;
    private float LerpOutput;
    public Button ArrowLeft;
    public Button ArrowRight;
    [SerializeField] private Button MiddleButton;
    [SerializeField] private TextMeshProUGUI buurt;
    




    private void Start()
    {
        carNumber = 0;
        point = transform.position;
        SelectCar();
    }

    void Update()
    {
        if (transform.position.x < minMoveAmmount)
        {
            transform.position = new Vector3(minMoveAmmount, transform.position.y, transform.position.z);
        }

        if (transform.position.x > maxMoveAmmount)
        {
            transform.position = new Vector3(maxMoveAmmount, transform.position.y, transform.position.z);
        }


        //
        if (moveToTarget)
        {
            
            transform.position = Vector3.Lerp(transform.position, point, LerpOutput);
            LerpOutput += Time.deltaTime * lerpTime;
            ArrowLeft.interactable = false;
            ArrowRight.interactable = false;

            if (LerpOutput >= 0.98f)
            {
                LerpOutput = 0f;
                moveToTarget = false;
                transform.position = point;
                ArrowLeft.interactable = true;
                ArrowRight.interactable = true;
                MiddleButton.Select();
                SelectCar();
            }

            
        }


    }

    public void GoLeft()
    {
        //transform.position = new Vector3(transform.position.x - moveAmmount, transform.position.y, transform.position.z);

        if (transform.position.x > minMoveAmmount)
        {
        point = new Vector3(transform.position.x - moveAmmount, transform.position.y, transform.position.z);
        MoveCamera();
        }
        else
        {
            transform.position = point;
        }

        if (carNumber > 0)
        {
            MoveCamera();
            carNumber--;
        }

       // Debug.Log("left");
    }

    public void GoRight()
    {
        //transform.position = new Vector3(transform.position.x + moveAmmount, transform.position.y, transform.position.z);

        if(transform.position.x < maxMoveAmmount)
        {
        point = new Vector3(transform.position.x + moveAmmount, transform.position.y, transform.position.z);
        MoveCamera();
        }

        else
        {
            transform.position = point;
        }

        if (carNumber < racingCars.Length - 1)
        {
            MoveCamera();
            carNumber++;
        }
        //Debug.Log("right");
    }

    public void SlectedcarGifTo()
    {
       
        Gamemanger.SingGame.PlayerSelection(currentCar);
        buurt.text = "player 2";
    }
    public void SelectCar()
    {
        Debug.Log("selectcar");
        currentCar = racingCars[carNumber];
    }

    public void MoveCamera()
    {
        // in update gaat die lerpen
        moveToTarget = true;
    }
}
