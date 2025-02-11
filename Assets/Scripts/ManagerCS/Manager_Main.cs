using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Main : MonoBehaviour
{
    #region SingleTon
    private static Manager_Main instance = null;
    public static Manager_Main Instance { get { return instance; } }
    private void Awake()
    {
        // DontDestroyOnLoad instance of MainManager to make MainManager SingleTon
        if (instance == null)
        {
            // Init Process Run
            ui_StickerRepository = FindObjectOfType<UI_StickerRepository>();
            GetAllInfoFromServer();
            instance = this;
            Screen.SetResolution(1920, 1080, true);
            DontDestroyOnLoad(instance);
        }
        else return;
    }
    #endregion

    /// <summary>
    /// Declare all "Variable"
    /// </summary>
    #region Declared Variable
    [Header("UIManager")]
    //����
    [SerializeField] private UI_Main ui_Main = null;

    public UI_Main UI_Main { get { return ui_Main; } }

    [field: SerializeField]
    public Manager_PictureDiary manager_PictureDiary { get; private set; }

    private UI_StickerRepository ui_StickerRepository = null;
    public UI_StickerRepository UI_StickerRepository { get { return ui_StickerRepository; } }

    [field: SerializeField]
    public GameObject ui_StickerRepositoryPrefab { get; private set; }

    [field: SerializeField]
    public Func_DiaryToJson func_DiaryToJson { get; private set; }

    [field: SerializeField]
    public GameObject ui_volumePanel { get; private set; }
    public GameObject volumeButton = null;
    private bool isvolumePanelOn = false;
    public bool IsVolumePanelOn { get { return isvolumePanelOn; } private set { } }
    public RectTransform[] buttonPos = null;
    public RawImage bgmXImage = null;
    public RawImage effXImage = null;

    [Header("AudioManager")]
    #region Audio Management
    private Manager_Audio _AudioManager = null;
    private void InitAudioManager() => _AudioManager = GetComponent<Manager_Audio>();
    public Manager_Audio GetAudio() => _AudioManager;
    public AudioSource mainAudioSource = null;
    #endregion

    [Header("===NumberOfStickers===")]
    [SerializeField] private int getBubbleGunStickerNum = 0;
    [SerializeField] private int getAudioStickerNum = 0;
    [SerializeField] private int getSignStickerNum = 0;
    [SerializeField] private int getBubbleStickerNum = 0;
    [SerializeField] private int getFreeStickerNum = 0;
    [SerializeField] private int getDiaryNum = 0;

    [Header("===NumberOfStickers===")]
    [SerializeField] private int setBubbleGunStickerNum = 0;
    [SerializeField] private int setAudioStickerNum = 0;
    [SerializeField] private int setSignStickerNum = 0;
    [SerializeField] private int setBubbleStickerNum = 0;
    [SerializeField] private int setFreeStickerNum = 0;
    [SerializeField] private int setDiaryNum = 0;

    #endregion


    #region These functions are for the purpose of finding a specific manager in specific Scene.

    public void InitPictureDiaryScene()
    {
        manager_PictureDiary = FindObjectOfType<Manager_PictureDiary>();
    }
    public void LeaveAtPictureDiaryScene()
    {
        manager_PictureDiary = null;
    }

    public void InitMainScene()
    {

    }

    #endregion

    private void Start()
    {
        InitAudioManager();
        GetAudio().PlaySound("Main", SoundType.BGM, gameObject, true, false);
        // Exist isFirst value
        if (PlayerPrefs.HasKey("IsFirst") == true)
        {
            return;
        }
        // No Exist isFirst value
        else
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString("IsFirst", "No");
            PlayerPrefs.Save();
        }
        func_DiaryToJson = FindObjectOfType<Func_DiaryToJson>();
    }

    private void Update()
    {
        // Check user input every frame
        Manager_UserInput.UpdateTouch();
    }

    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }

    public void OnClick_StickerRepositoryOn()
    {
        if (!ui_StickerRepositoryPrefab.activeSelf)
            ui_StickerRepositoryPrefab.SetActive(true);
        UI_StickerRepository.OnClick_RepositoryOpen();
    }
    public void OnClick_StickerRepositoryOff()
    {
        if (ui_StickerRepositoryPrefab.activeSelf)
            ui_StickerRepositoryPrefab.SetActive(false);
    }

    //manange recordSticker Number 
    public int GetBubbleGunStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getBubbleGunStickerNum = allFiles.Length;
            return getBubbleGunStickerNum;
        }
    }
    public int GetBubbleStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getBubbleStickerNum = allFiles.Length;
            return getBubbleStickerNum;
        }
    }
    public int GetAudioStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {//
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getAudioStickerNum = allFiles.Length;
            if (getAudioStickerNum > 0)
                if (getAudioStickerNum > 0 && int.Parse(allFiles[getAudioStickerNum - 1].Split(".")[0].Split("-")[1]) >= getAudioStickerNum)
                {
                    getAudioStickerNum = int.Parse(allFiles[getAudioStickerNum - 1].Split(".")[0].Split("-")[1]);
                }
            return getAudioStickerNum + 1;
        }
    }
    public int GetSignStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getSignStickerNum = allFiles.Length;
            if (getSignStickerNum > 0 && int.Parse(allFiles[getSignStickerNum - 1].Split(".")[0].Split("-")[1]) >= getSignStickerNum)
            {
                getSignStickerNum = int.Parse(allFiles[getSignStickerNum - 1].Split(".")[0].Split("-")[1]);
            }
            return getSignStickerNum + 1;
        }
    }
    public int GetRecordNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.wav", SearchOption.TopDirectoryOnly);
            getAudioStickerNum = allFiles.Length;
            if (getAudioStickerNum > 0 && int.Parse(allFiles[getAudioStickerNum - 1].Split(".")[0].Split("_")[1]) >= getAudioStickerNum)
            {
                getAudioStickerNum = int.Parse(allFiles[getAudioStickerNum - 1].Split(".")[0].Split("_")[1]);
            }
            return getAudioStickerNum + 1;
        }
    }
    public int GetFreeStickerNum(string folder)
    {
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/");
            return 0;
        }
        else
        {
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/", "*.png", SearchOption.TopDirectoryOnly);
            getFreeStickerNum = allFiles.Length;
            if (getFreeStickerNum > 0 && int.Parse(allFiles[getFreeStickerNum - 1].Split('.')[0].Split('-')[1]) >= getFreeStickerNum)
            {
                getFreeStickerNum = int.Parse(allFiles[getFreeStickerNum - 1].Split('.')[0].Split('-')[1]);
            }

            return getFreeStickerNum + 1;
        }
    }
    public int GetDiaryNum(string folder, string profileName)
    {
        getDiaryNum = 0;
        if (false == Directory.Exists(Application.persistentDataPath + $"/{folder}/" + profileName + "/Diary/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/" + profileName + "/Diary/");
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/" + profileName + "/Jsons/");
            Directory.CreateDirectory(Application.persistentDataPath + $"/{folder}/" + profileName + "/Record/");
            return 0;
        }
        else
        {
            string time = DateTime.Now.ToString("yyyy_MM_dd");
            string[] allFiles = Directory.GetFiles(Application.persistentDataPath + $"/{folder}/" + profileName + "/Diary/", "*.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < allFiles.Length; i++)
            {
                if (allFiles[i].Contains(time))
                {
                    getDiaryNum++;
                }
            }
            // getDiaryNum = allFiles.Length;
            return getDiaryNum + 1;
        }
    }

    /// <summary>
    /// If the sticker already exists,
    /// 5 is added to the number of times the sticker can be used, 
    /// and if the sticker does not exist, 
    /// the value of 5 is added as a new key.
    /// </summary>
    /// <param name="stickerName"></param>
    public void SaveSticker(string stickerName)
    {
        if (PlayerPrefs.HasKey(stickerName))
        {
            PlayerPrefs.SetInt(stickerName, PlayerPrefs.GetInt(stickerName) + 5);
        }
        else
        {
            PlayerPrefs.SetInt(stickerName, 5);
        }
        PlayerPrefs.Save();
    }

    /// <summary>
    /// First, check if the key of the sticker you want to use exists,
    /// return it if it does not exist,
    /// and if it exists,
    /// subtract 1 from the key value and save it.
    /// </summary>
    /// <param name="stickerName"></param>
    public void UseSticker(string stickerName)
    {
        if (PlayerPrefs.HasKey(stickerName) == false)
        {
            return;
        }
        PlayerPrefs.SetInt(stickerName, PlayerPrefs.GetInt(stickerName) - 1);

        PlayerPrefs.Save();
    }

    public void ReturnSticker(string stickerName)
    {
        if (PlayerPrefs.HasKey(stickerName) == false)
        {
            return;
        }
        PlayerPrefs.SetInt(stickerName, PlayerPrefs.GetInt(stickerName) + 1);
        PlayerPrefs.Save();
    }

    public string GetCurStickerUserCount(string stickerName)
    {
        if (PlayerPrefs.HasKey(stickerName) == false)
        {
            return "-1";
        }
        return PlayerPrefs.GetInt(stickerName).ToString();
    }
    public void SetBubbleGunStickerNum() => setBubbleGunStickerNum++;
    public void SetBubbleStickerNum() => setBubbleStickerNum++;
    public void SetAudioStickerNum() => setAudioStickerNum++;
    public void SetSignStickerNum() => setSignStickerNum++;
    public void SetFreeStickerNum() => setFreeStickerNum++;
    public void SetDiaryNum() => setDiaryNum++;

    public void PlayBGM(string sceneName)
    {
        GetAudio().StopPlaySound(gameObject);
        GetAudio().PlaySound(sceneName, SoundType.BGM, gameObject, true, true);
    }

    public void OnClick_VolumeControlButton()
    {
        isvolumePanelOn = !isvolumePanelOn;
        ui_volumePanel.gameObject.SetActive(isvolumePanelOn);
    }
}