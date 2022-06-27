using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Entity
{
    public class SaveManager
    {
        private static SaveManager _instance;

        private SaveFile _tempFile;
        private SaveFile[] _saveFiles;
        private SaveFile _targetFile; 
        public bool IsSaveInProgress { get; internal set; } 
        public int? LastSaveResult { get; internal set; }
        public bool HasLoadedFromNet { get; internal set; }

        private SaveManager()
        {
            _saveFiles = new SaveFile[16];
        }

        internal SaveFile CreateTempSaveFile()
        {
            _tempFile = new SaveFile();
            _tempFile.Identifier = "Tempname";
            _tempFile.Properties = new Dictionary<string, object>();
            TargetSaveFile(_tempFile);
            return _tempFile;
        }

        internal void SaveTempSaveFile(string identifier)
        {

            var saveFile = GetSaveFile(identifier);
            var index = GetNextAvalibleSaveSlotIndex();
            var id = string.IsNullOrEmpty(identifier) ? "SaveFile" + index : identifier;
            _tempFile.Identifier = id;

            if (saveFile != null)
            {
                OverrightSaveFile(_tempFile);
            }
            else
            { 
                InsertSaveFile(_tempFile);
            }
            _tempFile = null;

        }

        internal void OverrightSaveFile(SaveFile tempFile)
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                if (_saveFiles[i] == null) continue;
                if (string.IsNullOrEmpty(_saveFiles[i].Identifier)) continue;
                if (_saveFiles[i].Identifier.Equals(tempFile.Identifier))
                {
                    _saveFiles[i] = tempFile;
                }
            }
        }

        internal IEnumerable<SaveFile> GetSaveFiles()
        {
            return _saveFiles.Where(e=>e != null);
        }

        internal void TargetSaveFile(int id)
        {
            TargetSaveFile(_saveFiles[id]);
        }

        internal void TargetSaveFile(SaveFile id)
        {
            _targetFile = id;
        }

        internal void TargetSaveFile(string id)
        {
            foreach (var item in GetSaveFiles())
            { 
                if (item == null) continue;
                if (item.Identifier == null) continue;
                if (item.Identifier.Equals(id))
                    TargetSaveFile(item);
            }
            if (_targetFile == null)
                throw new Exception("Could not target a save file, save file does not exist " + id );
        }

        internal int GetNextAvalibleSaveSlotIndex()
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                if (_saveFiles[i] == null)
                    return i;
            }
            return -1;
        }

        internal static SaveManager GetInstance()
        {
            if(_instance == null)
                _instance = new SaveManager();
            return _instance;
        }

        internal void InsertSaveFile(SaveFile saveFile)
        {

            var index = GetNextAvalibleSaveSlotIndex(); 
            if (index != -1)
            { 
                _saveFiles[index] = saveFile; 
            } 
        } 

        internal SaveFile GetTargetSaveFile()
        {
            return _targetFile;
        }

        internal SaveFile GetActiveFile()
        { 
            return _tempFile;
        }

        internal SaveFile GetSaveFile(string identifier)
        {
            foreach (var item in GetSaveFiles())
            {
                if (item == null) continue;
                if (string.IsNullOrEmpty(item.Identifier)) continue;
                if (item.Identifier.Equals(identifier))
                    return item;
            }
            return null;
        }
    }
}
