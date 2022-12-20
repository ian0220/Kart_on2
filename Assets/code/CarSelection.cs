using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelection : MonoBehaviour
{
    [SerializeField] GameObject[] racingCars;
    [SerializeField] private int carNumber;
    [SerializeField] private float moveAmmount;
    [SerializeField] private float minMoveAmmount;
    [SerializeField] private float maxMoveAmmount;
    public GameObject currentCar;

    private void Start()
    {
        carNumber = 0;
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
    }

    public void GoLeft()
    {
        gameObject.transform.position = new Vector3(transform.position.x - moveAmmount, transform.position.y, transform.position.z);
        if (carNumber > 0)
            carNumber--;
    }

    public void GoRight()
    {
        gameObject.transform.position = new Vector3(transform.position.x + moveAmmount, transform.position.y, transform.position.z);
        if (carNumber < racingCars.Length - 1)
            carNumber++;
    }

    public void SelectCar()
    {
        currentCar = racingCars[carNumber];
    }
}
