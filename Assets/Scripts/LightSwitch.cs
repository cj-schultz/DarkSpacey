using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Animator anim;
    public Material activeMat;
    public Material disabledMat;
    public MeshRenderer switchMesh;

    private float useBufferTime = .75f;
    private float lastUseTime;

    private GameController gameController;

    private bool active;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        active = true;
    }

    public void Enable()
    {
        switchMesh.material = activeMat;
        active = true;
    }

    public void Disable()
    {
        switchMesh.material = disabledMat;
        active = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(active && other.tag == "ControllerInteractor" && Time.time - lastUseTime > useBufferTime)
        {
            gameController.ToggleLights();
            lastUseTime = Time.time;
        }
    }
}
