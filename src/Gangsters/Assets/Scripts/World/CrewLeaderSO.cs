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
    private string _crewName;
    public string CrewName => _crewName;

    public string FullName => $"{FirstName} {LastName}";
}
