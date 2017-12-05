using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public enum LightStatus { On, Off }

    public SteamVR_TrackedObject leftController;
    public SteamVR_TrackedObject rightController;

    public GameObject lights;

    public LightSwitch beginningSwitch;
    public LightSwitch endSwitch;

    public Text levelTimeText;

    private LightStatus currentLightStatus;

    private bool gameRunning;
    private float levelTime;

    void Start()
    {
        TurnLightsOn();

        levelTime = 0;
        UpdateLevelTimeText();
    }

    void Update()
    {        
        if(gameRunning)
        {
            levelTime += Time.deltaTime;
            UpdateLevelTimeText();
        }

        if(leftController.isValid)
        {
            SteamVR_Controller.Device leftDevice = SteamVR_Controller.Input((int)leftController.index);
            if (leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                LevelLoader.Instance.LoadNextLevel();
            }
            else if (leftDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                LevelLoader.Instance.LoadPreviousLevel();
            }
        }        

        if(rightController.isValid)
        {
            SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input((int)rightController.index);
            if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                LevelLoader.Instance.LoadNextLevel();
            }
            else if (rightDevice.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                LevelLoader.Instance.LoadPreviousLevel();
            }
        }                        
    }

    public void ToggleLights()
    {
        if(currentLightStatus == LightStatus.On)
        {
            TurnLightsOff();            
        }
        else if (currentLightStatus == LightStatus.Off)
        {
            TurnLightsOn();
        }        
    }

    public bool ObstacleHit()
    {
        if (currentLightStatus == LightStatus.Off)
        {
            TurnLightsOn();
            return true;
        }
        else
        {
            return false;
        }
    }

    void StartLevel()
    {
        gameRunning = true;
        levelTime = 0;
        UpdateLevelTimeText();
    }

    void EndLevel()
    {
        gameRunning = false;
    }

    void TurnLightsOn()
    {
        if(gameRunning)
        {
            EndLevel();
        }

        lights.SetActive(true);
        currentLightStatus = LightStatus.On;

        beginningSwitch.Enable();
        endSwitch.Disable();

        beginningSwitch.anim.SetTrigger("On");
        endSwitch.anim.SetTrigger("On");        
    }

    void TurnLightsOff()
    {
        if (!gameRunning)
        {
            StartLevel();        
        }

        lights.SetActive(false);
        currentLightStatus = LightStatus.Off;

        //beginningSwitch.Disable();
        endSwitch.Enable();

        beginningSwitch.anim.SetTrigger("Off");
        endSwitch.anim.SetTrigger("Off");        
    }

    void UpdateLevelTimeText()
    {
        levelTimeText.text = levelTime.ToString("0.000");
    }
}
