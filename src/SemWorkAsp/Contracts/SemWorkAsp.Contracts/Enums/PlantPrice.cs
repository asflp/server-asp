using System.Runtime.Serialization;

namespace SemWorkAsp.Contracts.Enums;

public enum PlantPrice
{
    [EnumMember(Value = "от 500")]
    First,
    [EnumMember(Value = "от 501 до 1000")]
    Second, 
    [EnumMember(Value = "от 1001 до 2000")]
    Third,
    [EnumMember(Value = "от 2001")]
    Fourth
}