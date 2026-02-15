using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPattern : MonoBehaviour
{
    GameObject[] spheres;
    Vector3[] startPosition, endPosition;
    static int numSphere = 200;
    static int loops = 6;
    float time = 0f;
    int numMoving = 1;
    // Start is called before the first frame update
    void Start()
    {
        spheres = new GameObject[numSphere]; // how many spheres
        startPosition = new Vector3[numSphere]; // initial positions of the spheres
        endPosition = new Vector3[numSphere];
        // Let there be spheres..
        //Start = Spiral, End = slight translation on Z
        for (int i = 0; i < numSphere; i++)
        {
            float r = 10f; // radius of the circle
            // Draw primitive elements:
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject.CreatePrimitive.html
            spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            // Initial positions of the spheres. make it in circle with r radius.
            // https://www.cuemath.com/geometry/unit-circle/
            //Circle Equation
            //spheresInitialPositions[i] = new Vector3(r * Mathf.Sin(i * 2 * Mathf.PI / numSphere), r * Mathf.Cos(i * 2 * Mathf.PI / numSphere), 10f);
            //Spiral of Archimedes Polar Equation: r = a + bTheta
            float a = 0, b = 0.6f, theta = i * 2 * loops * Mathf.PI / numSphere;
            r = a + (b * theta);
            startPosition[i] = new Vector3(r * Mathf.Cos(theta), r * Mathf.Sin(theta), 10);
            spheres[i].transform.position = startPosition[i];

            // Get the renderer of the spheres and assign colors.
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            // hsv color space: https://en.wikipedia.org/wiki/HSL_and_HSV
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            Color color = Color.HSVToRGB(hue, 1f, 1f); // Full saturation and brightness
            sphereRenderer.material.color = color;

            endPosition[i] = startPosition[i] - new Vector3(0, 0, i);
        }
    }
    void Update()
    {
        // Measure Time 
        time += Time.deltaTime; // Time.deltaTime = The interval in seconds from the last frame to the current one
                                // what to update over time?

        for (int i = 0; i < numSphere; i++)
        {

            // Lerp : Linearly interpolates between two points.
            // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/Vector3.Lerp.html
            // Vector3.Lerp(startPosition, endPosition, lerpFraction)

            // lerpFraction variable defines the point between startPosition and endPosition (0~1)
            // let it oscillate over time using sin function
            float lerpFraction = Mathf.Sin(time) * 0.5f + 0.5f;
            // Lerp logic. Update position
            spheres[i].transform.position = Vector3.Lerp(startPosition[i], endPosition[i], lerpFraction);
            // For now, start positions and end positions are fixed. But what if you change it over time?
            // startPosition[i]; endPosition[i];

            // Color Update over time
            Renderer sphereRenderer = spheres[i].GetComponent<Renderer>();
            float hue = (float)i / numSphere; // Hue cycles through 0 to 1
            //Color color = Color.HSVToRGB(Mathf.Abs(hue * Mathf.Sin(time)), Mathf.Cos(time), 2f + Mathf.Cos(time)); // Full saturation and brightness
            Color color = Color.HSVToRGB(hue, 1 - lerpFraction, 1);
            sphereRenderer.material.color = color;
        }
    }
}