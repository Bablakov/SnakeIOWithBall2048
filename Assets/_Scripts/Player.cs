using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour {
    [SerializeField, Range(0.1f, 100f)] private float speed = 1f;
    [SerializeField] private Transform head;
    [SerializeField] private List<Transform> bodyElements;

    private InputGame _inputGame;
    private Rotation _rotation;
    private Movement _movement;

   /* private int Gap = 10;
    private List<Vector3> PositionHistory = new List<Vector3>();*/

    private void Awake() {
        _movement = new Movement(head);
        _rotation = new Rotation(head);
        _inputGame = new InputDesktop();
    }

    private void Update() {
        Rotate();
        Move();
        /*PositionHistory.Insert(0, objectRotationMovement.transform.position);
        if (PositionHistory.Count > bodyElements.Count * Gap) {
            PositionHistory.RemoveRange(bodyElements.Count * Gap, PositionHistory.Count - bodyElements.Count * Gap);
        }*/
        MoveAndRotateBody();
    }

    private void MoveAndRotateBody() {
        /*var pointLook = objectRotationMovement.position;
        pointLook.y = 0.5f;


        bodyElements[0].transform.LookAt(pointLook);
        Vector3 moveDirection = objectRotationMovement.position -  bodyElements[0].position;
        moveDirection.y = 0;
        bodyElements[0].transform.position += moveDirection.normalized * speed * Time.deltaTime;

        for (int i = 1; i < bodyElements.Count; i++) {
            pointLook = bodyElements[i - 1].position;
            pointLook.y = 0.5f;


            bodyElements[i].transform.LookAt(pointLook);
            moveDirection = bodyElements[i - 1].position - bodyElements[i].position;
            bodyElements[i].transform.position += moveDirection.normalized * speed * Time.deltaTime;
            Debug.Log(i);
        }*/
        /*
                int index = 0;
                foreach (var body in bodyElements) {
                    Vector3 point = PositionHistory[Mathf.Min(index * Gap, PositionHistory.Count - 1)];
                    Vector3 moveDirection = point - body.transform.position;
                    body.transform.position += moveDirection * speed * Time.deltaTime;
                    body.transform.LookAt(point);
                    index++;
                }
                Debug.Log(PositionHistory.Count);*/

        bodyElements[0].transform.position = head.position - head.forward * 0.5f - bodyElements[0].forward * 0.5f;

    }

    private void Rotate() {
        _rotation.Rotate(_inputGame.GetDirectionMovememt());
    }

    private void Move() {
        _movement.Move(speed);
    }
}