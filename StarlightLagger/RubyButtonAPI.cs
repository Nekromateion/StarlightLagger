using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnhollowerRuntimeLib;
using Object = UnityEngine.Object;
using System.Runtime.CompilerServices;

namespace RubyButtonAPI
{
    //Credits:
    //Emilia - Porting it to MelonLoader and helping to keep the git updated
    //Tritn - Helping to keep the git updated

    //This adds a couple of new functions compared to the old one, however,
    //like the last one, I will not be providing any support as I will
    //personally not be using melonloader/unhollower in the near future.

    //Look here for a useful example guide:
    //https://github.com/DubyaDude/RubyButtonAPI/blob/master/RubyButtonAPI_Old.cs

    public class QmButtonBase
    {
        protected string btnQmLoc;
        private string _btnTag;
        protected string btnType;
        protected GameObject button;
        protected readonly int[] initShift = {0, 0};
        protected Color origBackground;
        protected Color origText;

        protected void SetActive(bool isActive)
        {
            button.gameObject.SetActive(isActive);
        }

        protected void SetLocation(int buttonXLoc, int buttonYLoc)
        {
            button.GetComponent<RectTransform>().anchoredPosition +=
                Vector2.right * (420 * (buttonXLoc + initShift[0]));
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (420 * (buttonYLoc + initShift[1]));

            _btnTag = "(" + buttonXLoc + "," + buttonYLoc + ")";
            button.name = btnQmLoc + "/" + btnType + _btnTag;
            button.GetComponent<Button>().name = btnType + _btnTag;
        }

        protected void SetToolTip(string buttonToolTip)
        {
            button.GetComponent<UiTooltip>().field_Public_String_0 = buttonToolTip;
            button.GetComponent<UiTooltip>().field_Public_String_1 = buttonToolTip;
        }
        
        public virtual void SetBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
        }

        public virtual void SetTextColor(Color buttonTextColor, bool save = true)
        {
        }
    }

    public class QmToggleButton : QmButtonBase
    {
        private GameObject _btnOff;
        private Action _btnOffAction;
        private GameObject _btnOn;

        private Action _btnOnAction;

        public QmToggleButton(string btnMenu, int btnXLocation, int btnYLocation, string btnTextOn, Action btnActionOn,
            string btnTextOff, Action btnActionOff, string btnToolTip, Color? btnBackgroundColor = null,
            Color? btnTextColor = null)
        {
            btnQmLoc = btnMenu;
            InitButton(btnXLocation, btnYLocation, btnTextOn, btnActionOn, btnTextOff, btnActionOff, btnToolTip,
                btnBackgroundColor, btnTextColor);
        }

        private void InitButton(int btnXLocation, int btnYLocation, string btnTextOn, Action btnActionOn,
            string btnTextOff, Action btnActionOff, string btnToolTip, Color? btnBackgroundColor = null,
            Color? btnTextColor = null)
        {
            btnType = "ToggleButton";
            button = Object.Instantiate(QmStuff.ToggleButtonTemplate(),
                QmStuff.GetQuickMenuInstance().transform.Find(btnQmLoc), true);

            _btnOn = button.transform.Find("Toggle_States_Visible/ON").gameObject;
            _btnOff = button.transform.Find("Toggle_States_Visible/OFF").gameObject;

            initShift[0] = -3;
            initShift[1] = -1;
            SetLocation(btnXLocation, btnYLocation);

            setOnText(btnTextOn);
            SetOffText(btnTextOff);
            Text[] btnTextsOn = _btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[0].name = "Text_ON";
            btnTextsOn[0].resizeTextForBestFit = true;
            btnTextsOn[1].name = "Text_OFF";
            btnTextsOn[1].resizeTextForBestFit = true;
            Text[] btnTextsOff = _btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[0].name = "Text_ON";
            btnTextsOff[0].resizeTextForBestFit = true;
            btnTextsOff[1].name = "Text_OFF";
            btnTextsOff[1].resizeTextForBestFit = true;

            SetToolTip(btnToolTip);

            SetAction(btnActionOn, btnActionOff);
            _btnOn.SetActive(false);
            _btnOff.SetActive(true);

            if (btnBackgroundColor != null)
                SetBackgroundColor((Color) btnBackgroundColor);
            else
                origBackground = _btnOn.GetComponentsInChildren<Text>().First().color;

            if (btnTextColor != null)
                SetTextColor((Color) btnTextColor);
            else
                origText = _btnOn.GetComponentsInChildren<Image>().First().color;

            SetActive(true);
        }
        
        public override void SetBackgroundColor(Color buttonBackgroundColor, bool save = true)
        {
            var btnBgColorList = _btnOn.GetComponentsInChildren<Image>().Concat(_btnOff.GetComponentsInChildren<Image>())
                .ToArray().Concat(button.GetComponentsInChildren<Image>()).ToArray();
            foreach (var btnBackground in btnBgColorList) btnBackground.color = buttonBackgroundColor;
            if (save)
                origBackground = buttonBackgroundColor;
        }

        public override void SetTextColor(Color buttonTextColor, bool save = true)
        {
            var btnTxtColorList = _btnOn.GetComponentsInChildren<Text>().Concat(_btnOff.GetComponentsInChildren<Text>())
                .ToArray();
            foreach (var btnText in btnTxtColorList) btnText.color = buttonTextColor;
            if (save)
                origText = buttonTextColor;
        }

        private void SetAction(Action buttonOnAction, Action buttonOffAction)
        {
            _btnOnAction = buttonOnAction;
            _btnOffAction = buttonOffAction;

            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            button.GetComponent<Button>().onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(
                (Action) (() => { SetToggleState(!_btnOn.activeSelf, true); })));
        }
        
        public void SetToggleState(bool toggleOn, bool shouldInvoke = false)
        {
            _btnOn.SetActive(toggleOn);
            _btnOff.SetActive(!toggleOn);
            try
            {
                switch (toggleOn)
                {
                    case true when shouldInvoke:
                        _btnOnAction.Invoke();
                        break;
                    case false when shouldInvoke:
                        _btnOffAction.Invoke();
                        break;
                }
            }
            catch
            {
                // ignored
            }
        }
        
        private void setOnText(string buttonOnText)
        {
            Text[] btnTextsOn = _btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[0].text = buttonOnText;
            Text[] btnTextsOff = _btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[0].text = buttonOnText;
        }
        
        private void SetOffText(string buttonOffText)
        {
            Text[] btnTextsOn = _btnOn.GetComponentsInChildren<Text>();
            btnTextsOn[1].text = buttonOffText;
            Text[] btnTextsOff = _btnOff.GetComponentsInChildren<Text>();
            btnTextsOff[1].text = buttonOffText;
        }
    }

    public static class QmStuff
    {
        // Internal cache of the Toggle Button Template for the Quick Menu
        private static GameObject _toggleButtonReference;
        
        // Internal cache of the QuickMenu
        private static QuickMenu _quickMenuInstance;

        // Fetch the Toggle Button Template from the Quick Menu
        public static GameObject ToggleButtonTemplate()
        {
            if (_toggleButtonReference == null)
                _toggleButtonReference =
                    GetQuickMenuInstance().transform.Find("UserInteractMenu/BlockButton").gameObject;
            return _toggleButtonReference;
        }
        
        // Fetch the Quick Menu instance
        public static QuickMenu GetQuickMenuInstance()
        {
            if (_quickMenuInstance == null) _quickMenuInstance = QuickMenu.prop_QuickMenu_0;
            return _quickMenuInstance;
        }
    }
}