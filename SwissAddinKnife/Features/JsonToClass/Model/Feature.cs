using System;
using System.ComponentModel;

namespace SwissAddinKnife.Features.JsonToClass.Model
{
    public enum Feature
    {
        [Description("complete")]
        Complete,

        [Description("attributes-only")]
        AttributesOnly,

        [Description("just-types")]
        JustTypes
    }
}
