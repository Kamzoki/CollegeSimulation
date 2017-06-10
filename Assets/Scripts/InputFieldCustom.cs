/*
 * Description: This class must be attached to InputField gameobject in order for the search behavior to work.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldCustom : MonoBehaviour {

    private void OnEnable()
    { //This class is called each time this code run. It just activates inputfield so that it's able to accept text and process events.
        gameObject.GetComponent<InputField>().ActivateInputField();
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    private void OnDisable()
    {
        //This class is called each time this code exits. It just deactivates inputfield.
        gameObject.GetComponent<InputField>().text = "";
        EventSystem.current.SetSelectedGameObject(null);
    }
}
