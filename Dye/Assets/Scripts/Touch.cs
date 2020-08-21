﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Gestures;

public class Touch : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] [Range(0, 5)] float lerpTime;

    PinnedTransformGesture gesture;
    Vector3 _targetRotation = Vector3.zero;
    Quaternion targetRotation;

    public LayerMask layerMask;


    private void Awake()
    {
        gesture = GetComponent<PinnedTransformGesture>();
    }

    private void OnEnable()
    {
        gesture.Transformed += transformedHandler;
    }

    private void OnDisable()
    {
        gesture.Transformed -= transformedHandler;
    }

    private void transformedHandler(object sender, System.EventArgs e)
    {
        //transform.localRotation *= Quaternion.AngleAxis(gesture.DeltaRotation, gesture.RotationAxis);


        _targetRotation += gesture.DeltaRotation * Vector3.up;
        targetRotation = Quaternion.Euler(_targetRotation);


        //transform.Rotate(gesture.DeltaRotation * Vector3.up);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpTime * Time.deltaTime);

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit Hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out Hit, Mathf.Infinity, layerMask))
            {
                ISelecionavel interacao = Hit.collider.GetComponent<ISelecionavel>();
                if (interacao != null) interacao.selecao();
            }
        }

    }

}
