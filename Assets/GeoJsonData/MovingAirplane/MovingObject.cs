using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MovingObject : MonoBehaviour
{

    public string csvFileName;
    public GameObject objectToMove;

    private List<Vector3> waypoints;
    private List<float> times;
    private List<float> distances;
    private List<float> speeds;
    private List<string> colors;


    private float gameTime;
    private int interval;
    private int oldInterval;


    // Start is called before the first frame update
    void Start()
    {

        print("In Start");
        gameTime = 0;
        interval = 0;
        oldInterval = 0;
        LoadWaypoints();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count == 0) return;
        gameTime += Time.deltaTime;

        interval = FindInterval(gameTime, times);
        float moveSpeed = speeds[interval];
        float step = moveSpeed * Time.deltaTime;
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, waypoints[interval+1], step);

        objectToMove.transform.LookAt(waypoints[interval + 1]);

        ChangeObjectColor(colors[interval]);

        if (interval != oldInterval)
        {
            print("Interval: " + interval);
            oldInterval = interval;
        }
    }

    void LoadWaypoints()
    {
        waypoints = new List<Vector3>();
        times = new List<float>();
        distances = new List<float>();
        speeds = new List<float>();
        colors = new List<string>();

        print("I'm here, in LoadWaypoints!");

        using (StreamReader reader = new StreamReader(csvFileName))
        {
            int intervalcounter = 0;
            string headerLine = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                if (values.Length >= 4)
                {
                    float time = float.Parse(values[0]);
                    float x = float.Parse(values[1]);
                    float y = float.Parse(values[2]);
                    float z = float.Parse(values[3]);
                    waypoints.Add(new Vector3(x, y, z));
                    times.Add(time);
                    colors.Add(values[4]);
                }
                if (intervalcounter > 0)
                {
                    distances.Add(Mathf.Sqrt(Mathf.Pow(waypoints[intervalcounter].x - waypoints[intervalcounter - 1].x, 2) +
                        Mathf.Pow(waypoints[intervalcounter].y - waypoints[intervalcounter - 1].y, 2)));
                    speeds.Add((distances[intervalcounter - 1] / (times[intervalcounter] - times[intervalcounter-1]))); 
                }
                intervalcounter++;
            }
        }
        print(waypoints);
        print(times);
        print("Speeds length: " + speeds.Count);
    }

    void ChangeObjectColor(string colorName)
    {
        // Convert the color name to a Color value
        Color color = Color.white; // Default color if the name is not recognized

        switch (colorName.ToLower())
        {
            case "green":
                color = Color.green;
                break;
            case "orange":
                color = new Color(1.0f, 0.5f, 0.0f); // RGB values for orange
                break;
            case "red":
                color = Color.red;
                break;
                // Add more cases for other colors as needed
        }

        // Check if the targetObject has a material
        if (objectToMove != null)
        {
            // Apply the color directly to the specified GameObject's material(s)
            Renderer[] renderers = objectToMove.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    material.color = color;
                }
            }
        }
        else
        {
            Debug.LogError("Target Object is not assigned!");
        }
    }

    int FindInterval(float gameTime, List<float> times)
    {

        while(gameTime > times[times.Count - 1])
        {
            gameTime -= times[times.Count - 1];
        }

        for(int i = 0; i < times.Count; i++)
        {
            if (gameTime < times[i])
            {
                return i - 1;
            }
            
        }
        return 0;
    }

}
