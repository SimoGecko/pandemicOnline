﻿// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// generic board piece that can be moved smoothly around and set in a particular position //////////

public class BoardPiece : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    bool useRandomPos = true;
    float circleRadius = .2f; // random around center


    float movementSpeed = 2f; // units/s

    AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    readonly Vector3 restPosition = new Vector3(-5, 0, 0);

    Vector3 posOffset;

    // private
    Vector3 endPos;
    public City CurrentCity { get; private set; }


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {

    }

    void Update() {

    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void MoveToCity(string cityNid) {
        MoveToCity(City.Get(cityNid));
    }
    public void MoveToCity(City city) {
        CurrentCity = city;
        MoveToPosition(city.Position);
    }

    public void MoveTo(Vector3 pos) {
        CurrentCity = null;
        MoveToPosition(pos);
    }
    public void MoveAway() {
        MoveTo(restPosition);
    }


    void MoveToPosition(Vector3 p) {
        if (useRandomPos) {
            posOffset = Utility.OnUnitCircle().To3() * circleRadius;
        }

        endPos = p + posOffset;
        StopCoroutine("MoveToRoutine");
        StartCoroutine("MoveToRoutine");
    }

    public void SetColor(char color) {
        GetComponentInChildren<MeshRenderer>().material = ColorManager.instance.Char2Material(color);
    }



    // queries



    // other
    IEnumerator MoveToRoutine() {
        float percent = 0;
        float dist = Vector3.Distance(transform.position, endPos);
        Vector3 startPos = transform.position;

        while (percent < 1) {
            percent += Time.deltaTime * movementSpeed / dist;
            transform.position = Vector3.Lerp(startPos, endPos, movementCurve.Evaluate(percent));
            yield return null;
        }
        transform.position = endPos;
    }

}