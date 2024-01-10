using Microsoft.MixedReality.Toolkit.Extensions.SceneTransitions;
using Microsoft.MixedReality.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Microsoft.MixedReality.Toolkit.UI;

public class MenuButtonInteractions : MonoBehaviour
{
    private Interactable interactable;
    public string sceneToLoad;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        interactable.OnClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        Debug.Log("Button Clicked!");
        SceneManager.LoadScene(sceneToLoad);
    }
}
