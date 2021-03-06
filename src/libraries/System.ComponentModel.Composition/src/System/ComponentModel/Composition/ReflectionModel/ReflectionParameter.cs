// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
    internal sealed class ReflectionParameter : ReflectionItem
    {
        private readonly ParameterInfo _parameter;

        public ReflectionParameter(ParameterInfo parameter!!)
        {
            _parameter = parameter;
        }

        public ParameterInfo UnderlyingParameter
        {
            get { return _parameter; }
        }

        public override string? Name
        {
            get { return UnderlyingParameter.Name; }
        }

        public override string GetDisplayName() =>
            $"{UnderlyingParameter.Member.GetDisplayName()} (Parameter=\"{UnderlyingParameter.Name}\")";  // NOLOC

        public override Type ReturnType
        {
            get { return UnderlyingParameter.ParameterType; }
        }

        public override ReflectionItemType ItemType
        {
            get { return ReflectionItemType.Parameter; }
        }
    }
}
