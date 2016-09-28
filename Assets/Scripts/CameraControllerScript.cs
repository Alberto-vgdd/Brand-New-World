using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour
{
    //The amount of time the camera takes to update it's position.
    private float m_MovementDampTime = 0.1f;
    private float m_ZoomDampTime = 0.1f;

    //Amount of extra zoom to keep the player away form the edges
    private float m_ScreenEdgeOffset = 0.25f;

    //The minimum amount of zoom.
    private float m_MinSize =1f;

    //Camera target/s
   /* [HideInInspector]*/ public Transform[] m_Targets;


    private Camera m_Camera;
    private float m_ZoomSpeed;
    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;

   
    void Awake ()
    {
        m_Camera = transform.FindChild("Camera").GetComponent<Camera>();
        SetStartPositionAndSize();

    }


    private void FixedUpdate()
    {
        Move();
        Zoom();
    }

    private void Move()
    {

        FindAveragePosition();

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_MovementDampTime);
    }

    private void FindAveragePosition()
    {
        //The position between all the targets.
        Vector3 AveragePosition = new Vector3();

        //This float helps the above vector.
        float NumberOfTargets = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if(m_Targets[i] != null) //Must use m_Targets[i].gameObject.activeSelf for every enemy.
            {
                NumberOfTargets++;
                AveragePosition += m_Targets[i].position;
            }
        }

        if (NumberOfTargets > 0)
        {
            AveragePosition /= NumberOfTargets;
        }

        //Make the average position stay at z = 0, and keep the player's y.
        //averagePos.y = m_Targets[0].position.y;
        AveragePosition.z = transform.position.z;
        

        m_DesiredPosition = AveragePosition;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_ZoomDampTime);
    }


    private float FindRequiredSize()
    {
        //The desired position in camera's local coordinates.
        Vector3 DesiredLocalPosition = transform.InverseTransformPoint(m_DesiredPosition);

        float MaximumSize = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            if (m_Targets[i] != null)  //Must use m_Targets[i].gameObject.activeSelf for every enemy.
            {
                //Distance from every target to the desired position.
                Vector3 TargetLocalPosition = transform.InverseTransformPoint(m_Targets[i].position);
                Vector3 DesiredPositionToTarget = TargetLocalPosition - DesiredLocalPosition;

                //Save the maximum absolute value (x/aspectRatio) to adjust camera's zoom to make every target fit. 
                MaximumSize = Mathf.Max(MaximumSize, Mathf.Abs(DesiredPositionToTarget.y));
                MaximumSize = Mathf.Max(MaximumSize, Mathf.Abs(DesiredPositionToTarget.x) / m_Camera.aspect);
            }
            
        }
        
        MaximumSize += m_ScreenEdgeOffset;
        MaximumSize = Mathf.Max(MaximumSize, m_MinSize);

        return MaximumSize;
    }


    public void SetStartPositionAndSize()
    {

        FindAveragePosition();

        transform.position = m_DesiredPosition;

        m_Camera.orthographicSize = FindRequiredSize();
    }



}
