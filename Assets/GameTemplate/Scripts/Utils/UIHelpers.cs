using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameTemplate.Scripts.Utils
{
    public static class UIHelpers
    {
        #region Text Helpers
        public static void SetText(this Text textComponent, string value)
        {
            if (textComponent != null)
                textComponent.text = value;
        }

        public static void SetText(this TextMeshProUGUI textComponent, string value)
        {
            if (textComponent != null)
                textComponent.text = value;
        }
        #endregion

        #region GameObject Helpers
        public static void SetActive(this GameObject obj, bool active)
        {
            if (obj != null)
                obj.SetActive(active);
        }
        #endregion

        #region Button Helpers
        public static void SetInteractable(this Button button, bool interactable)
        {
            if (button != null)
                button.interactable = interactable;
        }

        public static void AddClickListener(this Button button, UnityEngine.Events.UnityAction action)
        {
            if (button != null && action != null)
                button.onClick.AddListener(action);
        }
        #endregion

        #region CanvasGroup / Fade Helpers
        public static void SetVisible(this CanvasGroup group, bool visible)
        {
            if (group != null)
            {
                group.alpha = visible ? 1f : 0f;
                group.interactable = visible;
                group.blocksRaycasts = visible;
            }
        }

        public static IEnumerator Fade(this CanvasGroup group, float targetAlpha, float duration)
        {
            if (group == null) yield break;

            float startAlpha = group.alpha;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                group.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                yield return null;
            }

            group.alpha = targetAlpha;
            group.interactable = targetAlpha > 0f;
            group.blocksRaycasts = targetAlpha > 0f;
        }
        #endregion

        #region Tooltip Helpers
        public static void ShowTooltip(GameObject tooltip, string text, Vector3 position)
        {
            if (tooltip == null) return;

            tooltip.SetActive(true);
            var textComp = tooltip.GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null)
                textComp.text = text;

            tooltip.transform.position = position;
        }

        public static void HideTooltip(GameObject tooltip)
        {
            if (tooltip == null) return;
            tooltip.SetActive(false);
        }
        #endregion

        #region Layout Helpers
        public static void SetChildActive(Transform parent, bool active)
        {
            if (parent == null) return;

            foreach (Transform child in parent)
            {
                child.gameObject.SetActive(active);
            }
        }

        public static void ClearChildren(Transform parent)
        {
            if (parent == null) return;

            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
        #endregion
    }
}