using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassettePlayerManeger : MonoBehaviour {

    public GameObject Indicator;
    public KeyCode TestKeyCode;
    public Vector3 ShootForce;

    private GameObject m_Cassette;

    private void OnCollisionEnter(Collision collision)
    {
        Indicator.SetActive(true);
        if (collision.transform.gameObject.tag == "Cassette")
        {
            m_Cassette = collision.transform.gameObject;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(TestKeyCode) && m_Cassette != null)
        {
            m_Cassette.GetComponent<Rigidbody>().AddForce(ShootForce);
            Indicator.SetActive(false);
            m_Cassette = null;
        }
    }
}
