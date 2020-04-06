﻿using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.ARFoundation;

/// This component listens for images detected by the <c>XRImageTrackingSubsystem</c>
/// and overlays some information as well as the source Texture2D on top of the
/// detected image.
/// </summary>
[RequireComponent(typeof(ARTrackedImageManager))]
public class TrackedImageInfoManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The camera to set on the world space UI canvas for each instantiated image info.")]
    Camera m_WorldSpaceCanvasCamera;

    [SerializeField]
    public TextMeshProUGUI MainText = default;
    [SerializeField]
    public TextMeshProUGUI TempText = default;

    public float TimeTempTextApear = 2.0f;
    private float time = 0.0f;
    private bool launchTempText = false;
    private bool canSetGame = false;
    private bool gameAlreadySetUp = false;

    /// <summary>
    /// The prefab has a world space UI canvas,
    /// which requires a camera to function properly.
    /// </summary>
    public Camera worldSpaceCanvasCamera
    {
        get { return m_WorldSpaceCanvasCamera; }
        set { m_WorldSpaceCanvasCamera = value; }
    }

    [SerializeField]
    [Tooltip("If an image is detected but no source texture can be found, this texture is used instead.")]
    Texture2D m_DefaultTexture;

    /// <summary>
    /// If an image is detected but no source texture can be found,
    /// this texture is used instead.
    /// </summary>
    public Texture2D defaultTexture
    {
        get { return m_DefaultTexture; }
        set { m_DefaultTexture = value; }
    }

    ARTrackedImageManager m_TrackedImageManager;

    void Awake()
    {
        m_TrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        m_TrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        m_TrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void Update()
    {
       if (launchTempText)
        {
            if (time >= TimeTempTextApear)
            {
                launchTempText = false;
                time = 0.0f;
                TempText.gameObject.SetActive(false);
            }
            time += Time.deltaTime;
        }
    }

    void UpdateInfo(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name.Equals("Charizard_EX"))
        {
            MainText.text = "Charizard-EX Found\nYou Can set up the game";
            if (!gameAlreadySetUp)
            {
                canSetGame = true;
            }
        }
        if (trackedImage.referenceImage.name.Equals("Jayce") && !gameAlreadySetUp)
        {
            TempText.gameObject.SetActive(true);
            launchTempText = true;
            time = 0.0f;
            TempText.text = "Nah it's Jayce form Magic !";
        }
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Give the initial image a reasonable default scale
            trackedImage.transform.localScale = new Vector3(0.01f, 1f, 0.01f);

            UpdateInfo(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
            UpdateInfo(trackedImage);
    }

    public bool CanSetUpGame()
    {
        return (canSetGame);
    }

    public void ChangeSetUpGame(bool status)
    {
        canSetGame = status;
        if (!status)
        {
            gameAlreadySetUp = true;
        }
    }
}
