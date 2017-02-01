using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCursor : MonoBehaviour
{
    private GameObject lastSelectedGameObject;

    void Start()
    {
        lastSelectedGameObject = EventSystem.current.firstSelectedGameObject;
    }

	void Update()
    {
        GameObject currentSelectedGameObject = EventSystem.current.currentSelectedGameObject;
        if (currentSelectedGameObject && currentSelectedGameObject != lastSelectedGameObject)
        {
            Vector3 position = transform.position;
            position.y = currentSelectedGameObject.transform.position.y;
            transform.position = position;

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }
        lastSelectedGameObject = currentSelectedGameObject;

    }
}
