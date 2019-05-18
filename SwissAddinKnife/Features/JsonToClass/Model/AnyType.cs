using System;
using System.ComponentModel;

namespace SwissAddinKnife.Features.JsonToClass.Model
{
    public enum AnyType
    {
        [Description("object")]
        Object,
        [Description("dynamic")]
        Dynamic
    }
}
