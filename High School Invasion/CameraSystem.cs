using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraState
{
    idle,
    isMovingTowards,
    isInPlace,
    isMovingBack
}
public class CameraSystem : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform lookPosition;
    [SerializeField] private Transform playerCameraPosition;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 defaultOffset;

    [SerializeField] private AnimationCurve moveToCurve;
    [SerializeField] private AnimationCurve moveBackCurve;

    private SpriteRenderer playerTopbody;
    private SpriteRenderer playerLowerBody;
    private Transform playerShadow;
    private Transform playerGun;

    private GameObject player;
    public CameraState state = CameraState.idle;

    [SerializeField] private float moveDuration;
    [SerializeField] private float rotateDuration;

    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.Find("Player");

        followObject = GameObject.Find("CameraSystem");

        virtualCamera = GameObject.Find("VirtualCamera").GetComponent<CinemachineVirtualCamera>();

        playerTopbody = GameObject.Find("PlayerUpperBody").GetComponent<SpriteRenderer>();
        playerLowerBody = GameObject.Find("PlayerLowerBody").GetComponent<SpriteRenderer>();
        playerShadow = GameObject.Find("PlayerShadow").GetComponent<Transform>();
        playerGun = GameObject.Find("PlayerGun").GetComponent<Transform>();
    }

    void Start()
    {
        defaultOffset = virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        if (virtualCamera == null)
        {
            Debug.LogError("Camera not found. Check if the name is correct");
        }

        if (followObject == null)
        {
            Debug.LogError("FollowObject not found. Check if the name is correct");
        }

        if (lookPosition == null)
        {
            Debug.LogError("CameraPosition not found. Check if the field is filled");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(MoveCameraToTarget(lookPosition, offset));
        }

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            StartCoroutine(ResetCameraToPlayer(playerCameraPosition, defaultOffset));
        }
    }

    public IEnumerator MoveCameraToTarget(Transform position, Vector3 offset)
    {
        state = CameraState.isMovingTowards;

        player.GetComponent<PlayerController>().enabled = false;
        playerTopbody.enabled = false;
        playerLowerBody.enabled = false;
        playerGun.gameObject.SetActive(false);
        playerShadow.gameObject.SetActive(false);

        followObject.transform.position = position.position;

        float elapsedMove = 0f;
        float elapsedRotate = 0f;
        while (elapsedMove < 1)
        {
            elapsedMove += Time.deltaTime / moveDuration;
            elapsedRotate += Time.deltaTime / rotateDuration;
            float moveT = moveBackCurve.Evaluate(elapsedMove);
            float rotateT = moveBackCurve.Evaluate(elapsedRotate);

            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(defaultOffset, offset, moveT);
            followObject.transform.rotation = Quaternion.Slerp(followObject.transform.rotation, position.rotation, rotateT);

            yield return null;
        }
        state = CameraState.isInPlace;
        yield return null;
    }

    public IEnumerator ResetCameraToPlayer(Transform position, Vector3 defaultOffset)
    {
        state = CameraState.isMovingBack;

        //followObject.transform.rotation = Quaternion.identity;

        followObject.transform.position = position.position;

        player.GetComponent<PlayerController>().enabled = true;
        playerTopbody.enabled = true;
        playerLowerBody.enabled = true;
        playerGun.gameObject.SetActive(true);
        playerShadow.gameObject.SetActive(true);

        followObject.transform.position = position.position;

        float elapsedMove = 0f;
        float elapsedRotate = 0f;
        while (elapsedMove < 1)
        {
            elapsedMove += Time.deltaTime / moveDuration;
            elapsedRotate += Time.deltaTime / rotateDuration;
            float moveT = moveBackCurve.Evaluate(elapsedMove);
            float rotateT = moveBackCurve.Evaluate(elapsedRotate);

            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(offset, defaultOffset, moveT);
            followObject.transform.rotation = Quaternion.Slerp(followObject.transform.rotation, position.rotation, rotateT);

            yield return null;
        }
        state = CameraState.idle;
        yield return null;
    }

    private void RotateAndMoveCamera(Transform position, Vector3 offset, float elapsedTime)
    {
        virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(defaultOffset, offset, elapsedTime);
        followObject.transform.rotation = Quaternion.Slerp(followObject.transform.rotation, position.rotation, elapsedTime);
    }

    public CameraState GetCameraState()
    {
        return state;
    }
}
