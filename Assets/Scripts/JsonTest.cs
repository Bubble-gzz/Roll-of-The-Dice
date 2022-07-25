using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest
{
    public static void Save(LevelInfo levelInfo)
    {
        string js = JsonUtility.ToJson(levelInfo);
        Debug.Log(js);
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.json";
        //�򿪻����½��ĵ�
        using (StreamWriter sw = new StreamWriter(fileUrl))
        {
            //��������
            sw.WriteLine(js);
            //�ر��ĵ�
            sw.Close();
            sw.Dispose();
        }
    }

    public static LevelInfo Read()
    {
        //string���͵����ݳ���
        string readData;
        //��ȡ��·��
        string fileUrl = Application.streamingAssetsPath + "\\jsonInfo.json";
        //��ȡ�ļ�
        using (StreamReader sr = File.OpenText(fileUrl))
        {
            //���ݱ���
            readData = sr.ReadToEnd();
            sr.Close();
        }

        LevelInfo levelInfo = JsonUtility.FromJson<LevelInfo>(readData);
        return levelInfo;
    }
}
