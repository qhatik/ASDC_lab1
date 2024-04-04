using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Diagnostics;
using System;

public class BubbleSortVisualization : MonoBehaviour
{
    public GameObject rectanglePrefab;
    public int numberOfRectangles = 10;
    public float minWidth = 1f;
    public float maxWidth = 5f;
    public float minHeight = 2f;
    public float maxHeight = 6f;
    public float spacing = 1.5f;
    private GameObject[] rectangles;
    private float[] rectangleValues;
    public TextMeshProUGUI timeText; 

    void Start()
    {
        Intersection();
        StartCoroutine(BubbleSort());
    }

    void Intersection()
    {
        rectangles = new GameObject[numberOfRectangles];
        rectangleValues = new float[numberOfRectangles];

        for (int i = 0; i < numberOfRectangles; i++)
        {
            float width = UnityEngine.Random.Range(minWidth, maxWidth);
            float height = UnityEngine.Random.Range(minHeight, maxHeight);

            rectangleValues[i] = height;
            Vector2 position = new Vector2(i * (spacing + maxWidth), 0f);
            GameObject rectangle = Instantiate(rectanglePrefab, position, Quaternion.identity);
            rectangle.transform.localScale = new Vector2(width, height);
            rectangles[i] = rectangle;
        }
    }

    IEnumerator BubbleSort()
    {
        int n = rectangles.Length;
        bool swapped;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start(); 

        do
        {
            swapped = false;

            for (int i = 1; i < n; i++)
            {
                rectangles[i - 1].GetComponent<SpriteRenderer>().color = Color.red;
                rectangles[i].GetComponent<SpriteRenderer>().color = Color.red;

                yield return new WaitForSeconds(0.5f);

                if (rectangleValues[i - 1] > rectangleValues[i])
                {
                    Vector2 temp = rectangles[i - 1].transform.position;
                    rectangles[i - 1].transform.position = rectangles[i].transform.position;
                    rectangles[i].transform.position = temp;

                    GameObject tempObj = rectangles[i - 1];
                    rectangles[i - 1] = rectangles[i];
                    rectangles[i] = tempObj;

                    float tempValue = rectangleValues[i - 1];
                    rectangleValues[i - 1] = rectangleValues[i];
                    rectangleValues[i] = tempValue;

                    swapped = true;
                }

                rectangles[i - 1].GetComponent<SpriteRenderer>().color = Color.white;
                rectangles[i].GetComponent<SpriteRenderer>().color = Color.white;
            }

            for (int i = 0; i < n; i++)
            {
                if (i >= n - 1)
                {
                    rectangles[i].GetComponent<SpriteRenderer>().color = Color.green;
                }
            }

            n--;

        } while (swapped);

        stopwatch.Stop(); 
        TimeSpan elapsedTime = stopwatch.Elapsed;

       
        timeText.text = "Bubble Sort Time: " + elapsedTime.TotalSeconds.ToString("F2") + " seconds";

        UnityEngine.Debug.Log("Bubble Sort Time: " + elapsedTime.TotalSeconds + " seconds");
        for (int i = 0; i < n; i++)
        {
            rectangles[i].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
