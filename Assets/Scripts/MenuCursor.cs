using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCursor : MonoBehaviour
{
	void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject)
        {
            Vector3 position = transform.position;
            position.y = EventSystem.current.currentSelectedGameObject.transform.position.y;
            transform.position = position;
        }
	}
}
