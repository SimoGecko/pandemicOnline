// (c) Simone Guggiari 2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

////////// generic board piece that can be moved smoothly around and set in a particular position //////////

public class BoardPiece : MonoBehaviour {
    // --------------------- VARIABLES ---------------------

    // public
    //public bool useRandomPos = true;
    //public float circleRadius = .15f; // random around center
    float movementSpeed = 2f; // units/s
    public Vector3 offsetPos;

    AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    //public Vector3 restPosition = new Vector3(-5, 0, 0);

    // private
    bool anim;
    Vector3 goalPos;
    [HideInInspector]
    public bool placed;

    //public City CurrentCity { get; private set; }


    // references


    // --------------------- BASE METHODS ------------------
    void Start() {
        //MoveAway();
        placed = false;
    }

    protected virtual void Update() {
        //lerp to target pos
        if (!anim) {
            transform.position = TargetPos;
        }
        
    }

    // --------------------- CUSTOM METHODS ----------------


    // commands
    public void MoveToCity(string cityNid) {
        MoveToCity(City.Get(cityNid));
    }
    public void MoveToCity(City city) {
        //CurrentCity = city;
        AnimateTo(city.Position);
    }

    public void MoveTo(Vector3 pos) {
        //CurrentCity = null;
        AnimateTo(pos);
    }
    

    public void TeleportTo(Vector3 p) {
        if (anim) {
            StopCoroutine("MoveToAnimation");
            anim = false;
        }
        goalPos = p;
        transform.position = TargetPos;
    }

    void AnimateTo(Vector3 p) {
        if (anim) {
            StopCoroutine("MoveToAnimation");
            anim = false;
        }
        goalPos = p;
        StartCoroutine("MoveToAnimation");
    }





    public void SetColor(char color) {
        GetComponentInChildren<MeshRenderer>().material = ColorManager.instance.Char2Material(color);
    }



    // queries
    Vector3 TargetPos { get { return goalPos + (placed? offsetPos:Vector3.zero); } }



    // other
    IEnumerator MoveToAnimation() {
        anim = true;
        float percent = 0;
        Vector3 startPos = transform.position;
        float dist = Vector3.Distance(startPos, TargetPos);

        while (percent < 1) {
            percent += Time.deltaTime * movementSpeed / dist;
            transform.position = Vector3.Lerp(startPos, TargetPos, movementCurve.Evaluate(percent));
            yield return null;
        }
        transform.position = TargetPos;
        anim = false;
    }

}