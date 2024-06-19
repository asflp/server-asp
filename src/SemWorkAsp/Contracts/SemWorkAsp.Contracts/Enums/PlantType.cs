using System.Runtime.Serialization;

namespace SemWorkAsp.Contracts.Enums;

public enum PlantType
{
    [EnumMember(Value = "Лиственные")]
    Leafy,
    [EnumMember(Value = "Цветущие")]
    Blossoming,
    [EnumMember(Value = "Суккуленты")]
    Succulent,
    [EnumMember(Value = "Другие")]
    Other
}