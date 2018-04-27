#region License
// --------------------------------------------------
// Copyright © PayEx. All Rights Reserved.
// 
// This software is proprietary information of PayEx.
// USE IS SUBJECT TO LICENSE TERMS.
// --------------------------------------------------
#endregion

using System;

namespace Auth.Models
{
    public class Bid
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Amount { get; set; }
        public ApplicationUser User { get; set; }

        public string Pair { get; set; } = "xbt/usd";
    }
}