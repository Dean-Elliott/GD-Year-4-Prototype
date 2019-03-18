using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unnamedCharacter")]
public class Character : ScriptableObject
{
    [Header("selection screen")]
    public string characterName;
    public string description;
    [Header("visuals")]
    public Color mainColour = new Color(1f,1f,1f,1f);
    public Color secondaryColour = new Color(1f,1f,1f,1f);
    [Header("body attributes")]
    public float bodyScale = 1;
    public Vector2 weaponSize = new Vector2(1f,1f);
    public float mass = 1f;
    [Header("movement")]
    public float dashSpeed = 20f;
    public float rotationSpeed = 500f;
    public int rotationDirection = 1;
    public float gravityScale = 1f;
    [Header("special attributes")]
    public bool changeRotationDirectionOnButtonUp;
}
