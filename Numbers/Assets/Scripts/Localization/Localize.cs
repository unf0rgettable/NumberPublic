using TMPro;
using UnityEditor;
using UnityEngine;

namespace Localization
{
    public class Localize : MonoBehaviour
    {

        [StringInList(typeof(StaticRes), "GetLanguageKey")]
        [SerializeField]
        private string textID;
        
        private void Start()
        {
            SetText(textID);
        }

#if UNITY_EDITOR
        
        [MenuItem("CONTEXT/TextMeshProUGUI/Localize")]
        public static void AddLocalize(MenuCommand command)
        {
            if (Selection.activeGameObject != null)
            {
                ObjectFactory.AddComponent<Localize>(Selection.activeGameObject);
            }
        }
#endif
        
        private string SetText(string _id)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = Res.lang.GetTextById(_id);
            return Res.lang.GetTextById(_id);
        }
        
    }
}
