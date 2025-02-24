using System;

namespace MeleeAram.webapi.Utility;

public class Payload<T>
{
    public T Data { get; set; }
    public string status { get; set; } = "Success";

}
