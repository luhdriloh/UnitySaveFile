using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public Dictionary<FlowerColor, int> _numberOfFlowers;
    private readonly string _saveFileName = "/save_data.dat";

    private void Awake()
    {
        FlowerSaveData data = ((FlowerSaveData)Load());

        if (data == null)
        {
            _numberOfFlowers = new Dictionary<FlowerColor, int>();
        }
        else
        {
            _numberOfFlowers = data._flowerData;
        }
    }

    private void OnApplicationQuit()
    {
        FlowerSaveData data = new FlowerSaveData()
        {
            _flowerData = _numberOfFlowers
        };

        Save(data);
    }

    private object Load()
    {
        object toLoad = null;

        if (File.Exists(Application.persistentDataPath + _saveFileName))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.persistentDataPath + _saveFileName, FileMode.Open);

            toLoad = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

        return toLoad;
    }

    private void Save(object toSave)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Open(Application.persistentDataPath + _saveFileName, FileMode.Create);

        binaryFormatter.Serialize(fileStream, toSave);
        fileStream.Close();
    }
}
