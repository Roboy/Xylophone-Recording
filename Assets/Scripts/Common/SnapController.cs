using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class SnapController : MonoBehaviour {

    #region PUBLIC_MEMBER_VARIABLES

    public Rigidbody SnapPoint;

    #endregion // PUBLIC_MEMBER_VARIABLES

    #region PRIVATE_MEMBER_VARIABLES

    private SphereCollider m_SnapRegion;

    private FixedJoint m_Joint = null;
    private GameObject m_SnappedObject = null;
    private bool m_SnapObjectUseGravity;

    private bool m_Snapped = false;
    private bool m_WantToDrop = false;

    #endregion // PRIVATE_MEMBER_VARIABLES

    #region MONOBEHAVIOUR_FUNCTIONS

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == TagsConstants.CASSETTE && !m_Snapped)
        {
            m_Snapped = true;

            Debug.Log("Huu! I touch the cassette!");

            m_SnappedObject = other.gameObject;
            m_SnappedObject.transform.position = SnapPoint.transform.position;
            m_SnappedObject.transform.rotation = SnapPoint.transform.rotation;
            m_SnappedObject.transform.Translate(new Vector3(0.0f, 0.0f, 0.25f));
            Rigidbody otherRigidbody = m_SnappedObject.GetComponent<Rigidbody>();
            m_SnapObjectUseGravity = otherRigidbody.useGravity;
            otherRigidbody.useGravity = false;
            

            m_Joint = m_SnappedObject.AddComponent<FixedJoint>();
            m_Joint.connectedBody = SnapPoint;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        //  the cassette will drop only after the user presses the trigger
        if (m_WantToDrop && other.tag == TagsConstants.CASSETTE)
        {
            Debug.Log("Got rid of the cassette!");
            m_Snapped = false;
            m_SnappedObject = null;
            m_WantToDrop = false;
        }
    }

    #endregion // MONOBEHAVIOUR_FUNCTIONS

    #region PUBLIC_MEMBER_FUNCTIONS

    public void DropItem()
    {
        Debug.Log("Drop Drop!");
        if (m_SnappedObject != null && m_Joint != null)
        {
            Destroy(m_Joint);
            Rigidbody otherRigidbody = m_SnappedObject.GetComponent<Rigidbody>();
            otherRigidbody.useGravity = m_SnapObjectUseGravity;
            m_WantToDrop = true;
        }
    }

    #endregion // PUBLIC_MEMBER_FUNCTIONS

}