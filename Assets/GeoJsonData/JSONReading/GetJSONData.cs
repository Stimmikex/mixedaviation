using System.Collections.Generic;
using UnityEngine;
using Airports;
using Map;
using System.Linq;
using Microsoft.MixedReality.Toolkit.UI;
using Mountains;
using HazardsFunctions;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit;

// using GeoObject;

public class ObjectJSONGenerator : MonoBehaviour
{
    public bool dataType;
    // public AbstractMap map; // Reference to the Mapbox map component
    public GameObject map;
    public GameObject spherePrefab; // The prefab for the sphere object
    public TextAsset jsonFile; // JSON data containing lat, long, and height
    public MapInfo mapinfo;
    private List<GameObject> GameObjectsList = new List<GameObject>();

    private List<Vector3> objectCoords = new List<Vector3>();

    [SerializeField]
    float _spawnScale = 5f;

    public float ObjectSize;

    public Vector3 NewConvertLatLonToVector3(Vector3 latlon)
    {
        float LongFactor = mapinfo.points.LatLongFactors.y; // 1533;
        float LongDelta = mapinfo.points.LatLongDelta.y; //-11805;
        float LatFactor = mapinfo.points.LatLongFactors.x; // 2202;
        float LatDelta = mapinfo.points.LatLongDelta.x; // -101261;//-101728;


        float x = (float)(latlon.y * LongFactor + LongDelta);
        
        float y = (float)(mapinfo.points.verticalScaleFactor * latlon.z + mapinfo.points.verticalDelta);
        float z = (float)(latlon.x * LatFactor + LatDelta);
        return new Vector3(x, y, z);
    }


    public void CreateAirportObject(AirportList al, GameObject hazardObject, string name)
    {
        hazardObject.name = name;
        al.airportlist.ForEach(a =>
        {
            if (!double.IsNaN(a.latitude_deg) && !double.IsNaN(a.longitude_deg))
            {
                Vector3 latLong = new Vector3((float)a.latitude_deg, (float)a.longitude_deg, (float)a.elevation_ft+1500);
                // Vector3 spawnPosition = map.GeoToWorldPosition(latLong, true);
                if (GetInFence(latLong))
                {
                    float boxColliderSize = 40 / ObjectSize;
                    float objectSize = ObjectSize;
                    objectCoords.Add(latLong);
                    Vector3 spawnPosition = NewConvertLatLonToVector3(latLong);
                    GameObject temp = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
                    temp.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
                    temp.transform.SetParent(hazardObject.transform, false);
                    BoxCollider boxCollider = temp.AddComponent<BoxCollider>();
                    boxCollider.size = new Vector3(boxColliderSize, boxColliderSize, boxColliderSize);
                    temp.name = a.name;

                    temp = ObjectInteraction(temp, a.name);
                    GameObjectsList.Add(temp);
                }
            }
            else
            {
                Debug.LogError("Invalid coordinates for airport object: " + a.name);
            }
        });
    }

    public void CreateObject(Hazards.Features al, GameObject hazardObject, string name)
    {
        hazardObject.name = name;
        al.features.ForEach(a =>
        {
            
            if (a.geometry.coordinates.All(coord => !double.IsNaN(coord)) && a.geometry.coordinates.Count >= 3)
            {
                Vector3 latLong = new Vector3((float)a.geometry.coordinates[1], (float)a.geometry.coordinates[0], (float)a.geometry.coordinates[2]);

                if (GetInFence(latLong))
                {
                    float boxColliderSize = 20 / ObjectSize;
                    float objectSize = ObjectSize;
                    objectCoords.Add(latLong);
                    Vector3 spawnPosition = NewConvertLatLonToVector3(latLong);
                    GameObject temp = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);
                    temp.transform.localScale = new Vector3(objectSize, objectSize, objectSize);
                    temp.transform.SetParent(hazardObject.transform, false);
                    BoxCollider boxCollider = temp.AddComponent<BoxCollider>();
                    boxCollider.size = new Vector3(boxColliderSize, boxColliderSize, boxColliderSize);
                    temp.name = a.properties.NAME;

                    temp = ObjectInteraction(temp, a.properties.NAME);
                    GameObjectsList.Add(temp);
                }
            }
            
            else
            {
                Debug.LogError("Invalid coordinates for mountain object: " + a.properties.NAME);
            }
            
        });
    }

    private GameObject ObjectInteraction(GameObject ob, string name)
    {
        if (ob)
        {
            Interactable interactable = ob.AddComponent<Interactable>();

            // Subscribe to OnClick event
            interactable.OnClick.AddListener(() => OnHoloLensButtonClicked(ob, name));

            return ob;
        }
        return ob;
    }
    private void OnHoloLensButtonClicked(GameObject clickedObject, string objectName)
    {
        Debug.Log("HoloLens button clicked: " + objectName);
        /*
        ToolTip tooltip = clickedObject.AddComponent<ToolTip>();
        tooltip.ToolTipText = clickedObject.name;
        tooltip.ShowBackground = true;
        Vector3 offset = new Vector3(0, (float)0.05, 0);
        tooltip.transform.position = clickedObject.transform.position + offset;
        tooltip.Pivot.transform.position = clickedObject.transform.position + offset;

        int fontSize = 32;
        tooltip.FontSize = fontSize;
        */
        int fontSize = 32;
        ToolTip tooltip = clickedObject.AddComponent<ToolTip>();
        TextMesh textMesh = tooltip.gameObject.AddComponent<TextMesh>();
        Vector3 offsetMesh = new Vector3(0, 2, 0);
        textMesh.transform.localPosition = offsetMesh;
        textMesh.alignment = TextAlignment.Center;
        textMesh.anchor = TextAnchor.LowerCenter;
        textMesh.fontStyle = FontStyle.BoldAndItalic;
        textMesh.text = clickedObject.name;
        textMesh.fontSize = fontSize;
        textMesh.color = Color.red;

        Destroy(textMesh, 3.0f);

        // Perform your desired action when the user clicks on the object
    }

    private bool GetInFence(Vector3 latLong)
    {

        double lat = latLong.x;
        double lon = latLong.y;
        if (lat >= mapinfo.fence[0].y && lat <= mapinfo.fence[1].y && lon >= mapinfo.fence[0].x && lon <= mapinfo.fence[1].x)
        {
            return true;
        }
        return false;
    }
    private void Update()
    {
        int count = GameObjectsList.Count;
        for (int i = 0; i < count; i++)
        {
            var spawnedObject = GameObjectsList[i];
            Vector3 latLongheight = objectCoords[i];
            Vector3 updatedPosition = NewConvertLatLonToVector3(latLongheight);
            spawnedObject.transform.localPosition = updatedPosition;

        }
    }

    private void Start()
    {
        if (mapinfo)
        {
            string json = jsonFile.text;
            double spawnfactor = map.transform.localScale.x; // 0.0005

            GameObject parentbject = Instantiate(new GameObject(), new Vector3(0, (float)0, 0), Quaternion.identity);
            parentbject.transform.localScale = new Vector3((float)spawnfactor, (float)spawnfactor, (float)spawnfactor);
            parentbject.transform.SetParent(map.transform);

            if (dataType)
            {
                Hazards.Features hazards = HazardUtility.GetFromJSON(json);
                CreateObject(hazards, parentbject, spherePrefab.name);
            }
            else
            {
                AirportList airportdata = AirportUtility.GetAirportsFromJSON(json);
                CreateAirportObject(airportdata, parentbject, spherePrefab.name);
            }
        }
    }
}
