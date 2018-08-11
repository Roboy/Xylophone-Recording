using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SnapController : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

    public Rigidbody SnapPoint;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    private SteamVR_TrackedObject m_TrackedObject;
    private SteamVR_Controller.Device m_Device;

    private SphereCollider m_SnapRegion;

    private FixedJoint m_Joint = null;
    private GameObject m_SnappedObject = null;
    private bool m_SnapObjectUseGravity;

    private bool m_Snapped = false;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOUR_FUNCTIONS

    private void Start()
    {
        m_SnapRegion = GetComponent<SphereCollider>();
        m_TrackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        m_Device = SteamVR_Controller.Input((int)m_TrackedObject.index);

        if (m_Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Trigger pressed!");
            if (m_SnappedObject != null && m_Joint != null)
            {
                Destroy(m_Joint);
                Rigidbody otherRigidbody = m_SnappedObject.GetComponent<Rigidbody>();
                otherRigidbody.useGravity = m_SnapObjectUseGravity;
                m_SnappedObject = null;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagsConstants.CASSETTE && !m_Snapped)
        {
            m_Snapped = true;

            Debug.Log("Huu! I touch the cassette!");

            m_SnappedObject = other.gameObject;
            m_SnappedObject.transform.position = SnapPoint.transform.position;
            m_SnappedObject.transform.Translate(new Vector3(0.0f, -0.05f, 0.05f));
            Rigidbody otherRigidbody = m_SnappedObject.GetComponent<Rigidbody>();
            m_SnapObjectUseGravity = otherRigidbody.useGravity;
            otherRigidbody.useGravity = false;
            

            m_Joint = m_SnappedObject.AddComponent<FixedJoint>();
            m_Joint.connectedBody = SnapPoint;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == TagsConstants.CASSETTE)
        {
            Debug.Log("Got rid of the cassette!");
            m_Snapped = false;
        }
    }

    #endregion // MONOBEHAVIOUR_FUNCTIONS

}