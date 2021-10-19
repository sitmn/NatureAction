using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;
    private Transform playerTr;

    private Vector3 distanceRotatePosition;

    public float rad;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = playerObj.GetComponent<Transform>();

        rad = 1.5f * Mathf.PI;

        rotatePosition(rad);
        transform.position = new Vector3(playerTr.position.x, playerTr.position.y + GameManagerold.cameraDistancePlayerY, playerTr.position.z) + distanceRotatePosition;
        transform.rotation = Quaternion.LookRotation(playerTr.position + Vector3.up * GameManagerold.cameraRotateOffsetY - transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveCamera();
    }

    private void moveCamera()
    {
        rotatePosition(rad);

        Vector3 cameraMove = new Vector3(playerTr.position.x, playerTr.position.y + GameManagerold.cameraDistancePlayerY, playerTr.position.z) + distanceRotatePosition;
        cameraMove = Vector3.Lerp(transform.position, cameraMove, 1 - GameManagerold.cameraPositionDelay);
        transform.position = cameraMove;

        transform.rotation = Quaternion.LookRotation(playerTr.position + Vector3.up * GameManagerold.cameraRotateOffsetY - transform.position);
    }

    /*カメラの円の動き*/
    public void rotatePosition(float rad)
    {
        float distanceZ = GameManagerold.cameraDistancePlayer * Mathf.Sin(rad);
        float distanceX = GameManagerold.cameraDistancePlayer * Mathf.Cos(rad);

        distanceRotatePosition = new Vector3(distanceX, 0, distanceZ);
    }
}
