using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 场景导演类
/// </summary>
public class SceneDirector : MonoBehaviour
{
    public SphereController sphereController;

    public CameraController cameraController;

    public CubeController cubeController;

    [SerializeField] private List<float> muiscCheckPoint;

    //public List<float> muiscCheckDistance;

    public MusicPlayerController musicPlayerController;

    private void Awake() 
    {
        // 点位生成
        cubeController.PedalCreator();

        // 锁定60帧
        Application.targetFrameRate = 60;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}