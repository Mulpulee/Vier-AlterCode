using System;
using System.Collections.Generic;
using UnityEngine;
using Utility.Audio;
using Utility.DesignPattern.ObjectPooling;

namespace Utility.Management {
	public class AudioManager : IndestructibleSingleton<AudioManager> {
		public static readonly string AudioResourcesFolderName = "Audios";
		public static readonly string BGMVolumeKey = $"SFXVolume";
		public static readonly string SFXVolumeKey = $"BGMVolume";

		[SerializeField] private float m_specialEffectsVolume;
		[SerializeField] private float m_backgroundMusicVolume;

		public float SFXVolume => m_specialEffectsVolume;
		public float BGMVolume => m_backgroundMusicVolume;

		[SerializeField] private AudioSource m_backgroundMusicSource;
		[SerializeField] private AudioSource m_backgroundMusicSource2;
		[SerializeField] private AudioSource m_nowBackgroundMusicSource;

		private Dictionary<string, AudioClip> m_audioByName;
		private UnityObjectBag<AudioPlayer> m_audioPlayerPool;
		private List<AudioPlayer> m_activatedAudioPlayer;

		protected override void OnInstantiated() {
			InitializeAudioSource(m_backgroundMusicSource);
			InitializeAudioSource(m_backgroundMusicSource2);

			m_audioByName = new Dictionary<string, AudioClip>();
			m_audioPlayerPool = new UnityObjectBag<AudioPlayer>(InstantiateAudioPlayer, (temp) => { }, 100, transform);
			m_activatedAudioPlayer = new List<AudioPlayer>();

			AudioClip[] clips = Resources.LoadAll<AudioClip>(AudioResourcesFolderName);

			foreach (AudioClip clip in clips) {
				m_audioByName.Add(clip.name, clip);
			}

			LoadVolumes();
		}
		private void InitializeAudioSource(AudioSource audioSource) {
			if (audioSource == null) {
				audioSource = gameObject.AddComponent<AudioSource>();
			}
			audioSource.loop = true;
		}
		private AudioPlayer InstantiateAudioPlayer() {
			var instance = new GameObject("AudioPlayer");
			instance.AddComponent<AudioSource>();

			AudioPlayer player = instance.AddComponent<AudioPlayer>();
			player.SetVolume(m_specialEffectsVolume);

			return player;
		}

		public AudioPlayer PlaySFX(AudioClip clip, float pitch = 1.0f, Action callback = null) {
			AudioPlayer player = m_audioPlayerPool.GetObject();
			player.transform.SetParent(transform);
			player.SetVolume(m_specialEffectsVolume);
			player.PlayAudio(clip, pitch, callback);
			player.AddCallback(() => m_activatedAudioPlayer.Remove(player));
			player.Source.spatialBlend = 0.0f;

			m_activatedAudioPlayer.Add(player);

			return player;
		}
		public AudioPlayer PlaySFX(string name, float pitch = 1.0f) {
			if (m_audioByName.TryGetValue(name, out AudioClip clip)) {
				AudioPlayer player = PlaySFX(clip, pitch);
				return player;
			}

			return null;
		}

		public AudioPlayer PlaySFXFrom(AudioClip clip, Transform from, float pitch = 1.0f) {
			AudioPlayer player = PlaySFX(clip, pitch);
			player.transform.SetParent(from);
			player.transform.localPosition = Vector3.zero;
			player.AddCallback(() => player.transform.SetParent(from));
			player.Source.spatialBlend = 1.0f;

			return player;
		}
		public AudioPlayer PlaySFXFrom(string name, Transform from, float pitch = 1.0f) {
			if (m_audioByName.TryGetValue(name, out AudioClip clip)) {
				AudioPlayer player = PlaySFXFrom(clip, from, pitch);
				return player;
			}

			return null;
		}

		public AudioPlayer PlaySFXFrom(AudioClip clip, Vector3 from, float pitch = 1.0f) {
			AudioPlayer player = PlaySFX(clip, pitch);
			player.transform.position = from;
			player.Source.spatialBlend = 0.5f;

			return player;
		}
		public AudioPlayer PlaySFXFrom(string name, Vector3 from, float pitch = 1.0f) {
			if (m_audioByName.TryGetValue(name, out AudioClip clip)) {
				AudioPlayer player = PlaySFXFrom(clip, from, pitch);
				return player;
			}

			return null;
		}

		public void PlayBGM(AudioClip clip) {
			if (m_nowBackgroundMusicSource == null) {
				m_nowBackgroundMusicSource = m_backgroundMusicSource2;
			}

			if (m_nowBackgroundMusicSource.clip != clip) {
				AudioSource nowPlayingSource = m_nowBackgroundMusicSource;
				AudioSource nonPlayingSource = null;
				if (!m_backgroundMusicSource.isPlaying) {
					nonPlayingSource = m_backgroundMusicSource;
				} else {
					nonPlayingSource = m_backgroundMusicSource2;
				}
				m_nowBackgroundMusicSource = nonPlayingSource;

				nonPlayingSource.volume = 0.0f;
				nonPlayingSource.clip = clip;
				nonPlayingSource.Play();

				/* Fade.. */
				nowPlayingSource.volume = 0.0f;
				nonPlayingSource.volume = m_backgroundMusicVolume;
			}
		}
		public void PlayBGM(string name) {
			if (m_audioByName.TryGetValue(name, out AudioClip clip)) {
				PlayBGM(clip);
			}
		}
		public void StopBGM() {
			m_backgroundMusicSource.Stop();
		}

		public void OnBGMVolumeChange(float volume) {
			m_backgroundMusicSource.volume = volume;
			m_backgroundMusicSource2.volume = volume;
			m_backgroundMusicVolume = volume;

			SaveVolumes();
		}
		public void OnSFXVolumeChange(float volume) {
			foreach (AudioPlayer item in m_activatedAudioPlayer) {
				item.SetVolume(volume);
			}
			m_specialEffectsVolume = volume;

			SaveVolumes();
		}

		private void SaveVolumes() {
			PlayerPrefs.SetFloat(BGMVolumeKey, m_backgroundMusicVolume);
			PlayerPrefs.SetFloat(SFXVolumeKey, m_specialEffectsVolume);
		}
		private void LoadVolumes() {
			if (PlayerPrefs.HasKey(BGMVolumeKey)) {
				m_backgroundMusicVolume = PlayerPrefs.GetFloat(BGMVolumeKey, m_backgroundMusicVolume);
			} else {
				m_backgroundMusicVolume = 1.0f;
			}

			if (PlayerPrefs.HasKey(SFXVolumeKey)) {
				m_specialEffectsVolume = PlayerPrefs.GetFloat(SFXVolumeKey, m_specialEffectsVolume);
			} else {
				m_specialEffectsVolume = 1.0f;
			}

			OnBGMVolumeChange(m_backgroundMusicVolume);
			OnSFXVolumeChange(m_specialEffectsVolume);
		}
	}
}