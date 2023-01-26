using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace AcidOcean.UI
{
    public class HealthBar : VisualElement, INotifyValueChanged<float>
    {
        public int width { get; set; }
        public int height { get; set; }
        private float m_value;
        public float value
        {
            get
            {
                m_value = Mathf.Clamp(m_value, 0, 1);
                return m_value;
            }
            set
            {
                if (EqualityComparer<float>.Default.Equals(m_value, value))
                {
                    return;
                }
                if (this.panel != null)
                {
                    using (ChangeEvent<float> pooled = ChangeEvent<float>.GetPooled(this.value, value))
                    {
                        pooled.target = (IEventHandler)this;
                        this.SetValueWithoutNotify(value);
                        this.SendEvent((EventBase)pooled);
                    }
                }
                else
                {
                    SetValueWithoutNotify(value);
                }
            }
        }

        public enum FillType
        {
            Horizontal,
            Vertical
        }

        public Color fillColor { get; set; }

        public FillType fillType { get; set; }

        private VisualElement hbParent;
        private VisualElement hbBackground;
        private VisualElement hbForeground;

        public void SetValueWithoutNotify(float newValue)
        {
            m_value = newValue;
        }

        public new class UxmlFactory : UxmlFactory<HealthBar, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            UxmlIntAttributeDescription m_width = new UxmlIntAttributeDescription() { name = "width", defaultValue = 300 };
            UxmlIntAttributeDescription m_height = new UxmlIntAttributeDescription() { name = "height", defaultValue = 50 };
            UxmlFloatAttributeDescription m_value = new UxmlFloatAttributeDescription() { name = "value", defaultValue = 1f };
            UxmlEnumAttributeDescription<HealthBar.FillType> m_fillType = new UxmlEnumAttributeDescription<FillType>() { name = "fill-type", defaultValue = 0 };
            UxmlColorAttributeDescription m_fillColor = new UxmlColorAttributeDescription() { name = "fill-color", defaultValue = Color.red };

            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var hb = ve as HealthBar;
                hb.width = m_width.GetValueFromBag(bag, cc);
                hb.height = m_height.GetValueFromBag(bag, cc);
                hb.value = m_value.GetValueFromBag(bag, cc);
                hb.fillType = m_fillType.GetValueFromBag(bag, cc);
                hb.fillColor = m_fillColor.GetValueFromBag(bag, cc);

                hb.Clear();
                VisualTreeAsset vt = Resources.Load<VisualTreeAsset>("UI Documents/HealthBar");
                VisualElement healthbar = vt.Instantiate();

                hb.hbParent = healthbar.Q<VisualElement>("healthbar");
                hb.hbBackground = healthbar.Q<VisualElement>("background");
                hb.hbForeground = healthbar.Q<VisualElement>("foreground");

                hb.Add(healthbar);

                hb.hbParent.style.width = hb.width;
                hb.hbParent.style.height = hb.height;
                hb.style.width = hb.width;
                hb.style.height = hb.height;
                hb.hbForeground.style.backgroundColor = hb.fillColor;

                hb.RegisterValueChangedCallback(hb.UpdateHealth);
                hb.FillHealth();
            }
        }

        public void UpdateHealth(ChangeEvent<float> evt)
        {
            FillHealth();
        }

        private void FillHealth()
        {
            if (fillType == FillType.Horizontal)
            {
                hbForeground.style.scale = new Scale(new Vector3(value, 1, 0));
            }
            else
            {
                hbForeground.style.scale = new Scale(new Vector3(1, value, 0));
            }
        }
    }
}