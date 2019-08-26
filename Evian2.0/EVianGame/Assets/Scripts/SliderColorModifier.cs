using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderColorModifier : MonoBehaviour
{
	[SerializeField] Image m_image = null;
	[SerializeField] Gradient m_gradient;

	Slider m_slider = null;

	private void Start()
	{
		m_slider = GetComponent<Slider>();
	}

	void Update()
    {
		m_image.color = m_gradient.Evaluate(m_slider.normalizedValue);
    }
}
