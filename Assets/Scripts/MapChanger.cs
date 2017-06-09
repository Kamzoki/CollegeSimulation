using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChanger : MonoBehaviour {

    public Texture m_Map;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ProjectManager.PM.m_FloorMapHolder.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", m_Map);
        }
    }
}
