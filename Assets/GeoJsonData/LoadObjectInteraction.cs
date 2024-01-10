using Microsoft.MixedReality.Toolkit.Extensions.SceneTransitions;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;
using Airports;
using HazardsFunctions;
using System.Linq;
using Map;
using TMPro;

public class LoadObjectInteraction : MonoBehaviour
{
    private Interactable interactable;
    public GameObject ObjectContainer;
    public TextMeshPro textMesh;
    public string ObjectName;

    private GameObject mountainObject;
    private bool isObjectActive = true;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnClick.AddListener(OnButtonClicked);

        // Find the ObjectContainer at runtime
        mountainObject = ObjectContainer.transform.Find(ObjectName).gameObject;

        if (ObjectContainer == null)
        {
            Debug.LogError("ObjectContainer not found!");
        }
    }

    private void OnButtonClicked()
    {
        if (textMesh == null)
        {
            textMesh = gameObject.GetComponent<TextMeshPro>();
        }
        isObjectActive = !isObjectActive;
        mountainObject.SetActive(isObjectActive);

        textMesh.text = "Button Clicked!";
    }
}
