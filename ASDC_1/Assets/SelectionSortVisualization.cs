using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System.Diagnostics;
using System;

public class SelectionSortVisualization : MonoBehaviour
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
        StartCoroutine(SelectionSort());
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

    IEnumerator SelectionSort()
    {
        int n = rectangles.Length;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;

            for (int j = i + 1; j < n; j++)
            {
                if (rectangleValues[j] < rectangleValues[minIndex])
                {
                    minIndex = j;
                }
            }

            rectangles[minIndex].GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            
            Vector2 tempPosition = rectangles[i].transform.position;
            rectangles[i].transform.position = rectangles[minIndex].transform.position;
            rectangles[minIndex].transform.position = tempPosition;

            GameObject tempObj = rectangles[i];
            rectangles[i] = rectangles[minIndex];
            rectangles[minIndex] = tempObj;

            float tempValue = rectangleValues[i];
            rectangleValues[i] = rectangleValues[minIndex];
            rectangleValues[minIndex] = tempValue;
        }

        stopwatch.Stop();
        TimeSpan elapsedTime = stopwatch.Elapsed;

        timeText.text = "Selection Sort Time: " + elapsedTime.TotalSeconds.ToString("F2") + " seconds";

        UnityEngine.Debug.Log("Selection Sort Time: " + elapsedTime.TotalSeconds + " seconds");
       
        for (int i = 0; i < n; i++)
        {
            rectangles[i].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
