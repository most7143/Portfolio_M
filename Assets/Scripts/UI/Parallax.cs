using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] Backgrounds;  // 배경 4개 (배경 배열)
    public float[] ParallaxSpeeds;   // 각 배경의 이동 속도 (배경 배열에 맞춰 속도 설정)

    private Vector3 lastCameraPosition; // 카메라의 마지막 위치

    public float CameraSpeed = 2f; // 카메라의 이동 속도 (변경 가능)
    public float ResetDistance = 20f; // 카메라가 이동할 거리 (이거 이상 이동하면 초기화)

    private void Start()
    {
        // 카메라의 초기 위치 저장
        lastCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        // 카메라 이동 코드 추가 (카메라가 오른쪽으로 이동)
        Camera.main.transform.position += Vector3.right * CameraSpeed * Time.deltaTime;

        // 카메라의 이동량 계산
        float cameraMovement = Camera.main.transform.position.x - lastCameraPosition.x;

        // 각 배경을 업데이트
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            // 각 배경의 이동량 계산
            float parallax = cameraMovement * ParallaxSpeeds[i];
            Backgrounds[i].position += Vector3.right * parallax;
        }

        Debug.Log(cameraMovement);
        // 카메라가 설정된 거리 이상 이동했으면 초기화
        if (Camera.main.transform.position.x >= ResetDistance)
        {
            ResetCameraAndBackground();
        }

        // 마지막 카메라 위치를 업데이트
        lastCameraPosition = Camera.main.transform.position;
    }

    private void ResetCameraAndBackground()
    {
        // 카메라 위치 초기화
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);

        // 배경 위치 초기화
        foreach (Transform background in Backgrounds)
        {
            background.position = new Vector3(0, background.position.y, background.position.z);
        }
    }
}