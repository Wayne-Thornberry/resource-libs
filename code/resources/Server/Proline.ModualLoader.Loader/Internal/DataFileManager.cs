using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Internal
{
    public class DataFile
    {
        public string Identifier { get; set; }
        public Dictionary<string, object> Properties { get; set; }
    }

    public class SaveFile : DataFile
    {

    }

    public class Save
    {
        private const int MAXSAVEFILES = 16;
        public Player Owner { get; private set; }
        public DataFile LastSaveFile { get; private set; }

        internal Save(Player player)
        {
            _saveFiles = new DataFile[MAXSAVEFILES];
            Owner = player;
        }

        private DataFile[] _saveFiles;

        public void InsertSaveFile(DataFile file)
        {
            if (DoesSaveFileExist(file.Identifier))
            {
                for (int i = 0; i < _saveFiles.Length; i++)
                {
                    var saveFile = _saveFiles[i];
                    if (saveFile == null) continue;
                    if (string.IsNullOrEmpty(saveFile.Identifier)) continue;
                    if (saveFile.Identifier.Equals(file.Identifier)) _saveFiles[i] = file;
                }
            }
            else
            {
                for (int i = 0; i < _saveFiles.Length; i++)
                {
                    if (_saveFiles[i] == null)
                    {
                        _saveFiles[i] = file;
                        LastSaveFile = file;
                        return;
                    }
                }
                throw new Exception("Unable to save file, no save file slots avalible");
            }
        }

        public void DeleteSaveFile(string identifier)
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                var saveFile = _saveFiles[i];
                if (saveFile == null) continue;
                if (string.IsNullOrEmpty(saveFile.Identifier)) continue;
                if (saveFile.Identifier.Equals(identifier)) _saveFiles[i] = null;
            }
        }

        public IEnumerable<DataFile> GetSaveFiles()
        {
            return _saveFiles.Where(e => e != null);
        }
        internal int GetNextSlotIndex()
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                if (_saveFiles[i] == null)
                    return i;
            }
            return -1;
        }

        private bool DoesSaveFileExist(string identifier)
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                var saveFile = _saveFiles[i];
                if (saveFile == null) continue;
                if (string.IsNullOrEmpty(saveFile.Identifier)) continue;
                if (saveFile.Identifier.Equals(identifier)) return true;
            }
            return false;
        }

        public void ClearSaveFiles()
        {
            _saveFiles = new SaveFile[MAXSAVEFILES];
        }

        internal DataFile GetSaveFile(string identifier)
        {
            for (int i = 0; i < _saveFiles.Length; i++)
            {
                var saveFile = _saveFiles[i];
                if (saveFile == null) continue;
                if (string.IsNullOrEmpty(saveFile.Identifier)) continue;
                if (saveFile.Identifier.Equals(identifier)) return _saveFiles[i];
            }
            return null;
        }
    }

    public class DataFileManager
    {
        private static DataFileManager _instance;
        private List<Save> _saves;
        internal DataFile TempFile { get; set; }
        internal DataFile ActiveFile { get; set; }
        internal bool IsSaveInProgress { get; set; }
        internal int? LastSaveResult { get; set; }
        internal bool HasLoadedFromNet { get; set; }

        private DataFileManager()
        {
            _saves = new List<Save>();
        }


        public Save GetSave(Player player)
        {
            var save = _saves.FirstOrDefault(e => e.Owner == player);
            return save;
        }
        internal Save CreateSave(Player player)
        {
            if (DoesPlayerHaveSave(player))
                throw new Exception("Player already has a save, cannot create duplicate player saves");
            var save = new Save(player);
            _saves.Add(save);
            return save;
        }

        internal bool DoesPlayerHaveSave(Player player)
        {
            var save = _saves.FirstOrDefault(e => e.Owner == player);
            return save != null;
        }

        internal static DataFileManager GetInstance()
        {
            if (_instance == null)
                _instance = new DataFileManager();
            return _instance;
        }

        internal void ClearTempFile()
        {
            TempFile = null;
        }

        internal void InsertSave(Save save)
        {
            _saves.Add(save);
        }
    }
}
