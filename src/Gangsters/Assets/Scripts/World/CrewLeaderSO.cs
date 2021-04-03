using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CrewLeader")]
public class CrewLeaderSO : ScriptableObject
{
    [SerializeField]
    private string _firstName;
    public string FirstName => _firstName;

    [SerializeField]
    private string _lastName;
    public string LastName => _lastName;

    [SerializeField]
    private string _nickName;
    public string NickName => _nickName;

    public string FullName => $"{FirstName} \"{NickName}\" {LastName}";
}
