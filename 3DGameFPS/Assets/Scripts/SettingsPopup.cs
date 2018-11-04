using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour {
    [SerializeField] private AudioClip sound;

    // Use this for initialization
    public void Open () {
        gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	public void Close () {
        gameObject.SetActive(false);
	}

    public void OnSubmitName(string name)
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed)
    {
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }

    public void OnSoundToggle()
    {
        Managers.Audio.soundMute = !Managers.Audio.soundMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnSoundValue(float volume)
    {
        Managers.Audio.soundVolume = volume;
    }

    public void OnPlayMusic(int selector) {
        Managers.Audio.PlaySound(sound);

        switch (selector) {
            case 1:
                Managers.Audio.PlayIntroMusic();
                break;
            case 2:
                Managers.Audio.PlayLevelMusic();
                break;
            default:
                Managers.Audio.StopMusic();
                break;
        }
    }

    public void OnMusicToggle() {
        Managers.Audio.musicMute = !Managers.Audio.musicMute;
        Managers.Audio.PlaySound(sound);
    }

    public void OnMusicValue(float volume) {
        Managers.Audio.musicVolume = volume;
    }
}
