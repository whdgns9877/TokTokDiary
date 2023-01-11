using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
public class Func_LoadPngFile : MonoBehaviour
{
    Data_LocalSticker data_LocalSticker = null;

    
    // Start is called before the first frame update
    void Start()
    {
        data_LocalSticker = FindObjectOfType<Data_LocalSticker>();
        FindAllPng();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //������ �ִ� ��� ���� �ܾ����
    public void FindAllPng()
    {
        string path = "C:/Users/User/Desktop/Sticker/";
        string[] allFiles = Directory.GetFiles(path, "*.png",SearchOption.TopDirectoryOnly);
        
        for (int i = 0; i < allFiles.Length; i++)
        {
            byte[] byteTexture = File.ReadAllBytes(allFiles[i]);


            if (byteTexture.Length > 0)
            {
                Texture2D texture = new Texture2D(0, 0);
                texture.LoadImage(byteTexture);


                if (allFiles[i].Contains("Waffle"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Waffle);
                    //Debug.Log("����");
                }
                else if (allFiles[i].Contains("Audio"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Audio);
                    Debug.Log("�����");
                }
                else if (allFiles[i].Contains("Weather"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Weather);
                    Debug.Log("����");
                }
                else if (allFiles[i].Contains("Diary"))
                {
                    Manager_Main.Instance.Data_LocalSticker.AddInventory(texture, ItemType.Diary);
                    Debug.Log("�ϱ�");
                }
                else
                {
                    Debug.Log("������");
                }
            }

            
        }
        Manager_Main.Instance.UI_PictureDiary.UpdateInventory();
    }

    //����ȭ�鿡 �ִ� �ش� �̹��� �ҷ�����
    public Texture2D LoadTexture(string fileName)
    {
        Texture2D texture = new Texture2D(4, 4);
        string path = "C:/Users/User/Desktop/Sticker/";
        FileStream fs = new FileStream(path + fileName + ".png", FileMode.Open, FileAccess.Read);
        byte[] imageData = new byte[fs.Length];
        fs.Read(imageData, 0, (int)fs.Length);
        texture.LoadImage(imageData);

        return texture;
    }
}