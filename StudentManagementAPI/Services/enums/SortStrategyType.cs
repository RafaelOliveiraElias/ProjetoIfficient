using System.Runtime.Serialization;

namespace StudentManagementAPI.Services.enums
{
    public enum SortStrategyType
    {
        [EnumMember(Value = "Bubble Sort")]
        BubbleSort,

        [EnumMember(Value = "LINQ Sort")]
        LinqSort
    }

}