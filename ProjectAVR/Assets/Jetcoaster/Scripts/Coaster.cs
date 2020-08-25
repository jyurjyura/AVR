﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coaster : MonoBehaviour
{
    public List<Transform> waypoint;
    public int count = 0;
    public float speed = 5;
    [SerializeField]
    bool isMoving = false;
    //float x, y, z;

    //private void Start()
    //{
    //    x = transform.position.x;
    //    y = transform.position.y;
    //    z = transform.position.z;
    //}
    void Update()
    {
        Vector3 d = waypoint[count].position - transform.position;
        if (d.magnitude < speed * Time.deltaTime)
        {
            isMoving = false;
            
            count++;
            transform.position += d;
            if (count >= waypoint.Count)
            {
                count = 0;
            }
            //Mathf.Lerp
            //transform.LookAt(Mathf.Lerp(transform.position.x,waypoint[count + 1].transform.position.x,0f));
            
            return;
        }
        d.Normalize();
        transform.position += d * Time.deltaTime * speed;
        //transform.rotation = Quaternion.LookRotation(waypoint[count + 1].transform.position);
        isMoving = true;
    }

    public void SetSpeed(float val)
    {
        speed = val;
    }
}
