/*
 * Description: This Class is responsilbe for operations control and player controls. It performs searching, accepts player input, controls UI elements and call other functions. It also stores 
 *              a database of all places in the faculty.
 * Notes: There should only be one instance of ProjectManager or else and expition will take action. I didn't handle such expition because the project isn't that complicated simply don't put another
 *        GameObject with this script on.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class ProjectManager : MonoBehaviour
{

	public static ProjectManager PM; //To enable calling this instance from anyother script.

	public GameObject m_Player; //This is a reference of the player gameobject.
	[HideInInspector]
	public List<PlaceObject> m_Places; // A list of all places located in the faculty. This list is filled dynamically through code (Check Place.cs script).

	public Text m_PlaceDescription; //This is a referece of the text on the screen.
	public GameObject m_SearchBar; //This is a reference of the searchbar gameobject.

	private bool isSearchOn = false;
	private bool isSeachSuccessful = false;

	private GameObject currentSearchedPlace; //This is used to hold a reference to the last searched place.

	void Awake ()
	{
        //This funciton is called on the very start of the project.

		PM = this; //Initilize this static variable to this instance.
		m_Places = new List<PlaceObject> ();// Initilize a new list of places.
		m_SearchBar.SetActive (false); // Making sure that the search bar isn't active in the start of the project.
	}

	void Update ()
	{
        //This function is called each frame. There're 60 frames each second, this means that this function is called 60 times every second.

		//Seaching behavior
		if (Input.GetKeyUp (KeyCode.KeypadEnter) || Input.GetKeyUp (KeyCode.Return)) { //whenever the player press enter (the two enters on the keyboard)
			isSeachSuccessful = false; //This bool indicates if a search was found or not.
			m_PlaceDescription.text = ""; //Making sure that the search bar has no text in it.

			if (isSearchOn == true) { // isSearchOn is a bool that is toggeled true and false by pressing enter.
				if (m_SearchBar.GetComponent <InputField> ().text != null) { //Making sure that searchbar gameobject has a component called InputField (excipition handeling).
					foreach (PlaceObject place in m_Places) { //Looping through places list.
						if (m_SearchBar.GetComponent <InputField> ().text == place.getPlaceName ()) { //If the text of the searchbar matches some place name.
							if (currentSearchedPlace != null && place.getPlaceName() != currentSearchedPlace.name) { //If there's a previous place that was searched, clear that place's light.
								currentSearchedPlace.GetComponent <Place> ().LightOffPlace ();
							}
							currentSearchedPlace = place.getPlaceObject (); //Assign the new searched place to the currentSearchedPlace.
							isSeachSuccessful = true; //Turn isSearchSuccessful to true.
                            break; //Break from the loop, no need to complete it.
						}
					}
					if (isSeachSuccessful == false && m_SearchBar.GetComponent <InputField> ().text != "") { //if the player typed somthing and it didn't match any name in the database, tell him to enter a valid name.
						m_PlaceDescription.text = "Enter a valid name!";
						Invoke ("HideText", 4f); //Calls the funtion "HideText" after 4 seconds.
					} else if (isSeachSuccessful == true) {// if the player typed somthing and it did match a name, call LightOnPlace from currentSearchedPlace and write the place Msg on the screen.
						currentSearchedPlace.GetComponent <Place> ().LightOnPlace ();
						m_PlaceDescription.text = currentSearchedPlace.GetComponent <Place> ().m_PlaceMsg;
						Invoke ("HideText", 4f);// Calls the function "HideText" after 4 seconds.
					}
				}
			}

			isSearchOn = !isSearchOn; //Toggels isSearchOn bool to true/false each time an enter is pressed.
			m_SearchBar.SetActive (isSearchOn); // Toggels searchbar gameobject active/inactive each time an enter is pressed.
            m_SearchBar.GetComponent<InputFieldCustom>().enabled = isSearchOn; //enables the InputFieldCustom script attached to the searchbar gameObject.
			m_Player.GetComponent <FirstPersonController> ().enabled = !isSearchOn; //Toggeles player controller on and off each time an enter is pressed. (The player can't move or do anything if the searchbar is active.
		}
	}

	void HideText ()
	{
        //This functions clears whatever text is in PlaceDescription textholder.
		if (m_PlaceDescription.text != "") {
			m_PlaceDescription.text = "";	
		}
	}
}
