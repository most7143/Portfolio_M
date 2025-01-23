using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] Backgrounds;  // ��� 4�� (��� �迭)
    public float[] ParallaxSpeeds;   // �� ����� �̵� �ӵ� (��� �迭�� ���� �ӵ� ����)

    private Vector3 lastCameraPosition; // ī�޶��� ������ ��ġ

    public float CameraSpeed = 2f; // ī�޶��� �̵� �ӵ� (���� ����)
    public float ResetDistance = 20f; // ī�޶� �̵��� �Ÿ� (�̰� �̻� �̵��ϸ� �ʱ�ȭ)

    private void Start()
    {
        // ī�޶��� �ʱ� ��ġ ����
        lastCameraPosition = Camera.main.transform.position;
    }

    private void Update()
    {
        // ī�޶� �̵� �ڵ� �߰� (ī�޶� ���������� �̵�)
        Camera.main.transform.position += Vector3.right * CameraSpeed * Time.deltaTime;

        // ī�޶��� �̵��� ���
        float cameraMovement = Camera.main.transform.position.x - lastCameraPosition.x;

        // �� ����� ������Ʈ
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            // �� ����� �̵��� ���
            float parallax = cameraMovement * ParallaxSpeeds[i];
            Backgrounds[i].position += Vector3.right * parallax;
        }

        Debug.Log(cameraMovement);
        // ī�޶� ������ �Ÿ� �̻� �̵������� �ʱ�ȭ
        if (Camera.main.transform.position.x >= ResetDistance)
        {
            ResetCameraAndBackground();
        }

        // ������ ī�޶� ��ġ�� ������Ʈ
        lastCameraPosition = Camera.main.transform.position;
    }

    private void ResetCameraAndBackground()
    {
        // ī�޶� ��ġ �ʱ�ȭ
        Camera.main.transform.position = new Vector3(0, Camera.main.transform.position.y, Camera.main.transform.position.z);

        // ��� ��ġ �ʱ�ȭ
        foreach (Transform background in Backgrounds)
        {
            background.position = new Vector3(0, background.position.y, background.position.z);
        }
    }
}