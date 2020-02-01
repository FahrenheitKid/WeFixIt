﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    [Header("Properties")]
    public float maxAnchorDistance;
    public float retractionDelay;
    private Vector3 anchorPoint;
    private List<Vector3> linePoints;
    private bool retracting;

    [Header("Refs")]
    private LineRenderer line;
    private HoseBox hoseBox;

    private void Awake()
    {
        retracting = false;
        line = GetComponent<LineRenderer>();
        hoseBox = FindObjectOfType<HoseBox>();

        anchorPoint = hoseBox.transform.position;
        linePoints = new List<Vector3>();
        linePoints.Add(anchorPoint);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Retract();
        }

        if(retracting)
        {
            return;
        }

        float distance = Vector3.Distance(anchorPoint, transform.position);

        if (distance > maxAnchorDistance)
        {
            Vector3 direction = transform.position - anchorPoint;
            direction = direction.normalized * maxAnchorDistance;
            hoseBox.AddCollider(anchorPoint, anchorPoint + direction);
            anchorPoint += direction;
            linePoints.Add(anchorPoint);
        }

        UpdateLine();
    }

    private void UpdateLine()
    {
        line.positionCount = linePoints.Count + 1;
        line.SetPositions(linePoints.ToArray());
        line.SetPosition(line.positionCount - 1, transform.position);
    }

    private void UpdateRetractingLine()
    {
        line.positionCount = linePoints.Count;
        line.SetPositions(linePoints.ToArray());
    }

    public void Retract()
    {
        StartCoroutine(StartRetraction());
    }

    private IEnumerator StartRetraction()
    {
        retracting = true;
        hoseBox.DeleteColliders();

        while (linePoints.Count > 0)
        {
            transform.position = linePoints[linePoints.Count - 1];
            linePoints.RemoveAt(linePoints.Count - 1);
            UpdateRetractingLine();

            yield return new WaitForSeconds(retractionDelay);
        }

        transform.position = hoseBox.transform.position;
        anchorPoint = transform.position;
        linePoints.Add(hoseBox.transform.position);
        retracting = false;
    }
}