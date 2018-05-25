#region License
// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------
#endregion

using System.Collections.Generic;
using System.Linq;

namespace Auth.Models
{
    public class Exchange
    {
        public Exchange()
        {
            this.CurrentPair = Pairs.FirstOrDefault();
        }


        public List<string> Pairs { get; } = new List<string> { "xbt/usd" };
        public readonly string CurrentPair;
    }
}