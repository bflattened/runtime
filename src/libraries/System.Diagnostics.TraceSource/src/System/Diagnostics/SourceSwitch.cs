// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections;

namespace System.Diagnostics
{
    public class SourceSwitch : Switch
    {
        public SourceSwitch(string name) : base(name, string.Empty) { }

        public SourceSwitch(string displayName, string defaultSwitchValue)
            : base(displayName, string.Empty, defaultSwitchValue)
        { }

        public SourceLevels Level
        {
            get
            {
                return (SourceLevels)SwitchSetting;
            }
            set
            {
                SwitchSetting = (int)value;
            }
        }

        public bool ShouldTrace(TraceEventType eventType)
        {
            return (SwitchSetting & (int)eventType) != 0;
        }

        protected override void OnValueChanged()
        {
            SwitchSetting = (int)Enum.Parse<SourceLevels>(Value, true);
        }
    }
}
