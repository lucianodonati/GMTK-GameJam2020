﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class ImageNamer : MonoBehaviour
{
    [Button(ButtonStyle.Box, Name = "Rename all images")]
    public void RenameImage()
    {
        Image[] objectImages = GetComponentsInChildren<Image>();

        foreach (Image image in objectImages)
        {
            image.gameObject.name = image.sprite.name;
        }
    }
}