using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class Manager_Main : MonoBehaviour
{
    #region SingleTon
    private static Manager_Main instance = null;
    public static Manager_Main Instance { get { return instance; } }

    [Header("UIManager")]
    //����
    [SerializeField] private UI_Main ui_Main = null;
    public UI_Main UI_Main { get { return ui_Main; } }
    //�׸��ϱ�
    [SerializeField] private UI_PictureDiary ui_PictureDiary = null;
    public UI_PictureDiary UI_PictureDiary { get { return ui_PictureDiary; } }

    [Header("Datas")]
    [SerializeField] private Data_LocalSticker data_LocalSticker = null;
    public Data_LocalSticker Data_LocalSticker { get { return data_LocalSticker; } }

    private void Awake()
    {
        // DontDestroyOnLoad instance of MainManager to make MainManager SingleTon
        if (instance == null)
        {
            // Init Process Run
            GetAllInfoFromServer();
            instance = this;
            Screen.SetResolution(960, 540, false);
            DontDestroyOnLoad(instance);
        }
        else return;
    }
    #endregion
    /// <summary>
    /// Declare all "Variable"
    /// </summary>

    public Manager_PictureDiary manager_PictureDiary { get; private set; }



    #region These functions are for the purpose of finding a specific manager in specific Scene.
    public void AwakeInPictureDiaryScene()
    {
        manager_PictureDiary = FindObjectOfType<Manager_PictureDiary>();
    }
    public void LeaveAtPictureDiaryScene()
    {
        manager_PictureDiary = null;
    }
    #endregion

    public void AwakeOnCreateSticker()
    {

    }
    private void GetAllInfoFromServer()
    {
        // Receiving data from server, initial work, etc.
    }
}