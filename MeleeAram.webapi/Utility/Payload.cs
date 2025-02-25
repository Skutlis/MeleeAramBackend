using System;

namespace MeleeAram.webapi.Utility;

public class Payload<T>
{
    public T Data { get; set; }
    public string StatusMessage { get; set; } = "Success";
    public bool success { get; set; } = true;


}
