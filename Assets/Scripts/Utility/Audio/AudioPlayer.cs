using System;
using System.Collections;
using UnityEngine;
using Utility.DesignPattern.ObjectPooling;

namespace Utility.Audio {
	[RequireComponent(typeof(AudioSource))]
	public class AudioPlayer : MonoBehaviour, IPoolable {
		[SerializeField] private AudioSource m_audioSource;

		private Action m_returnCallback;
		private Action m_onPlayFinished;

		public AudioSource Source => m_audioSource;

		private void Awake() {
			m_audioSource = GetComponent<AudioSource>();
		}

		public void SetVolume(float volume) {
			m_audioSource.volume = volume;
		}

		public void AddCallback(Action callback) {
			if (m_onPlayFinished == null) {
				m_onPlayFinished = callback;
			} else {
				m_onPlayFinished += callback;
			}
		}
		public void PlayAudio(AudioClip clip, float pitch, Action callback = null) {
			m_audioSource.clip = clip;
			m_audioSource.pitch = pitch;
			m_audioSource.Play();

			StartCoroutine(AudioPlay(clip.length, callback));
		}
		public void PlayAudioFrom(AudioClip clip, float pitch, Transform parent, Action callback = null) {
			transform.SetParent(parent);
			transform.localPosition = Vector3.zero;
			PlayAudio(clip, pitch, callback);
		}

		void IPoolable.SetRelease(Action release) {
			m_returnCallback = release;
		}

		IEnumerator AudioPlay(float delay, Action callback = null) {
			var wait = new WaitForSeconds(delay);
			yield return wait;

			m_returnCallback.Invoke();
			callback?.Invoke();

			m_onPlayFinished?.Invoke();
			m_onPlayFinished = null;
		}
	}
}