using UnityEngine;
using System.Collections;

public class CameraControllerScript : MonoBehaviour
{
    //The amount of time the camera takes to update it's position.
    private float m_DampTime = 0.1f;

    //Amount of extra zoom to keep the player away form the edges
    private float m_ScreenEdgeOffset = 0.5f;

    //The minimum amount of zoom.
    private float m_MinSize =1.5f;

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

        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();

        for (int i = 0; i < m_Targets.Length; i++)
        {
            averagePos += m_Targets[i].position;
        }

        if (m_Targets .Length > 0)
        {
            averagePos /= m_Targets.Length;
        }

        //Make the average position stay at z = 0, and keep the player's y.
        //averagePos.y = m_Targets[0].position.y;
        averagePos.z = transform.position.z;
        

        m_DesiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
    }


    private float FindRequiredSize()
    {
        //The desired position in camera's local coordinates.
        Vector3 DesiredLocalPosition = transform.InverseTransformPoint(m_DesiredPosition);

        float MaximumSize = 0f;

        for (int i = 0; i < m_Targets.Length; i++)
        {
            //Distance from every target to the desired position.
            Vector3 TargetLocalPosition = transform.InverseTransformPoint(m_Targets[i].position);
            Vector3 DesiredPositionToTarget = TargetLocalPosition - DesiredLocalPosition;

            //Save the maximum absolute value (x/aspectRatio) to adjust camera's zoom to make every target fit. 
            MaximumSize = Mathf.Max(MaximumSize, Mathf.Abs(DesiredPositionToTarget.y));
            MaximumSize = Mathf.Max(MaximumSize, Mathf.Abs(DesiredPositionToTarget.x) / m_Camera.aspect);
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
