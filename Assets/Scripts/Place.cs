/*
 * Description: This class is attached to any collider the indicates a place in the faculty. It handles onCollision actions with the player and information about the place.
 * Note: this Script contains another inner public class called PlaceObject which is used to generat objects for the database in the projectmanager. (the List stores objects of this type).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Place : MonoBehaviour
{
	public float m_TimetoHideText = 5f; // This is a public timer that can be adjusted from the inspector to alter the info text clearing.
	public string m_PlaceMsg; //This is a public string that appears whenever the player search for this place. The string can be changed from the inspector.
    public Sprite m_PlaceDetails;

    private Behaviour halo; //This variable holds a reference of the Halo component which is a special light effect attached to this gameobject.
    private bool isInRange = false; // This bool indicate when the player is near this place or not.

    void Start ()
	{
        // this function is called after the Awake function.

		PlaceObject thisPlace = new PlaceObject (gameObject.name, gameObject); // Creates a new PlaceObject object and assign this information to the new object.
		ProjectManager.PM.m_Places.Add (thisPlace); // Dynamic assignment to the database by calling the PM and adding the new PlaceObject object to the database.
        halo = (Behaviour)GetComponent("Halo"); // getting the halo reference.
    }

	void OnTriggerEnter (Collider col)
	{
        // This function is called on any collision this gameobject does with any other collider.
		if (col.gameObject.tag == "Player") { // If this gameobject collided with the player.
            isInRange = true;
			ProjectManager.PM.m_PlaceDescription.text = gameObject.name; //Setting the text that appears on the screen to this gameobject's name.
            LightOffPlace();
            Invoke ("HideText", m_TimetoHideText); //Calls the function "HideText" after the number of seconds specified by the m_TimeToHideText.
		}
	}

    private void OnTriggerExit(Collider col)
    {
        //This function is called on any collider leaves the collision of this gameobject.
        if (col.gameObject.tag == "Player")
        {
            isInRange = false;

            // TODO remove comments when ayman finishs his job.
            //ProjectManager.PM.m_PlaceDetailsImageHoder.GetComponent<Image>().sprite = null;
            //ProjectManager.PM.m_BGImage.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInRange == true)
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            { //if player pressed Tab button while in range, dynamically assign the sprite of this place to the reference detials image in PM then activate the BGImage in PM.

                // TODO remove comments when ayman finishs his job.
               // ProjectManager.PM.m_PlaceDetailsImageHoder.GetComponent<Image>().sprite = m_PlaceDetails;
                //ProjectManager.PM.m_BGImage.SetActive(true);

            }
        }
    }
    void HideText ()
    { //This functions clears whatever text is in PlaceDescription textholder.
        if (ProjectManager.PM.m_PlaceDescription.text == gameObject.name) {
			ProjectManager.PM.m_PlaceDescription.text = "";
		}
	}

	public void LightOnPlace ()
	{ //This function enables Halo lighting effect and it's called whenever a search was done on this gameobject.
        if (halo != null)
        { //Checking if the this gameobject has a halo effect, if it does, light it up.
            halo.enabled = true;
        }
        else
        { //else, if this gameobject doesn't has a halo effect, write this warnning message to the console.
            Debug.Log("No Halo component on: " + gameObject.name);
        }

	}

	public void LightOffPlace ()
    { //This funtion disables Halo lighting effect and it's called whenever another search happenes or the player walks into this gameobject. (The light isn't needed anymore).
        if (halo != null && halo.enabled == true)
        { //Checking if the this gameobject has a halo effect, if it does, shut it down (When the player walk in, the light isn't needed anymore).
            halo.enabled = false;
        }
        else
        { //else, if this gameobject doesn't has a halo effect, write this warnning message to the console.
            Debug.Log("No Halo component on: " + gameObject.name);
        }
    }
}

public class PlaceObject
{
	string placeName;
	GameObject placeObject;

	public PlaceObject (string placeName, GameObject placeObject)
	{
		this.placeName = placeName;
		this.placeObject = placeObject;
	}

	public string getPlaceName ()
	{
		return placeName;
	}

	public GameObject getPlaceObject ()
	{
		return placeObject;
	}
}