using System.Diagnostics.CodeAnalysis;

namespace ReqOverflow.Specs.Controller.Drivers;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public enum VoteDirection
{
    Up = 1,
    Down = -1
}