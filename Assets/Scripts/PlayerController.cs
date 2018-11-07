using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private float mouseSensitivity;

    [Header("Player Stats")]
    public float playerHP;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float moveSpeed;

    private bool isGrounded;

    //movement
    private Rigidbody rb;
    Camera mainCam;
    private Vector3 playerVel,playerRot,upForce;
    private float cameraRotX, currCameraX;
    private bool isJump;
    private float CameraRotLimit = 85f;
    #endregion

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCam = GetComponentInChildren<Camera>();
    }

    private void FixedUpdate()
    {
        //movement　プレーヤローテーション　横
        if (playerVel != Vector3.zero) rb.MovePosition(rb.position + playerVel * Time.fixedDeltaTime) ;
        rb.MoveRotation(rb.rotation * Quaternion.Euler(playerRot));
        //rotation プレーヤローテーション　縦
        if (mainCam != null)
        {
            currCameraX -= cameraRotX;
            currCameraX = Mathf.Clamp(currCameraX, -CameraRotLimit, CameraRotLimit);

            mainCam.transform.localEulerAngles = new Vector3(currCameraX, 0f, 0f);
        }
        //jump action
        if (upForce != Vector3.zero) rb.AddForce(upForce, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        // input movement 動き
        float moveHor = Input.GetAxisRaw("Horizontal");
        float moveVer = Input.GetAxisRaw("Vertical");
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        if (upForce != Vector3.zero) 

        //jumping
        upForce = Vector3.zero;
        if (Input.GetButton("Jump"))   isJump = true;
        else isJump = false;
        if (isJump) upForce = Vector3.up * jumpHeight;

        Vector3 movHor = transform.right * moveHor;
        Vector3 movVer = transform.forward * moveVer;
        playerRot = new Vector3(0f, mouseX, 0f) * mouseSensitivity;
        cameraRotX = mouseY * mouseSensitivity;
        playerVel = (movHor + movVer).normalized * moveSpeed;

    }

    private void GroundedCheck()
    {

    }


}
