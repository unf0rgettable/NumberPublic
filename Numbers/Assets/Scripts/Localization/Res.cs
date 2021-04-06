using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Localization
{
    public class Res : MonoBehaviour
    {
        //Изменения для initCommit
        string json = "";
        private string nameLanguagesFile;
        public static Languages lang { get; private set; }
        private void Awake()
        {
            setUpLanguage();
            loadLanguages();
        }
        #region Language
        private void setUpLanguage()
        {
            BetterStreamingAssets.Initialize();
            if (Application.systemLanguage == SystemLanguage.Russian
                || Application.systemLanguage == SystemLanguage.Ukrainian
                || Application.systemLanguage == SystemLanguage.Belarusian)
            {
                nameLanguagesFile = "ruru";
            }
            else
            {
                nameLanguagesFile = "enus";
            }
        }
        private void loadLanguages()
        {
            if ( !BetterStreamingAssets.FileExists("/lang/" + nameLanguagesFile + ".json"))
            {
                Debug.LogErrorFormat("Streaming asset not found: {0}", "/lang/" + nameLanguagesFile + ".json");
            }

            else
            {
                json = BetterStreamingAssets.ReadAllText("/lang/" + nameLanguagesFile + ".json");
                lang = JsonUtility.FromJson<Languages>(json);

                if (interfaceLanguages != null)
                    interfaceLanguages.onChangeLanguage();            
            }
        }
        #endregion

        public static string[] GetLanguageKey()
        {
            List<string> temp = new List<string>();
            foreach (var item in GetIds())
            {
                temp.Add(item.Name);
            }

            return temp.ToArray();
        }
        public static List<FieldInfo> GetIds()
        {
            return typeof(Languages).GetFields().ToList();
        }

        #region interface
        public static ILanguages interfaceLanguages { get; set; }
        public interface ILanguages
        {
            void onChangeLanguage();
        }
        #endregion
    }

    public class Languages
    {
        /// <summary>
        /// Рестарт
        /// </summary>
        public string Restart;
        /// <summary>
        /// Достижения
        /// </summary>
        public string Achievement;
        /// <summary>
        /// Подсказка
        /// </summary>
        public string Tip;        
        /// <summary>
        /// Бомба
        /// </summary>
        public string Bomb;
        /// <summary>
        /// Удалить цифры
        /// </summary>
        public string RemoveAll;        
        /// <summary>
        /// Отмена
        /// </summary>
        public string Undo;
        /// <summary>
        /// Отмена
        /// </summary>
        public string StartAgain;
        /// <summary>
        /// Ходы кончились
        /// </summary>
        public string MovesOver;
        /// <summary>
        /// Бонус кончился
        /// </summary>
        public string BonusEnd;
        /// <summary>
        /// Получи бонус за просмотр рекламы
        /// </summary>
        public string WatchAdAndGetBonus;
        /// <summary>
        /// Точно перезагрузить уровень?
        /// </summary>
        public string ReloadLvlConfirm;
        /// <summary>
        /// Сетка сотрется!
        /// </summary>
        public string GridClearAlert;
        /// <summary>
        /// Поздравляем уровень завершен!
        /// </summary>
        public string CongratsLvlEnd;
        /// <summary>
        /// Выиграли бонус!
        /// </summary>
        public string WonBonus;
        /// <summary>
        /// Массив для обучения
        /// </summary>
        public List<string> Tutorial;
        /// <summary>
        /// Фразы подтверждения
        /// </summary>
        public List<string> Confirmation;
        /// <summary>
        /// Фразы отрицания
        /// </summary>
        public List<string> Denial;

        public string GetTextById(string id)
        {
            List<FieldInfo> asd = typeof(Languages).GetFields().ToList();
            foreach (var item in asd)
            {
                if (item.Name == id)
                {
                    return item.GetValue(this).ToString();
                }
            }

            return "";
        }
    }
}