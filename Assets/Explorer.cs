using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explorer : MonoBehaviour
{
    public Material mat;
    public Vector2 pos;
    public float scale, angle;

    private Vector2 smoothPos;
    private float smoothScale, smoothAngle;

    private void updateShader()
    {
        smoothPos = Vector2.Lerp(smoothPos, pos, .5f);
        smoothScale = Mathf.Lerp(smoothScale, scale, .03f);
        smoothAngle = Mathf.Lerp(smoothAngle, angle, .03f);


        float aspect = (float)Screen.width / (float)Screen.height;
        float scaleX = smoothScale;
        float scaleY = smoothScale;

        if (aspect > 1f)
            scaleY /= aspect;
        else
            scaleX *= aspect;

        mat.SetVector("_Area", new Vector4(smoothPos.x, smoothPos.y, scaleX, scaleY));
        mat.SetFloat("_Angle", smoothAngle);
    }
    
    private void HandleInputs()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
            scale *= .99f;

        if (Input.GetKey(KeyCode.KeypadMinus))
            scale *= 1.01f;

        if (Input.GetKey(KeyCode.E))
            angle -= .01f;

        if (Input.GetKey(KeyCode.Q))
            angle += .01f;

        //
        Vector2 dir = new Vector2(.01f * scale, 0);
        float s = Mathf.Sin(angle);
        float c = Mathf.Cos(angle);
        dir = new Vector2(dir.x * c, dir.x * s);

        if (Input.GetKey(KeyCode.A))
            pos -= dir;

        if (Input.GetKey(KeyCode.D))
            pos += dir;

        //
        dir = new Vector2(-dir.y, dir.x);
        if (Input.GetKey(KeyCode.S))
            pos -= dir;

        if (Input.GetKey(KeyCode.W))
            pos += dir;
    }

    void FixedUpdate()
    {
        HandleInputs();
        updateShader();
    }
}
