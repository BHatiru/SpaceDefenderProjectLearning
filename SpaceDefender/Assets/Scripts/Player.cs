using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Vector2 minBound, maxBound;
    [SerializeField] float moveSpeed = 9f;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Shooter shooter;

    void Awake(){
        shooter = GetComponent<Shooter>();
    }
    void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBound = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBound = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    void Move()
    {
        Vector2 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBound.x + paddingLeft, maxBound.x-paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBound.y + paddingBottom, maxBound.y-paddingTop);
        transform.position = newPosition;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value){
        if(shooter != null){
            shooter.isFiring = value.isPressed;
        }
    }
}
