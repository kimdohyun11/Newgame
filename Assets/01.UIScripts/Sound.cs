using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] private AudioSource _audio;
    [SerializeField] private Scrollbar _scrollbar;

    private void Start()
    {
        if (_audio == null)
            _audio = GetComponent<AudioSource>();

        if (_scrollbar != null)
        {
            _scrollbar.value = _audio.volume;
            _scrollbar.onValueChanged.AddListener(SetVolume);
        }
    }

    void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            float newVolume = Mathf.Clamp(_audio.volume + scroll * 0.1f, 0f, 1f);
            _audio.volume = newVolume;

            if (_scrollbar != null)
                _scrollbar.value = newVolume;

            Debug.Log("Volume: " + newVolume);
        }
    }

    public void SetVolume(float value)
    {
        _audio.volume = value;
    }
}
