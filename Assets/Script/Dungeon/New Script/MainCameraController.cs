using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerObj;
    private Transform PlayerTr;

    private Vector3 distanceRotatePosition;

    public float rad;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveRotate();
    }

    private void moveRotate()
    {
        Vector3 cameraMove = new Vector3(PlayerTr.position.x, PlayerTr.position.y + GameManagerold.cameraDistancePlayerY, PlayerTr.position.z) + distanceRotatePosition;
        cameraMove = Vector3.Lerp(transform.position, cameraMove, 1 - GameManagerold.cameraPositionDelay);
        transform.position = cameraMove;

        transform.rotation = Quaternion.LookRotation(PlayerTr.position + Vector3.up * GameManagerold.cameraRotateOffsetY - transform.position);
    }

    /*カメラの円の動き*/
    public void rotatePosition(float rad)
    {
        float distanceZ = GameManagerold.cameraDistancePlayer * Mathf.Sin(rad);
        float distanceX = GameManagerold.cameraDistancePlayer * Mathf.Cos(rad);

        distanceRotatePosition = new Vector3(distanceX, 0, distanceZ);
    }
}
