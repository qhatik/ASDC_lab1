using System;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class InsertionSortVisualization : MonoBehaviour
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
        StartCoroutine(InsertionSort());
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

    IEnumerator InsertionSort()
    {
        int n = rectangles.Length;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 1; i < n; ++i)
        {
            float key = rectangleValues[i];
            GameObject keyRectangle = rectangles[i];
            int j = i - 1;

            while (j >= 0 && rectangleValues[j] > key)
            {
                rectangleValues[j + 1] = rectangleValues[j];
                rectangles[j + 1] = rectangles[j];
                j = j - 1;
                rectangles[j + 1].GetComponent<SpriteRenderer>().color = Color.red;
            }

            rectangleValues[j + 1] = key;
            rectangles[j + 1] = keyRectangle;

            for (int k = 0; k <= i; k++)
            {
                Vector2 newPosition = new Vector2(k * (spacing + maxWidth), 0f);
                rectangles[k].transform.position = newPosition;
            }

            yield return new WaitForSeconds(0.5f);
        }

        stopwatch.Stop();
        TimeSpan elapsedTime = stopwatch.Elapsed;

        timeText.text = "Insertion Sort Time: " + elapsedTime.TotalSeconds.ToString("F2") + " seconds";

        UnityEngine.Debug.Log("Insertion Sort Time: " + elapsedTime.TotalSeconds + " seconds");
        for (int i = 0; i < n; i++)
        {
            rectangles[i].GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
