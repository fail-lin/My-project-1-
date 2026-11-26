using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Ballmover : MonoBehaviour
{
   
    public AnimationCurve speedCurve = new AnimationCurve(
        new Keyframe(0f, 2f),
        new Keyframe(0.5f, 0.1f),
        new Keyframe(1f, 2f));
    public Vector3 pointa = new Vector3(-5f,0,0);
    public Vector3 Pointb = new Vector3(5f, 0, 0);
    private float waittime = 1.5f;
    private enum state {moving,waiting}
    private state state1;
    private float totaldistant;
    private float distance;
    private float wait;
    private int direction ;


    public GameObject Sphere;
    public Vector3 Targetposition = new Vector3 (0, 0, 0);
    public float speed=5f;
    // Start is called befoe the first frame update
    void Start()
    {
        totaldistant = Vector3.Distance(pointa, Pointb);
        transform.position = pointa ;
        float direction = 1;
        distance= 0;
        state1 = state.moving;
    }

    // Update is called once per frame
    void Update()
    {
        if(state1 == state.waiting)
        {
            wait += Time.deltaTime;
            if (wait >= waittime)
            {
                wait = 0;
                state1 = state.moving;
            }
            return;
        }
        float process = Mathf.Clamp(distance/totaldistant,0f,totaldistant);
        float curve = direction > 0 ? process : 1 - process; 
        float k = speedCurve.Evaluate(curve);
        distance += speed * Time.deltaTime * direction * k;
        if(distance>=totaldistant)
        {
            distance = totaldistant;
            direction = -1;
            state1 = state.waiting;
            wait = 0;
        }
        else if(distance <= 0)
        {
            distance = 0;
            state1 = state.waiting;
            direction = 1;
            wait = 0;
            
        }
        Vector3 dir = (Pointb - pointa).normalized;
        transform.position = distance * dir +pointa;
    }
}
